using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityEngineHelper : MonoBehaviour
{
    public void TransformPosition(GameObject go, int x, int y, int z)
	{
		TransformPosition(go.transform, x, y, z);
	}

	public void TransformPosition(Transform tr, int x, int y, int z)
	{
		var newPosition = tr.position;
		newPosition.x += x;
		newPosition.y += y;
		newPosition.z += z;
		tr.position = newPosition;
	}

	public void TansformLocalScale(GameObject go, int x, int y, int z)
	{
		TansformLocalScale(go.transform, x, y, z);
	}

	public void TansformLocalScale(Transform tr, int x, int y, int z)
	{
		var newLocalScale = tr.localScale;
		newLocalScale.x += x;
		newLocalScale.y += y;
		newLocalScale.z += z;
		tr.localScale = newLocalScale;
	}

	public void TransformAngle(GameObject go, int x, int y, int z)
	{
		TransformAngle(go.transform, x, y, z);
	}

	public void TransformAngle(Transform tr, int x, int y, int z)
	{
		var newAngle = tr.localEulerAngles;
		newAngle.x += x;
		newAngle.y += y;
		newAngle.z += z;
		tr.localEulerAngles = newAngle;
	}
}
