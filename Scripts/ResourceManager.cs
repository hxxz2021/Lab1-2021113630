using System;
using System.Linq;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

namespace TutorialInfo.Scripts
{
    public class ResourceManager : MonoBehaviour
    {
        private List<Resource> _resource;
        private List<Building> _building;
        private Job _job;
        private Date _date;

        private Message _message;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            _resource = new List<Resource>();
            
            foreach (var i in Tools.Resource)
                _resource.Add(new Resource(i, 0));

            _resource[TranslateResource("木头")].Increase(100000);

            _building = new List<Building>();
            foreach (var i in Tools.Building)
                _building.Add(new Building(i.L, i.R));
            
            var buildingCost = Tools.BuildingCost;
            for (var i = 0; i < buildingCost.Count; i ++)
            {
                _building[i].Cost = buildingCost[i].L;
                _building[i].Step = buildingCost[i].R;
            }
            
            _date = new Date();
            _message = FindObjectOfType<Message>();
            
            _job = new Job(Tools.Job);
            _job.SetConsume(Tools.JobConsume);
            _job.SetProduce(Tools.JobProduce);
            
            UnityEngine.Random.InitState(DateTime.Now.Millisecond);
            
            _resource[TranslateResource("木头")].Available = 1;
        }
        
        private int TranslateResource(string resourceName) 
        {
            for (var i = 0; i < _resource.Count; i ++) 
                if (_resource[i].Name == resourceName)
                    return i;
            return -1;
        }

        private int TranslateBuilding(string buildingName) 
        {
            for (var i = 0; i < _building.Count; i++)
                if (_building[i].Name == buildingName)
                    return i;
            return -1;
        }

        private int TranslateJob(string jobName)
        {
            for (var i = 0; i < _building.Count; i++)
                if (_job.ScheduleList[i].Name == jobName)
                    return i;
            return -1;
        }

        public List<Building> GetCraftList() { return _building; }
        public List<Resource> GetResourceList() { return _resource; }
        public Job GetJobList() { return _job; }
        
        public int GetDate() { return _date.Cnt; }

        public bool ResourceCheck(List<int> cost)
        {
            return !cost.Where((t, i) => _resource[i].Cnt < t).Any();
        }

        public void ResourceCost(List<int> cost)
        {
            for (var i = 0; i < cost.Count; i++)
                _resource[i].Decrease(cost[i]);
            FindObjectOfType<ResourceList>().Refresh();
        }
        
        public void ResourceGain(List<int> cost)
        {
            for (var i = 0; i < cost.Count; i++)
                _resource[i].Increase(cost[i]);
            FindObjectOfType<ResourceList>().Refresh();
        }
        
        private void BuildAddition(int index)
        {
            switch (_building[index].Name)
            {
                case "货车":
                    break;
                case "陷阱":
                    break;
                case "小屋":
                    _job.CntMax += 5;
                    break;
                case "制革屋":
                    break;
                case "熏肉房":
                    break;
                case "工坊":
                    break;
                case "水晶工坊":
                    break;
            }
        }
        
        public void Build(int index)
        {
            if (_building[index].Available == 0 || _building[index].Remains == 0)
                return;
            
            var cost = _building[index].GetPrice;
            var message = GameMessage.BuildMessage[index];
            
            if (!ResourceCheck(cost))
            {
                _message.Add(GameMessage.ShortageMessage);
                return;
            }
            
            ResourceCost(cost);
            _building[index].Increase();
            for (var i = 1; i < message.Count; i ++)
                _message.Add(message[i]);
            
            BuildAddition(index);
            
            if (_building[index].CntMax == 1 || _building[index].Remains > 0) 
                return;
            _message.Add(message[0]);
        }

        public void MakeFire()
        {
            var tmp = GameMessage.FireMessage;
            if (!ResourceCheck(new List<int>{ 1 }))
            {
                _message.Add(tmp[0]);
                return;
            }
            ResourceCost(new List<int>{ 1 });
            switch (++ _date.FireTimes)
            {
                case 1:
                    _message.Add(tmp[1]);
                    break;
                case 2:
                    _message.Add(tmp[2]);
                    break;
                default:
                    _message.Add(tmp[3]);
                    break;
            }
            FindObjectOfType<MakeFire>().Click(_date.FireTimes);
        }
            
