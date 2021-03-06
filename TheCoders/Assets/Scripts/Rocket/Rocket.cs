﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rocket : MonoBehaviour
{
	public int Damage { get { return m_damage; } }

	[SerializeField] private float m_moveSpeed;
	[SerializeField] private int m_damage;
	[SerializeField] private SpriteRenderer m_rocketImage;
	[SerializeField] private float m_rotateSpeed;
	[SerializeField] private float m_angleDampTime;

	private float m_timePast;
	private float m_lifeTime = 5.0f;
	private float m_lifeTimeLeft;
	private GameObject m_target;
	private Vector3 m_dir;
	private Quaternion m_originalAngle;
	private float m_velocity = 0.0f;

	public void OnEnable()
	{
		AudioController.Instance.PlayRocketFlySound();
	}

	// Start is called before the first frame update
	public void Initialise(GameObject target, int damage, Sprite rocketSprite, Vector3 initPosition)
    {
		m_damage = damage;
		if (rocketSprite != null)
		{
			m_rocketImage.sprite = rocketSprite;
		}
		m_target = target;
		var rotation = transform.rotation;
		transform.rotation = rotation;
		transform.position = initPosition;
		m_lifeTimeLeft = m_lifeTime;
		m_dir = m_target.transform.position - this.transform.position;
		m_dir.Normalize();
		float angle = Vector3.SignedAngle(transform.up, m_dir, Vector3.forward);
		var random = Random.Range(angle - 20, angle + 20);
		var currentRotation = transform.rotation;
		currentRotation.eulerAngles = new Vector3(0.0f, 0.0f, random);
		transform.rotation = currentRotation;
		m_originalAngle = transform.rotation;
	}

	// Update is called once per frame
	void Update()
    {
		if (m_target != null)
		{
			if (m_target.activeSelf)
			{
				m_dir = m_target.transform.position - this.transform.position;
				m_timePast += Time.deltaTime;

				float angle = Vector3.SignedAngle(transform.up, m_dir, Vector3.forward);
				var clampedtime = 1 - Mathf.Clamp(m_timePast / m_angleDampTime, 0.5f, 1f);
				float modifiedAngle = Mathf.SmoothDampAngle(0, angle, ref m_velocity, clampedtime);
				var currentrotation = transform.rotation;

				currentrotation.eulerAngles += new Vector3(0.0f, 0.0f, modifiedAngle * Time.deltaTime * m_rotateSpeed);
				transform.rotation = currentrotation;
				transform.position += transform.up * m_moveSpeed * Time.deltaTime;
				if (transform.position == m_target.transform.position)
				{
					this.gameObject.SetActive(false);
				}
			}
			else
			{
				m_dir.Normalize();
				m_target = null;
			}
		}
		else
		{
			transform.position += m_dir * m_moveSpeed * Time.deltaTime;
		}


		if (m_lifeTimeLeft > 0)
		{
			m_lifeTimeLeft -= Time.deltaTime;
		}
		else
		{
			gameObject.SetActive(false);
		}
	}
}
