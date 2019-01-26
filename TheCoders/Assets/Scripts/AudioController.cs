using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
	[SerializeField] AudioSource m_audioSource;
	[SerializeField] List<AudioClip> m_earthClickSounds;
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

	public void ExplosionAudioSource()
	{
		if (!m_audioSource.isPlaying && m_earthSoundDelay <= 0.0f)
		{
			m_earthSoundDelay = 0.7f;
			var randomNumber = Random.Range(0, m_earthClickSounds.Count);
			m_audioSource.clip = m_earthClickSounds[randomNumber];
			m_audioSource.Play();
		}
	}

	private void Update()
	{
		if (m_earthSoundDelay > 0.0f)
		{
			m_earthSoundDelay -= Time.deltaTime;
		}
	}
}
