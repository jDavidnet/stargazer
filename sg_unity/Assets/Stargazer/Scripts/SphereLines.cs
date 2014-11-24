using UnityEngine;
using System.Collections.Generic;

public class SphereLines : MonoBehaviour 
{
	public Material material;

	public int Count
	{
		get
		{
			return points.Count;
		}
	}

	private const float kLineWidth = 1f;
	private const int kLineSegments = 180;

	private List<Vector3> points = new List<Vector3>();
	private LineRenderer lineRenderer = null;

	public void Add(Vector3 point)
	{
		points.Add(point);

		if (points.Count >= 2)
		{
			lineRenderer.SetVertexCount(kLineSegments * (points.Count - 1));

			int linePointCount = 0;
			for (int i = 0; i < points.Count - 1; i++)
			{
				for (int j = 0; j < kLineSegments; j++)
				{
					Vector3 p = Vector3.Slerp(points[i], points[i + 1], j / (float)kLineSegments);
					lineRenderer.SetPosition(linePointCount++, p);
				}
			}
		}
	}

	private void Awake() 
	{
		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.SetWidth(kLineWidth, kLineWidth);
		lineRenderer.SetColors(Color.white, Color.white);
		lineRenderer.useWorldSpace = true;
		lineRenderer.material = material;
	}
}
