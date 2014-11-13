using UnityEngine;
using System.Collections;

public class LineTest : MonoBehaviour
{
	public SphereLines sphereLines = null;

	void Start () 
	{
		sphereLines.Add(Vector3.right);
		sphereLines.Add(Vector3.forward + Vector3.up);
		sphereLines.Add(Vector3.forward);
		sphereLines.Add(Vector3.right);
	}
}
