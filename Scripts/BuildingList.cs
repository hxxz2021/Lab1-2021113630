using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Object = System.Object;

namespace TutorialInfo.Scripts
{
	public class BuildingList : MonoBehaviour
	{
		private ResourceManager _resourceManager;
		public GameObject optionButtonTemplate, optionColumnTemplate, optionButtonTemplateFalse;
		private List<Button> _buttonList;
		private List<int> _indexList;

		private List<Building> _craftList;
		
		private void Start()
		{
			if (!_resourceManager) 
				Init();
		}

		private void Init()
		{
			_resourceManager = FindObjectOfType<ResourceManager>();
			_buttonList = new List<Button>();
			_indexList = new List<int>();
		}
		
		public void Refresh()
		{
			if (!_resourceManager)
				Init();
			
			_craftList = _resourceManager.GetCraftList();
			
			_buttonList.Clear();
			_indexList.Clear();
			
			for (var i = 0; i < gameObject.transform.childCount; i ++)
				Destroy(gameObject.transform.GetChild(i).gameObject);
			
			foreach (var i in _craftList.Where(i => i.Available != 0))
			{
				var buttonTMP = Instantiate(i.Remains != 0 ? optionButtonTemplate : optionButtonTemplateFalse, 
					gameObject.transform, false);

				buttonTMP.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Tools.BuildingName(_craftList.IndexOf(i));
				var costList = i.GetPrice;
				for (var j = 0; j < costList.Count; j++)
				{
					if (costList[j] == 0)
						continue;
					var columnTMP = Instantiate(optionColumnTemplate, buttonTMP.transform.GetChild(1).transform, false);
					columnTMP.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Tools.ResourceName(j);
					columnTMP.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = costList[j].ToString();
				}

				var tmp = buttonTMP.GetComponent<Button>();
				tmp.onClick.AddListener(() => Click(tmp));
				
				_buttonList.Add(tmp);
				_indexList.Add(_craftList.IndexOf(i));
			}
		}

		private void Click(Button button)
		{
			var index = _buttonList.IndexOf(button);
			_resourceManager.Build(_indexList[index]);
			Refresh();
		}
	}
}