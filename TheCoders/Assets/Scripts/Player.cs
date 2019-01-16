using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	// Update is called once per frame
	private void Start()
	{
		MovementHelper.Instance.MoveObject(this.gameObject, new Vector3(10.0f, 10.0f, 0.0f), 5.0f, OnDestinationDone);

	}

	private void OnDestinationDone()
	{
		MovementHelper.Instance.MoveObject(this.gameObject, new Vector3(-transform.position.x, -transform.position.y, 0.0f), 5.0f, OnDestinationDone);
	}
}
