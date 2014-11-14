using UnityEngine;
using System.Collections;

public class FingerLineRenderer : MonoBehaviour {

	private LineRenderer lineRenderer;
	private float lineWidth = 0.1f;

	// Use this for initialization
	void Start () {
		print ("FingerLineRenderer.Start()");
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.SetWidth(lineWidth, lineWidth);
		lineRenderer.useWorldSpace = true;
		lineRenderer.SetVertexCount(2);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
