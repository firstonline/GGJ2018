using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Heart : MonoBehaviour
{
	[SerializeField] SpriteRenderer m_heartImage;
	[SerializeField] Sprite[] m_heartSprites;
	private float m_lifeTime;
	private float m_growSpeed;
	private Vector3 m_direction;
	private float m_flySpeed;

	// Use this for initialization
	public void Initialise(Vector3 position)
	{
		m_heartImage.sprite = m_heartSprites[Random.Range(0, m_heartSprites.Length)];
		m_direction.x = Random.Range(-1.0f, 1.0f);
		m_direction.y = Random.Range(-1.0f, 1.0f);
		m_direction.Normalize();
		m_lifeTime = Random.Range(0.5f, 1.0f);
		m_growSpeed = Random.Range(0.5f, 1.0f);
		m_flySpeed = Random.Range(0.1f, 0.5f);
		transform.localScale = Vector3.zero;
		position.z = -1;
		transform.position = position;
	}

	// Update is called once per frame
	void Update()
	{
		if (m_lifeTime > 0)
		{
			m_lifeTime -= Time.deltaTime;
			transform.position += m_direction * Time.deltaTime * m_flySpeed;
			var localScale = transform.localScale;
			float newScale = m_growSpeed * Time.deltaTime * m_growSpeed;
			localScale.x += newScale;
			localScale.y += newScale;
			localScale.z += newScale;
			transform.localScale = localScale;
		}
		else
		{
			gameObject.SetActive(false);
		}
	}
}
