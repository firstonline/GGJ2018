using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Comet : MonoBehaviour
{
	// Population reduced when hitting earth
	public int Damage;
	public int MaxHealth;
	public float MoveSpeed;
	public Vector2 DirectionVector;

	[SerializeField] private GameObject m_explosionVFX;
	[SerializeField] private GameObject m_trailRenderer;

	private Rigidbody2D rbComponent;
	private int Health;


	[SerializeField] private GameObject m_healthBarFill;
	[SerializeField] private GameObject m_healthBarParent;

	// Called at construction
	void Awake()
	{
		//Get components
		rbComponent = gameObject.GetComponent<Rigidbody2D>();
	}

	private void OnEnable()
	{
		m_healthBarParent.gameObject.SetActive(false);
		Health = MaxHealth;
		m_healthBarFill.transform.localScale = new Vector3(1f, 1f, 1f);
		m_explosionVFX.SetActive(false);
		//m_trailRenderer.SetActive(true);
		m_trailRenderer.GetComponent<TrailRenderer>().emitting = true;
	}

	// Start is called before the first frame update
	void Start()
    {
		m_healthBarParent.SetActive(false);
		Health = MaxHealth;
		m_healthBarFill.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	public void SetVelocity( float Speed, Vector2 Direction )
	{
		rbComponent.velocity = (Direction * Speed);
		Vector2 vel = rbComponent.velocity;
		this.transform.up = -Direction;
	}

	public void TakeDamage( int Value )
	{
		Health -= Value;
		m_healthBarFill.transform.localScale = new Vector3(1f, ((float)Health) / ((float)MaxHealth), 1f);
		m_healthBarParent.SetActive(true);
		if ( Health <= 0 )
		{
			Die();
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Comet Overlap with other: " + other.tag);

		if (other.CompareTag("Planet"))
		{
			GameMode.Instance.GetPopController().ReducePopulation(Damage);
			Die();
		}
		else if (other.CompareTag("Rocket"))
		{
			Rocket rocket = other.gameObject.GetComponent<Rocket>();
			TakeDamage(rocket.Damage);
			other.gameObject.SetActive(false);
			var explosion = Instantiate(m_explosionVFX);
			explosion.transform.localScale *= 0.01f;
			var position = other.transform.position;
			position.z = -1;
			explosion.transform.position = position;
			explosion.gameObject.SetActive(true);
			AudioController.Instance.PlayRocketExplosionSound(other.transform.position);
		}
	}

	private void Die()
	{
		var explosion = Instantiate(m_explosionVFX);
		var position = transform.position;
		position.z = -1;
		explosion.transform.position = position;
		m_trailRenderer.GetComponent<TrailRenderer>().emitting = false;
		m_trailRenderer.GetComponent<TrailRenderer>().Clear();
		//m_trailRenderer.SetActive(false);
		explosion.gameObject.SetActive(true);
		gameObject.SetActive(false);
		AudioController.Instance.PlayEarthExplosionSound();
	}

	private void OnMouseDown()
	{
		if (Time.timeScale > 0.0f)
		{
			TakeDamage(GameMode.Instance.PlayerDamagePerClick);
		}
	}
}
