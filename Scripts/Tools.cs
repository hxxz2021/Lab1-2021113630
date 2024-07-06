using System.Collections.Generic;
using System;
using UnityEngine;

namespace TutorialInfo.Scripts
{
	public class Tuple<TL, TR>
	{
		public TL L;
		public TR R;
		
		public Tuple(TL lInit, TR rInit)
		{
			L = lInit;
			R = rInit;
		}
	}

	public class Triple<TL, TM, TR>
	{
		public TL L;
		public TM M;
		public TR R;

		public Triple(TL lInit, TM mInit, TR rInit)
		{
			L = lInit;
			M = mInit;
			R = rInit;
		}
	}
	
	public static class Tools
	{
		public static int Min(int l, int r)
		{
			return l < r ? l : r;
		}

		public static int Max(int l, int r)
		{
			return l > r ? l : r;
		}

		public static int Abs(int x)
		{
			return x > 0 ? x : -x;
		}

		public static int LogDate(Date date)
		{
			if (date.Cnt <= 20)
				return 1;
			return (int)Math.Log(date.Cnt - 20) + 1;
		}
		
		public static readonly List<string> Resource = new()
		{
			"木头",
			"皮毛",
			"肉",
			"皮革",
			"熏肉",
			"铁",
			"水晶",
			"符文碎片"
		};
		
		public static string ResourceName(int index) { return Resource[index]; }

		public static readonly List<Tuple<string, int>> Building = new()
		{
			new Tuple<string, int>("货车", 1),
			new Tuple<string, int>("陷阱", 10),
			new Tuple<string, int>("小屋", 20),
			new Tuple<string, int>("旅馆", 1),
			new Tuple<string, int>("贸易站", 1),
			new Tuple<string, int>("制革屋", 1),
			new Tuple<string, int>("熏肉房", 1),
			new Tuple<string, int>("工坊", 1),
			new Tuple<string, int>("水晶工坊", 1)
		};

		public static readonly List<Tuple<List<int>, List<int>>> BuildingCost = new()
		{
			new Tuple<List<int>, List<int>>(new List<int>{ 50 }, new List<int>()),
			new Tuple<List<int>, List<int>>(new List<int>{ 25 }, new List<int>{ 25 }),
			new Tuple<List<int>, List<int>>(new List<int>{ 100 }, new List<int>{ 100 }),
			new Tuple<List<int>, List<int>>(new List<int>{ 1000, 0, 0, 500, 500, 0, 1 }, new List<int>()),
			new Tuple<List<int>, List<int>>(new List<int>{ 1000, 0, 0, 500, 500, 0, 1 }, new List<int>()),
			new Tuple<List<int>, List<int>>(new List<int>{ 1000, 500, 250 }, new List<int>()),
			new Tuple<List<int>, List<int>>(new List<int>{ 1000, 250, 500 }, new List<int>()),
			new Tuple<List<int>, List<int>>(new List<int>{ 2000, 0, 0, 1000, 1000, 0, 1 }, new List<int>()),
			new Tuple<List<int>, List<int>>(new List<int>{ 10000, 0, 0, 1000, 1000, 100, 100}, new List<int>())
		};
		
		public static string BuildingName(int index) { return Building[index].L; }
		
		public static readonly List<string> Job = new()
		{
			"采集者",
			"猎人",
			"制革师",
			"熏肉师",
			"铁矿工人",
			"水晶工匠"
		};
		
		public static string JobName(int index) { return Job[index]; }

		public static readonly List<List<int>> JobConsume = new()
		{
			new List<int>(),
			new List<int>(),
			new List<int> { 0, 5 },
			new List<int> { 5, 0, 5 },
			new List<int> { 0, 0, 0, 0, 5 },
			new List<int> { 0, 0, 0, 0, 10 }
		};
		
		public static readonly List<List<int>> JobProduce = new()
		{
			new List<int> { 2 },
			new List<int> { 0, 1, 1 },
			new List<int> { 0, 0, 0, 1 },
			new List<int> { 0, 0, 0, 0, 1 },
			new List<int> { 0, 0, 0, 0, 0, 1},
			new List<int> { 0, 0, 0, 0, 0, 0, 1}
		};
	}

	public static class DateCheck
	{
		
	}
}