        public void Log()
        {
            var tmp = GameMessage.LogMessage;
            foreach (var i in tmp)
                _message.Add(i);
            ResourceGain(new List<int> {_building[TranslateBuilding("货车")].Cnt == 0 ? 
                UnityEngine.Random.Range(5, 25) : UnityEngine.Random.Range(25, 50)});
        }

        public void GainTrap()
        {
            if (_building[TranslateBuilding("陷阱")].Cnt == 0)
            {
                _message.Add(GameMessage.GainTrapMessage[0]);
                return;
            }

            var tmp = Enumerable.Repeat(0, Tools.Resource.Count).ToList();
            var date = Tools.LogDate(_date);
            for (var i = 0; i < _building[TranslateBuilding("陷阱")].Cnt; i ++)
            {
                tmp[TranslateResource("皮毛")] += UnityEngine.Random.Range(1, 5) * date;
                tmp[TranslateResource("肉")] += UnityEngine.Random.Range(1, 5) * date;
            }

            if (_resource[TranslateResource("水晶")].Cnt == 0)
                tmp[TranslateResource("水晶")] = 1;
            for (var i = 0; i < _building[TranslateBuilding("陷阱")].Cnt; i ++)
            {
                if (UnityEngine.Random.Range(1, 1000000) <= 100)
                    tmp[TranslateResource("水晶")] += 1;
                if (UnityEngine.Random.Range(1, 1000000) <= 1)
                    tmp[TranslateResource("符文碎片")] += 1;
            }
            if (tmp[TranslateResource("符文碎片")] != 0)
                tmp[TranslateResource("水晶")] += 1;
            
            ResourceGain(tmp);
            
            if (tmp[TranslateResource("水晶")] == 0)
                _message.Add(GameMessage.GainTrapMessage[1]);
            else if (tmp[TranslateResource("符文碎片")] == 0)
                _message.Add(GameMessage.GainTrapMessage[2]);
            else
                _message.Add(GameMessage.GainTrapMessage[3]);

            _resource[TranslateResource("水晶")].Available = 1;
            
            if (tmp[TranslateResource("符文碎片")] != 0)
                _resource[TranslateResource("符文碎片")].Available = 1;
        }
        
        public void JobAdjust(int index, int tmp)
        {
            if (tmp > 0)
                _job.IncreaseJob(index, tmp);
            else
                _job.DecreaseJob(index, Tools.Abs(tmp));
            FindObjectOfType<ResourceList>().Refresh();
        }

        public static void SwitchRoom(int index)
        {
            switch (index)
            {
                case 1:
                    FindObjectOfType<BuildingList>().Refresh();
                    break;
                case 2:
                    FindObjectOfType<JobList>().Refresh();
                    break;
                case 3:
                    break;
            }
        }

        public void JobListClear()
        {
            _job.Clear(); 
            FindObjectOfType<JobList>().Refresh();
        }

        private void ResourceStatistics()
        {
            var consume = Enumerable.Repeat(0, Tools.Resource.Count).ToList();
            var produce = Enumerable.Repeat(0, Tools.Resource.Count).ToList();
            
            var scheduleListTMP = _job.ScheduleList;
            foreach (var i in scheduleListTMP)
            {
                for (var j = 0; j < i.Consume.Count; j++)
                    consume[j] += i.Consume[j] * i.Cnt;
                for (var j = 0; j < i.Produce.Count; j++)
                    produce[j] += i.Produce[j] * i.Cnt;
            }
            
            var extraListTMP = _job.ExtraList;
            foreach (var i in extraListTMP)
            {
                for (var j = 0; j < i.Consume.Count; j++)
                    consume[j] += i.Consume[j] * i.Cnt;
                for (var j = 0; j < i.Produce.Count; j++)
                    produce[j] += i.Produce[j] * i.Cnt;
            }

            if (!ResourceCheck(consume))
            {
                _message.Add(GameMessage.ShortageMessage);
                return;
            }
            ResourceCost(consume);
            ResourceGain(produce);
        }

