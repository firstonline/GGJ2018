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

	[SerializeField]
	private Slider HealthBar;
	[SerializeField]
	private Canvas HealthCanvas;

	// Called at construction
	void Awake()
	{
		//Get components
		rbComponent = gameObject.GetComponent<Rigidbody2D>();
	}

	private void OnEnable()
	{
		HealthCanvas.enabled = false;
		Health = MaxHealth;
		HealthBar.value = 1.0f;
		m_explosionVFX.SetActive(false);
		//m_trailRenderer.SetActive(true);
		m_trailRenderer.GetComponent<TrailRenderer>().emitting = true;
	}

	// Start is called before the first frame update
	void Start()
    {
		HealthCanvas.enabled = false;
		Health = MaxHealth;
		HealthBar.value = 1.0f;
	}

	public void SetVelocity( float Speed, Vector2 Direction )
	{
		rbComponent.velocity = (Direction * Speed);
		Vector2 vel = rbComponent.velocity;
	}

	public void TakeDamage( int Value )
	{
		Health -= Value;
		HealthBar.value = ((float)Health) / ((float)MaxHealth);
		HealthCanvas.enabled = true;
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
			AudioController.Instance.PlayEarthExplosionSound();
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
	}

	private void OnMouseDown()
	{
		TakeDamage(GameMode.Instance.PlayerDamagePerClick);
	}
}
