﻿using System.Collections;
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
		if ( PopulationGainPerClick > 0 )
		{
			GameMode.Instance.GetPopController().AddPopulation(PopulationGainPerClick);
			if (null != anim)
			{
				// play Bounce but start at a quarter of the way though
				anim.Play("PlanetClickAnim", 0, 0.0f);
			}
			AudioController.Instance.PlayEarthClickSound();
		}
	}
}
