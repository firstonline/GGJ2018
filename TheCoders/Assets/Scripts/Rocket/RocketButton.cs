using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RocketButton : MonoBehaviour
{
	[SerializeField] private Button m_button;

	public void Initialise(UnityAction onClickAction)
	{
		m_button.onClick.AddListener(onClickAction);
	}
}
