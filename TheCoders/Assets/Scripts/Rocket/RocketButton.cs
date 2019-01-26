using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RocketButton : MonoBehaviour
{
	[SerializeField] private Button m_button;
	[SerializeField] private TextMeshProUGUI m_humanCost;

	public void Initialise(UnityAction onClickAction, int humanCost)
	{
		m_button.onClick.AddListener(onClickAction);
		m_humanCost.text = humanCost.ToString();
	}
}
