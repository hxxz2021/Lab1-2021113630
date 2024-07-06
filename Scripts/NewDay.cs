using System;
using System.Collections.Generic;
using UnityEngine;

namespace TutorialInfo.Scripts
{
	public class NewDay : MonoBehaviour
	{
		private void Awake()
		{
			DontDestroyOnLoad(gameObject);
		}

		private int _date;
		
		// ReSharper disable once FieldCanBeMadeReadOnly.Local
		private Tuple<int, int> _sign = new(0, 0);

		private static readonly Triple<string, List<string>, List<Universal>> Day1 = new(
			"小黑屋",
			new List<string>
			{
				"清夜无尘，月色如银",
				"本应寂静如常的森林中，突然传来一阵响声，深处一座破旧的木屋里，随月捂着脑袋，借着从门外照进来的月光，迷茫地打量着周围的环境",
				"然而很快他便发现，整个屋中，除去现在身下垫着的这张毯子，他只看见门口的角落里一捆干柴",
				"“……”",
				"随月扶着额头，沉吟了两秒：“这开局难度……这么高的吗”",
				"像是回应随月的话，他的背后又传来一阵响声。随月回过头去，一座书架凭空出现在另一个角落里",
				"随月木然的望着书架，沉默了一会：“……？”",
				"他活动了一下身子，撑着地板慢慢站了起来，抱了几根柴火到一旁点着，"
			},
			new List<Universal>
			{
				new("就没人怀疑木屋里为什么有柴火吗", 0, new List<int>{ 1 }),
				new("这是奇幻世界，先添柴生个火试试", 0, new List<int>{ 1 })
			}
			);

		private static readonly Triple<string, List<string>, List<Universal>> Day2 = new(
			"陌生的少女",
			new List<string>
			{
				"月出惊山鸟，时鸣春涧中",
				"随月盘腿坐在火堆旁边，他偏过头朝门外望去，漫无边际的苍翠之上悬挂着一抹银白",
				"夜色渐深，随月打了个哈欠，靠在墙上，思考着自己是怎么来到这个世界的，但他的眼皮已经耷拉了下来，没过一会，他便头靠在肩上，渐渐睡了过去",
				"……",
				"夜半时分，随月突然被门口的响声惊醒，他迷糊的睁开双眼，门外一个瘦弱的少女正一步一步的往前挪着",
				"随月揉了揉眼睛想看的更清楚一些，但还没等他抬起头，门外的少女就身子一歪，摔倒在了地上",
				"“……”",
				"随月迟疑了一下，先是将头探出门外观察了一会，确定没有什么异样后，小心翼翼地走到少女旁边",
				"他伸出手，本想试探着戳一下少女的脸，但是在半空中又把手缩了回来，沉吟了两秒：",
				"“先说好了，可不是我把你推到的，等会你醒了可不准赖上我”",
				"见身前的少女半天没有动静，随月这才放下心来，低着头仔细打量着",
				"少女的身旁有一张皱巴巴的纸条，应该是摔倒时掉下来的，随月把它捡起来，抖落掉粘在上面的泥土，上面歪歪斜斜的写着“黎星”",
				"随月把纸条装进口袋，然后把少女背起来往屋里走去，边走边用只有自己听得见的声音说：",
				"“在我这住下来可不准当闲人啊，明天我想想给你安排去干点什么活……我刚来这里也人生地不熟的，这还带着个人……”",
				"而这时，在随月的背后，少女的嘴角悄悄扬起……"
			},
			new List<Universal>
			{
				new("身为主角居然这么没有安全意识吗"),
				new("这样让一个陌生人进屋真的合理吗"),
				new("不管了，但是少女应该就是女主吧")
			}
		);

		private static readonly Triple<string, List<string>, List<Universal>> Day20 = new(
			"信使",
			new List<string>()
			{
				""
			},
			new List<Universal>()
			{
				
			}
		);

		private static Dictionary<int, Triple<string, List<string>, List<Universal>>> DayList;
		
		private void Start()
		{
			DayList = new Dictionary<int, Triple<string, List<string>, List<Universal>>>
			{
				{ 1, Day1 },
				{ 2, Day2 },
				{ 20, Day20 }
			};
		}

		private void EventSelectDay(int date, Triple<string, List<string>, List<Universal>> tmp)
		{
			_sign.L = 0;
			_sign.R = date;
			FindObjectOfType<SwitchRoom>().SelectPopUp();
			FindObjectOfType<Popup>().NewPopup(tmp.L, tmp.M, tmp.R);
		}

		private void EventReceiveDay(int tmp)
		{
			switch (_sign.R)
			{
				case 1:
					FindObjectOfType<ResourceManager>().MakeFire();
					FindObjectOfType<ResourceManager>().ResourceGain(new List<int>{ 1 });
					break;
				case 2:
					break;
				case 20:
					break;
			}
		}

		public void EventReceive(int tmp)
		{
			switch (_sign.L)
			{
				case 0:
					EventReceiveDay(tmp);
					break;
			}
			
			if (_date == 2)
				FindObjectOfType<SwitchRoom>().RoomSecondDay();
		}
		
		public void EventSelect(Date date)
		{
			_date = date.Cnt;
			_sign.L = _sign.R = -1;
			if (DayList.ContainsKey(date.Cnt))
			{
				EventSelectDay(date.Cnt, DayList[date.Cnt]);
				return;
			}
		}
	}
}