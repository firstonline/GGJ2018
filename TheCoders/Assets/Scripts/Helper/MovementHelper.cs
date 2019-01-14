using UnityEngine;
using System;
using System.Collections.Generic;

public class MovementHelper : MonoBehaviour
{
	private class Movement
	{
		public Transform Transform { get; set; }
		public Vector3 TargetPosition { get; set; }
		public Vector3 TargetDirection { get; set; }
		public float MoveSpeed { get; set; }
		public float RoatationSpeed { get; set; }
		public Action OnPositionDoneCallback { get; set; }
		public Action OnRatationDoneCallback { get; set; }
	}

	public static MovementHelper Instance
	{
		get { return ms_movementHelper; }
	}

	private Dictionary<int, Movement> m_movingObjects;
	private List<int> m_keysToRemove;
	private static MovementHelper ms_movementHelper;

	private void Awake()
	{
		// TODO: add check for multiple instances
		ms_movementHelper = this;
		m_movingObjects = new Dictionary<int, Movement>();
		m_keysToRemove = new List<int>(); 
	}

	public void MoveObject(GameObject go, Vector3 targetPosition, float speed, Action callback)
	{
		MoveObject(go.transform, targetPosition, speed, callback);
	}

	public void MoveObject(Transform tr, Vector3 targetPosition, float speed, Action callback)
	{
		int hashCode = tr.GetHashCode();
		if (m_movingObjects.ContainsKey(hashCode))
		{
			var movingObject = m_movingObjects[hashCode];
			movingObject.TargetPosition = targetPosition;
			movingObject.MoveSpeed = speed;
			movingObject.OnPositionDoneCallback = callback;
		}
		else
		{
			var movement = new Movement
			{
				Transform = tr,
				TargetPosition = targetPosition,
				MoveSpeed = speed,
				OnPositionDoneCallback = callback,
			};
			m_movingObjects.Add(hashCode, movement);
		}
	}

	public void FaceTarget(GameObject go, Vector3 targetPosition, float speed, Action callback)
	{
		FaceTarget(go.transform, targetPosition, speed, callback);
	}

	public void FaceTarget(Transform tr, Vector3 targetPosition, float speed, Action callback)
	{
		int hashCode = tr.GetHashCode();
		if (m_movingObjects.ContainsKey(hashCode))
		{
			var movingObject = m_movingObjects[hashCode];
			movingObject.TargetDirection = (targetPosition - tr.position).normalized;
			movingObject.RoatationSpeed = speed;
			movingObject.OnRatationDoneCallback = callback;
		}
		else
		{
			var movement = new Movement
			{
				Transform = tr,
				TargetDirection = (targetPosition - tr.position).normalized,
				RoatationSpeed = speed,
				OnRatationDoneCallback = callback,
			};
			m_movingObjects.Add(hashCode, movement);
		}
	}

	private void Update()
	{
		var keys = m_movingObjects.Keys;
		bool positionDone;
		bool rotationDone;
		foreach (var key in keys)
		{
			positionDone = false;
			rotationDone = false;
			// Modify position
			var movingObject = m_movingObjects[key];
			if (movingObject.MoveSpeed > 0.0f)
			{
				var step = movingObject.MoveSpeed * Time.deltaTime;
				var tr = movingObject.Transform;
				tr.position = Vector3.MoveTowards(tr.position, movingObject.TargetPosition, step);
				if (tr.position == movingObject.TargetPosition)
				{
					positionDone = true;
					movingObject.MoveSpeed = 0;
					if (movingObject.OnPositionDoneCallback != null)
					{
						movingObject.OnPositionDoneCallback();
					}
				}
			}
			else
			{
				positionDone = true;
			}

			// Modify rotation
			if (movingObject.RoatationSpeed > 0.0f)
			{
				var step = movingObject.RoatationSpeed * Time.deltaTime;
				var tr = movingObject.Transform;
				tr.forward = Vector3.RotateTowards(tr.forward, movingObject.TargetDirection, step, 0.0f);
				if (tr.forward == movingObject.TargetDirection)
				{
					rotationDone = true;
					movingObject.RoatationSpeed = 0;
					if (movingObject.OnRatationDoneCallback != null)
					{
						movingObject.OnRatationDoneCallback();
					}
				}
			}
			else
			{
				rotationDone = true;
			}

			if (positionDone && rotationDone)
			{
				m_keysToRemove.Add(key);
			}
		}

		foreach (int key in m_keysToRemove)
		{
			m_movingObjects.Remove(key);
		}
		m_keysToRemove.Clear();
	}
}
