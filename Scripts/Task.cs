using System;
using UnityEngine;

namespace TutorialInfo.Scripts
{
	public class Task : MonoBehaviour
	{
		private void Awake()
		{
			DontDestroyOnLoad(gameObject);
			
		}

		public void TaskCheck()
		{
			
		}
	}
}