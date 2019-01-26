using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
	public int PopulationGainPerClick;

	//On click - do this
	private void OnMouseDown()
	{
		GameMode.Instance.GetPopController().AddPopulation(PopulationGainPerClick);
	}
}
