using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace TutorialInfo.Scripts
{
	public class Popup : MonoBehaviour
	{
		private ResourceManager _resourceManager;
		public GameObject contentColumnTemplate;
		public GameObject optionButtonTemplate, optionColumnTemplate, optionButtonTemplateFalse;
		public GameObject titleBox, contentListBox, buttonListBox;

		private List<Button> _buttonList;

		private List<Universal> _list;

		private void Start()
		{
			if (!_resourceManager) 
				Init();
		}

		private void Init()
		{
			_resourceManager = FindObjectOfType<ResourceManager>();
			_buttonList = new List<Button>();
		}

		public void NewPopup(string title, List<string> content, List<Universal> buttonList)
		{
			if (!_resourceManager) 
				Init();
			_buttonList.Clear();
			_list = buttonList;
			
			titleBox.transform.GetComponent<TextMeshProUGUI>().text = title;

			for (var i = 0; i < contentListBox.transform.childCount; i ++)
				Destroy(contentListBox.transform.GetChild(i).gameObject);

			foreach (var i in content)
			{
				var columnTMP = Instantiate(contentColumnTemplate, contentListBox.transform, false);
				columnTMP.transform.GetComponent<TextMeshProUGUI>().text = i;
			}
			
			for (var i = 0; i < buttonListBox.transform.childCount; i ++)
				Destroy(buttonListBox.transform.GetChild(i).gameObject);

			var resource = FindObjectOfType<ResourceManager>().GetResourceList();
			
			foreach (var i in buttonList)
			{
				i.Available = FindObjectOfType<ResourceManager>().ResourceCheck(i.Consume) ? 1 : 0;
				
				var buttonTMP = Instantiate(i.Available == 1 ? optionButtonTemplate : optionButtonTemplateFalse, 
					buttonListBox.transform, false);
				
				buttonTMP.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = i.Name;
				
				var consume = i.Consume;
				Debug.Log(i.Consume.Count);
				for (var j = 0; j < consume.Count; j++)
				{
					if (consume[j] == 0)
						continue;
					var columnTMP = Instantiate(optionColumnTemplate, buttonTMP.transform.GetChild(1).transform, false);
					columnTMP.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Tools.ResourceName(j);
					columnTMP.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
						"<b>-" + consume[j] + "</b>";
				}
				
				var produce = i.Produce;
				Debug.Log(i.Produce.Count);
				for (var j = 0; j < produce.Count; j++)
				{
					if (produce[j] == 0)
						continue;
					var columnTMP = Instantiate(optionColumnTemplate, buttonTMP.transform.GetChild(1).transform, false);
					columnTMP.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Tools.ResourceName(j);
					columnTMP.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
						"<b>+" + produce[j] + "</b>";
				}
				
				var tmp = buttonTMP.GetComponent<Button>();
				tmp.onClick.AddListener(() => Click(tmp));
				
				_buttonList.Add(tmp);
			}
		}
		private void Click(Button button)
		{
			var index = _buttonList.IndexOf(button);
			FindObjectOfType<ResourceManager>().ResourceCost(_list[index].Consume);
			FindObjectOfType<ResourceManager>().ResourceGain(_list[index].Produce);
			FindObjectOfType<NewDay>().EventReceive(_buttonList.IndexOf(button));
			FindObjectOfType<SwitchRoom>().SelectRoomFire();
		}
	}
}