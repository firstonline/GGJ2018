using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comet : MonoBehaviour
{
	// Population reduced when hitting earth
	public int Damage;
	public int Health;
	public float MoveSpeed;
	public Vector2 DirectionVector;

	[SerializeField] private GameObject m_explosionVFX;
	[SerializeField] private GameObject m_trailRenderer;

	private Rigidbody2D rbComponent;

	// Called at construction
	void Awake()
	{
		//Get components
		rbComponent = gameObject.GetComponent<Rigidbody2D>();
	}

	private void OnEnable()
	{
		m_explosionVFX.SetActive(false);
		m_trailRenderer.SetActive(true);
	}

	// Start is called before the first frame update
	void Start()
    {
		//rbComponent.velocity = (DirectionVector * MoveSpeed);
    }

	public void SetVelocity( float Speed, Vector2 Direction )
	{
		rbComponent.velocity = (Direction * Speed);
		Vector2 vel = rbComponent.velocity;
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void TakeDamage( int Value )
	{
		Health -= Value;
		if ( Health <= 0 )
		{
			GameMode.Instance.GetPopController().ReducePopulation(Damage);
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
			var explosion = Instantiate(m_explosionVFX);
			explosion.transform.localScale *= 0.01f;
			var position = other.transform.position;
			position.z = -1;
			explosion.transform.position = position;
			explosion.gameObject.SetActive(true);
		}
	}

	private void Die()
	{
		var explosion = Instantiate(m_explosionVFX);
		var position = this.transform.position;
		position.z = -1;
		explosion.transform.position = position;
		m_trailRenderer.SetActive(false);
		explosion.gameObject.SetActive(true);
		gameObject.SetActive(false);
	}
}
