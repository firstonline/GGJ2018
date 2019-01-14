using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	// Update is called once per frame
	private void Start()
	{
		MovementHelper.Instance.MoveObject(this.gameObject, new Vector3(10.0f, 10.0f, 0.0f), 5.0f, OnDestinationDone);
		MovementHelper.Instance.FaceTarget(this.gameObject, new Vector3(10.0f, 10.0f, 0.0f), 5.0f, OnRotationDone);
	}

	private void OnDestinationDone()
	{
		Debug.Log("yayyy");
	}

	private void OnRotationDone()
	{
		Debug.Log("yayyy22");
	}
}
