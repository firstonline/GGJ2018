using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyParticleSystem : MonoBehaviour
{
	private ParticleSystem m_ps;

	private void Start()
	{
		m_ps = GetComponent<ParticleSystem>();
	}

	// Update is called once per frame
	void Update()
    {
		if (m_ps)
		{
			if (!m_ps.IsAlive())
			{
				Destroy(gameObject);
			}
		}
		else
		{
			Destroy(gameObject);
		}
    }
}
