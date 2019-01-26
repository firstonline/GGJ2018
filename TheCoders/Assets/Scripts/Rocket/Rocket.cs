using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rocket : MonoBehaviour
{
	[SerializeField] private float m_moveSpeed;
	[SerializeField] private int m_damage;
	[SerializeField] private SpriteRenderer m_rocketImage;
	[SerializeField] private float m_rotateSpeed;
	[SerializeField] private float m_angleDampTime;
	private float m_timePast;
	private float m_lifeTime = 5.0f;
	private GameObject m_target;
	private Vector3 m_dir;
	private Quaternion m_originalAngle;
	private float m_velocity = 0.0f;

	// Start is called before the first frame update
	public void Initialise(GameObject target, int damage, Sprite rocketSprite)
    {
		m_damage = damage;
		if (rocketSprite != null)
		{
			m_rocketImage.sprite = rocketSprite;
		}
		m_target = target;
		m_originalAngle = transform.rotation;
	}

    // Update is called once per frame
    void Update()
    {
		if (m_target != null)
		{
			m_dir = m_target.transform.position - this.transform.position;
			m_timePast += Time.deltaTime;
			float angle = -Vector3.SignedAngle(transform.up, m_dir, Vector3.up);
			var clampedTime = 1 - Mathf.Clamp(m_timePast / m_angleDampTime, 0.95f, 1f);
			float modifiedAngle = Mathf.SmoothDampAngle(m_originalAngle.eulerAngles.z, angle, ref m_velocity, clampedTime);

			var currentRotation = transform.rotation;
			Debug.Log(modifiedAngle + ",m");
			currentRotation.eulerAngles += new Vector3(0.0f, 0.0f, modifiedAngle * Time.deltaTime * m_rotateSpeed);
			transform.rotation = currentRotation;
			transform.position += transform.up  * m_moveSpeed * Time.deltaTime;
			if (transform.position == m_target.transform.position)
			{
				Destroy(this.gameObject); 
			}
		}
		else
		{
			transform.position += m_dir * m_moveSpeed * Time.deltaTime;
		}

		if (m_lifeTime > 0)
		{
			m_lifeTime -= Time.deltaTime;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		
	}
}
