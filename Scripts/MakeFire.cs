using System;
using UnityEngine;

namespace TutorialInfo.Scripts
{
	public class MakeFire : MonoBehaviour
	{
		private int _cnt;
		public double delayTime = 5;
		private double _timer;

		private Message _message;

		private void Start()
		{
			_message = FindObjectOfType<Message>();
		}

		private void Update()
		{
			if (_timer <= 0)
				return;
			_timer -= Time.deltaTime;
			if (_timer >= 0)
				return;
			_message.Add(GameMessage.MakeFireMessage[_cnt - 1 <= 2 ? _cnt - 1 : 2]);
		}

		public void Click(int cnt)
		{
			_timer = delayTime;
			_cnt = cnt;
		}
	}
}