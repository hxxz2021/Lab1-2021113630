using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TutorialInfo.Scripts
{
	public static class GameMessage
	{
		public static readonly List<List<string>> BuildMessage = new()
		{
			new List<string>() // cart
			{
				"ERROR",
				"摇摇晃晃的货车满载从森林运出的木头"
			},
			new List<string>() // trap
			{
				"再增加陷阱已毫无裨益",
				"陷阱越多，抓到的猎物就越多."
			},
			new List<string>() // hut
			{
				"已经没有可以建小屋的空地了",
				"黎星在林中建起一座小屋，她说消息很快就会流传出去",
				"可能有无家可归的人会来这里定居"
			},
			new List<string>() // hostel
			{
				"ERROR",
				"黎星在森林边缘建起了一座旅馆，她说会有在大陆上云游的人前来借宿",
				"她沉默了一下，或许，会见到一些本应只能在史诗中听见的名字"
			},
			new List<string>() // trading post
			{
				"ERROR.",
				"黎星在森林边缘建起了一座贸易站，她说游牧商队可能会来进行贸易",
				"她歪着头想了想，好像也有心怀歹意的人伪装成商队"
			},
			new List<string>() // leather house
			{
				"ERROR.",
				"黎星在村中建起了一座制革屋",
			},
			new List<string>() // bacon house
			{
				"ERROR.",
				"黎星在村中建起了一座熏肉房",
			},
			new List<string>() // workshop
			{
				"ERROR.",
				"黎星在村中建起了一座工坊",
				
			},
			new List<string>() // crystal workshop
			{
				"ERROR.",
				"黎星在村中建起了一座水晶工坊"
			}
		};

		public static readonly List<string> JobUnlockMessage = new()
		{
			"村民的默认职业为采集者",
			"现在可以指派村民的职业为猎人了",
			"现在可以指派村民的职业为制革师了",
			"现在可以指派村民的职业为熏肉师了",
			"现在可以指派村民的职业为铁矿工人了",
			"现在可以指派村民的职业为水晶工匠了"
		};

		public static readonly List<string> FireMessage = new()
		{
			"木头不够了.",
			"火堆冒出火苗.",
			"火堆燃烧着.",
			"火堆熊熊燃烧."
		};

		public static readonly List<string> MakeFireMessage = new()
		{
			"房间很宜人",
			"房间暖和",
			"房间很热"
		};

		public static readonly List<string> LogMessage = new()
		{
			"林地上散落着枯枝败叶."
		};

		public static readonly List<string> GainTrapMessage = new()
		{
			"尚未建造陷阱",
			"陷阱捕获到皮毛碎片，小块肉",
			"陷阱捕获到皮毛碎片，小块肉和水晶碎片",
			"陷阱捕获到皮毛碎片，小块肉，水晶碎片和符文碎片"
		};

		public static readonly string ShortageMessage = "材料不够了.";

		public static readonly List<string> VillagerIncreaseMessage = new()
		{
			"一位流浪者在夜里抵达，他迫不及待的决定在这里定居",
			"一户人家在村外驻足的许久，最终还是坚定的走进了村中",
			"一支车队伴随着轻松的氛围驶来，他们决定在这里停留一段时间"
		};

		public static readonly List<string> InitMessage = new()
		{
			"破旧的木屋在黑夜中摇摇欲坠",
			"一道白光从北极星落下，倒塌的木屋又恢复了原状",
			"一片银白洒下，雪后的木屋焕然一新"
		};
	}
}