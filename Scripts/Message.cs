using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace TutorialInfo.Scripts
{
	public class Message : MonoBehaviour
	{
		public GameObject messageTemplate, messagePanel;
		public List<GameObject> messageList = new();
		private const int MaxMessageCount = 50;
		
		private void Clear()
		{
			foreach (var i in messageList)
				Destroy(i);
			messageList.Clear();
		}

		public void Add(string message)
		{
			if (message == "clear")
			{
				Clear();
				return;
			}
			if (messageList.Count == MaxMessageCount)
			{
				Destroy(messageList[0]);
				messageList.RemoveAt(0);
			}
			var tmp = Instantiate(messageTemplate, messagePanel.transform, false);
			tmp.GetComponent<TextMeshProUGUI>().text = message;
			messageList.Add(tmp);
		}
	}
}