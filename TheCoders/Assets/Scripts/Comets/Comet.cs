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
	}

	// Start is called before the first frame update
	void Start()
    {
		//rbComponent.velocity = (DirectionVector * MoveSpeed);
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
			gameObject.SetActive(false);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Comet Overlap with other: " + other.tag);
		if (other.CompareTag("Planet"))
		{
			GameMode.Instance.GetPopController().ReducePopulation(Damage);
			gameObject.SetActive(false);
		}
	}

	private void OnMouseDown()
	{
		TakeDamage(GameMode.Instance.PlayerDamagePerClick);
	}
}