        private void VillagerIncrease()
        {
            switch (_job.Remains)
            {
                case 0:
                    return;
                case <= 3 when UnityEngine.Random.Range(1, 100) <= 70:
                    _job.Increase();
                    _message.Add(GameMessage.VillagerIncreaseMessage[0]);
                    break;
                case <= 15 when UnityEngine.Random.Range(1, 100) <= 70:
                    _job.Increase(UnityEngine.Random.Range(3, 7));
                    _message.Add(GameMessage.VillagerIncreaseMessage[1]);
                    break;
                default:
                    _job.Increase(UnityEngine.Random.Range(_job.Remains - 10, _job.Remains - 5));
                    _message.Add(GameMessage.VillagerIncreaseMessage[2]);
                    break;
            }
        }
        
        private void Unlock()
        {
            var resourceTMP = Enumerable.Repeat(0, Tools.Resource.Count).ToList();
            var buildingTMP = Enumerable.Repeat(0, Tools.Building.Count).ToList();
            var jobTMP = Enumerable.Repeat(0, Tools.Building.Count).ToList();

            if (_job.Cnt > 0)
                resourceTMP[TranslateResource("皮毛")] = resourceTMP[TranslateResource("肉")] = 1;

            if (_resource[TranslateResource("皮毛")].Cnt > 0 && _resource[TranslateResource("肉")].Cnt > 0)
                buildingTMP[TranslateBuilding("制革屋")] = buildingTMP[TranslateBuilding("熏肉房")] = 1;
            
            if (_resource[TranslateResource("皮革")].Cnt > 0 && _resource[TranslateResource("熏肉")].Cnt > 0)
                buildingTMP[TranslateBuilding("旅馆")] = buildingTMP[TranslateBuilding("贸易站")] = 
                    buildingTMP[TranslateBuilding("工坊")] = buildingTMP[TranslateBuilding("水晶工坊")] = 1;
            
            if (_date.Cnt >= 2)
            {
                buildingTMP[TranslateBuilding("货车")] = 1;
                buildingTMP[TranslateBuilding("小屋")] = 1;
                buildingTMP[TranslateBuilding("陷阱")] = 1;
            }
            
            if (_building[TranslateBuilding("小屋")].Cnt > 0)
                jobTMP[TranslateJob("猎人")] = jobTMP[TranslateJob("采集者")] = 1;
            
            if (_building[TranslateBuilding("制革屋")].Cnt > 0)
                resourceTMP[TranslateResource("皮革")] = jobTMP[TranslateJob("制革师")] = 1;
            
            if (_building[TranslateBuilding("熏肉房")].Cnt > 0)
                resourceTMP[TranslateResource("熏肉")] = jobTMP[TranslateJob("熏肉师")] = 1;
            
            if (_building[TranslateBuilding("工坊")].Cnt > 0)
                jobTMP[TranslateJob("铁矿工人")] = 1;
            
            if (_building[TranslateBuilding("水晶工坊")].Cnt > 0)
                jobTMP[TranslateJob("水晶工匠")] = 1;
            
            for (var i = 0; i < _resource.Count; i++)
                _resource[i].Available |= resourceTMP[i];
            for (var i = 0; i < _building.Count; i++)
                _building[i].Available |= buildingTMP[i];
            for (var i = 0; i < _job.ScheduleList.Count; i++)
                _job.ScheduleList[i].Available |= jobTMP[i];
        }

        public void NextDay()
        {
            _date.FireTimes = 0;
            _date.Cnt ++;
            
            FindObjectOfType<DateDisplay>().Refresh(_date.Cnt);
            FindObjectOfType<SwitchRoom>().Refresh();

            ResourceStatistics();
            
            FindObjectOfType<NewDay>().EventSelect(_date);
            
            VillagerIncrease();
            
            Unlock();
        }
     }
}
