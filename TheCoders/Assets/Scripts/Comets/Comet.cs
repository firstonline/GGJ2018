﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comet : MonoBehaviour
{
	// Population reduced when hitting earth
	public int Damage;
	public int Health;
	public float MoveSpeed;
	public Vector2 DirectionVector;
	private Rigidbody2D rbComponent;

	// Called at construction
	void Awake()
	{
		//Get components
		rbComponent = gameObject.GetComponent<Rigidbody2D>();
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
}
