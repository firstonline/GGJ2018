using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
	public int PopulationGainPerClick = 1;
	private Animator anim;

	private void Start()
	{
		anim = GetComponent<Animator>();
	}

	//On click - do this
	private void OnMouseDown()
	{
		GameMode.Instance.GetPopController().AddPopulation(PopulationGainPerClick);
		if (null != anim)
		{
			// play Bounce but start at a quarter of the way though
			anim.Play("PlanetClicked", 0, 0.0f);
		}
	}
}
