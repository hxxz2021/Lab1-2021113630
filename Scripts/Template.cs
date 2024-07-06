using System.Collections.Generic;
using System.Linq;

namespace TutorialInfo.Scripts
{
	public class Resource
	{
		public string Name;
		public int Cnt, CntMax, Available;
		
		public int Remains => CntMax == -1 ? -1 : CntMax - Cnt;

		public Resource(string nameInit = "", int cntInit = 0, int cntMaxInit = -1, int availableInit = 0)
		{
			Available = availableInit;
			Name = nameInit;
			Cnt = cntInit; 
			CntMax = cntMaxInit;
		}

		public void Increase(int increment = 1)
		{
			if (increment < 0 || CntMax != -1 && Cnt + increment > CntMax)
				return;
			Cnt += increment;
		}

		public void Decrease(int decrement = -1)
		{
			if (decrement < 0 || Cnt < decrement)
				return;
			Cnt -= decrement;
		}
	}

	public class Building : Resource
	{
		public List<int> Cost, Step;

		public Building(string nameInit = "", int cntMaxInit = 1, int cntInit = 0) : base(nameInit, cntInit, cntMaxInit)
		{
			Cost = new List<int>();
			Step = new List<int>();
		}

		public List<int> GetPrice
		{
			get
			{
				if (Remains == 0)
					return new List<int>();
				var tmp = new List<int>(Cost.Select(t => t).ToList());
				for (var i = 0; i < Tools.Min(Cost.Count, Step.Count); i++)
					tmp[i] += Step[i] * Cnt;
				return tmp;
			}
		}
	}
	public class Universal : Resource
	{
		public List<int> Consume, Produce;

		public Universal(string nameInit = "", int availableInit = 0, List<int> consumeInit = null, List<int> produceInit = null ) : base(nameInit, 0, -1, availableInit)
		{
			Consume = consumeInit ?? new List<int>();
			Produce = produceInit ?? new List<int>();
		}
	}
	
	public class Job : Resource
	{
		public List<Universal> ScheduleList, ExtraList;

		public Job(List<string> nameInit) : base("", 0, 0)
		{
			ScheduleList = new List<Universal>();
			foreach (var i in nameInit)
				ScheduleList.Add(new Universal(i));
			
			ExtraList = new List<Universal>();
		}

		public new void Increase(int increment = 1)
		{
			if (increment < 0 || Cnt + increment > CntMax)
				return;
			Cnt += increment;
			IncreaseJob(0, increment);
		}

		public new void Decrease(int decrement = 1)
		{
			if (decrement < 0 || Cnt < decrement)
				return;
			Cnt -= decrement;
			for (var i = ScheduleList.Count - 1; i >= 0; i --)
			{
				var tmp = Tools.Min(decrement, ScheduleList[i].Cnt);
				decrement -= tmp;
				DecreaseJob(i, tmp);
				DecreaseJob(0, tmp);
			}
		}

		public void SetConsume(List<List<int>> consume)
		{
			for (var i = 0; i < consume.Count; i ++)
				ScheduleList[i].Consume = new List<int>(consume[i]);
		}

		public void SetProduce(List<List<int>> produce)
		{
			for (var i = 0; i < produce.Count; i ++)
				ScheduleList[i].Produce = new List<int>(produce[i]);
		}
		
		private int Translate(string jobName)
		{
			for (var i = 0; i < ScheduleList.Count; i ++)
				if (ScheduleList[i].Name == jobName)
					return i;
			return -1;
		}

		public void IncreaseJob(int index, int increment = 1)
		{
			if (index == 0)
				ScheduleList[0].Increase(increment);
			else
			{
				var tmp = Tools.Min(increment, ScheduleList[0].Cnt);
				ScheduleList[0].Decrease(tmp);
				ScheduleList[index].Increase(tmp);
			}
		}
		
		public void DecreaseJob(int index, int decrement = 1)
		{
			var tmp = Tools.Min(decrement, ScheduleList[index].Cnt);
			if (index == 0)
				ScheduleList[0].Decrease(tmp);
			else
			{	
				ScheduleList[0].Increase(tmp);
				ScheduleList[index].Decrease(tmp);
			}
		}

		public void Clear()
		{
			for (var i = 1; i < ScheduleList.Count; i++)
			{
				ScheduleList[0].Increase(ScheduleList[i].Cnt);
				ScheduleList[i].Decrease(ScheduleList[i].Cnt);
			}
		}
	}

	public class Date
	{
		public int Cnt = 0;
		public int FireTimes = 0;
		public bool Forest = false, Outdoor = false;
	}
}