using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
	[SerializeField] AudioSource m_audioSource;
	[SerializeField] AudioSource m_rocketExplosionSource;
	[SerializeField] AudioSource m_rocketFlySource;
	[SerializeField] AudioSource m_earthExplosionSource;
	[SerializeField] List<AudioClip> m_earthClickSounds;
	[SerializeField] List<AudioClip> m_explosionSounds;
	[SerializeField] AudioClip m_flySound;
	[SerializeField] AudioClip m_meteorDestroyed;
	private float m_earthSoundDelay;

	public static AudioController Instance
	{
		get
		{
			if (ms_instance == null)
			{
				ms_instance = FindObjectOfType<AudioController>();
			}
			return ms_instance;
		}
	}

	private static AudioController ms_instance;

	private void Awake()
	{
		if (ms_instance == null)
		{
			ms_instance = this;
		}
	}

	public void PlayEarthClickSound()
	{
		if (!m_audioSource.isPlaying && m_earthSoundDelay <= 0.0f)
		{
			m_earthSoundDelay = 0.7f;
			var randomNumber = Random.Range(0, m_earthClickSounds.Count);
			m_audioSource.clip = m_earthClickSounds[randomNumber];
			m_audioSource.Play();
		}
	}

	public void PlayEarthExplosionSound()
	{
		m_earthSoundDelay = 0.7f;
		m_earthExplosionSource.clip = m_explosionSounds[0];
		m_earthExplosionSource.Play();
	}

	public void PlayRocketExplosionSound(Vector3 position)
	{
		// no rockets left
		if (m_rocketFlySource.isPlaying && RocketsManager.Instance.GetActiveRocketsCount() == 0)
		{
			m_rocketFlySource.Stop();
		}
		m_rocketExplosionSource.transform.position = position;
		m_rocketExplosionSource.clip = m_explosionSounds[1];
		m_rocketExplosionSource.Play();
	}

	public void PlayRocketFlySound()
	{
		m_rocketFlySource.clip = m_flySound;
		m_rocketFlySource.Play();
	}
	
	public void MeteoriteDestroyedSound()
	{
		m_audioSource.clip = m_meteorDestroyed;
		m_audioSource.Play();
	}

	private void Update()
	{
		if (m_earthSoundDelay > 0.0f)
		{
			m_earthSoundDelay -= Time.deltaTime;
		}
	}
}
