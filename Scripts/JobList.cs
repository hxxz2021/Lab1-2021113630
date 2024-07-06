using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TutorialInfo.Scripts
{
	public class JobList : MonoBehaviour
	{
		private ResourceManager _resourceManager;
		public GameObject jobButtonTemplate, jobColumnTemplate;
		private List<GameObject> _buttonList;
		private List<int> _indexList;

		public void Start()
		{
			if (!_resourceManager)
				Init();
		}

		private void Init()
		{
			_resourceManager = FindObjectOfType<ResourceManager>();
			_buttonList = new List<GameObject>();
			_indexList = new List<int>();
		}
		
		public void Refresh()
		{
			if (!_resourceManager)
				Init();
			
			Debug.Log(111);
			
			var jobList = _resourceManager.GetJobList();
			_buttonList.Clear();
			_indexList.Clear();
			
			for (var i = 0; i < gameObject.transform.childCount; i ++)
				Destroy(gameObject.transform.GetChild(i).gameObject);
			
			foreach (var i in jobList.ScheduleList)
			{
				if (i.Available == 0)
					continue;
				var buttonTMP = Instantiate(jobButtonTemplate, gameObject.transform, false);
				buttonTMP.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Tools.JobName(jobList.ScheduleList.IndexOf(i));
				buttonTMP.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = i.Cnt.ToString();
				
				var consume = i.Consume;
				Debug.Log(i.Consume.Count);
				for (var j = 0; j < consume.Count; j++)
				{
					if (consume[j] == 0)
						continue;
					var columnTMP = Instantiate(jobColumnTemplate, buttonTMP.transform.GetChild(6).transform, false);
					columnTMP.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Tools.ResourceName(j);
					columnTMP.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
						"<b>-" + consume[j] * i.Cnt + "</b>/天";
				}
				
				var produce = i.Produce;
				Debug.Log(i.Produce.Count);
				for (var j = 0; j < produce.Count; j++)
				{
					if (produce[j] == 0)
						continue;
					var columnTMP = Instantiate(jobColumnTemplate, buttonTMP.transform.GetChild(6).transform, false);
					columnTMP.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Tools.ResourceName(j);
					columnTMP.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
						"<b>+" + produce[j] * i.Cnt + "</b>/天";
				}

				if (jobList.ScheduleList.IndexOf(i) == 0)
				{
					buttonTMP.transform.Find("Increase01").gameObject.SetActive(false);
					buttonTMP.transform.Find("Decrease01").gameObject.SetActive(false);
					buttonTMP.transform.Find("Increase10").gameObject.SetActive(false);
					buttonTMP.transform.Find("Decrease10").gameObject.SetActive(false);
				}
				
				var tmpIncrease01 = buttonTMP.transform.Find("Increase01").GetComponent<Button>();
				var tmpDecrease01 = buttonTMP.transform.Find("Decrease01").GetComponent<Button>();
				var tmpIncrease10 = buttonTMP.transform.Find("Increase10").GetComponent<Button>();
				var tmpDecrease10 = buttonTMP.transform.Find("Decrease10").GetComponent<Button>();
				
				tmpIncrease01.onClick.AddListener(() => Click(buttonTMP, tmpIncrease01));
				tmpDecrease01.onClick.AddListener(() => Click(buttonTMP, tmpDecrease01));
				tmpIncrease10.onClick.AddListener(() => Click(buttonTMP, tmpIncrease10));
				tmpDecrease10.onClick.AddListener(() => Click(buttonTMP, tmpDecrease10));
				
				_buttonList.Add(buttonTMP);
				_indexList.Add(jobList.ScheduleList.IndexOf(i));
			}
		}

		private void Click(GameObject obj, Button button)
		{
			var index = _buttonList.IndexOf(obj);
			var tmp = button.name switch
			{
				"Increase01" => 1,
				"Decrease01" => -1,
				"Increase10" => 10,
				_ => -10,
			};
			_resourceManager.JobAdjust(_indexList[index], tmp);
			Refresh();
		}
	}
}