using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
	public Heart m_heartPrefab;

	public int PopulationGainPerClick = 1;
	private Animator anim;
	private ObjectPooler m_pooler;
	private Camera m_camera;

	private void Start()
	{
		m_camera = Camera.main;
		anim = GetComponent<Animator>();
		m_pooler = new ObjectPooler(new GameObject[] { m_heartPrefab.gameObject });
	}

	//On click - do this
	private void OnMouseDown()
	{
		if (Time.timeScale > 0 && PopulationGainPerClick > 0 )
		{
			int spawnAmount = Random.Range(1, 4);
			for (int i = 0; i < spawnAmount; i++)
			{
				var heart = m_pooler.GetNewObject().GetComponent<Heart>();
				heart.Initialise(m_camera.ScreenToWorldPoint(Input.mousePosition));
			}

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
