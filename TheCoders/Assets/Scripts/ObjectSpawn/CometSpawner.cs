using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometSpawner : MonoBehaviour
{
	//Comet prefab(s) to spawn
	[SerializeField]
	private GameObject[] CometPrefabs;

	//Object pool for comets
	private ObjectPooler CometObjectPool;

	void Awake()
	{
		CometObjectPool = new ObjectPooler(CometPrefabs);
	}

	// Spawn a comet with a given direction and speed
	public void SpawnComet( float Speed, int Health, int Damage )
	{
		Vector2 SpawnPoint = GameMode.instance.Arena.RandomArenaBorderPosition();
		Debug.Log("New Comet Spawned at: " + SpawnPoint);
		GameObject NewCometObj = CometObjectPool.GetNewObject();
		NewCometObj.transform.position = SpawnPoint;
		NewCometObj.SetActive(false);
		Comet CometComponent = NewCometObj.GetComponent<Comet>();
		CometComponent.Health = Health;
		CometComponent.Damage = Damage;
		CometComponent.MoveSpeed = Speed;

		//Get direction vector from Spawnpoint to Centre
		Vector2 Direction = (Vector2) (GameMode.instance.Planet.transform.position) - SpawnPoint;
		NewCometObj.SetActive(true);
		CometComponent.SetVelocity(Speed, Direction);
	}

	public int getCometPoolSize()
	{
		return CometObjectPool.Length();
	}

	public bool isAnyCometActive()
	{
		return CometObjectPool.HasActiveElements();
	}

	public int getActiveCount()
	{
		return CometObjectPool.CountActiveElements();
	}
}
