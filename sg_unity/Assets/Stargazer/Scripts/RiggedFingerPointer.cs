/*
 * Draw a LineRenderer from the tip of the index finger to the intersection with the SkyBox.
 * Use RiggedPepperCutHand
 * Thibaud
 * 
 * PENDING:
 * - Move the LineRenderer's initialization outside of the Update()
 * - The index finger is stuck in extended position (LeapOVR bug?)
 * - The LineRenderer doesn't show on the left hand, only on the right hand
 * - Calculate the intersection with the SkyBox
 */

using UnityEngine;
using System.Collections;
using Leap;

public class RiggedFingerPointer : RiggedFinger
{

	// Bone.BoneType.TYPE_DISTAL
	const int bone_distal = 3;

	// variables
	public LineRenderer lineRenderer = null;
	public float lineWidth = 0.01f;
	public Color lineColor = Color.red;
	public Material material;

	// PENDING: calculate the intersection with the SkyBox
	public float lineLength = 100.0f;

	public override void UpdateFinger()
	{
		base.UpdateFinger();
		// is this the index finger?
		if (this.fingerType == Finger.FingerType.TYPE_INDEX)
		{
			// is the finger extended?
			if (this.finger_.IsExtended)
			{
				// prepare the LineRenderer; PENDING: move this initialization out of the Update()
				if (lineRenderer == null)
				{
					if (gameObject != null)
					{
						lineRenderer = gameObject.AddComponent<LineRenderer> ();
					}
					if (lineRenderer != null)
					{
						lineRenderer.SetWidth(lineWidth, lineWidth);
						lineRenderer.SetColors(lineColor, lineColor);
						lineRenderer.material = material;
						lineRenderer.useWorldSpace = true;
						lineRenderer.SetVertexCount(2);
					}
				}
				// draw a line from the tip of the index finger to the SkyBox
				else
				{
					Vector3 start = GetBoneCenter(bone_distal);
					Vector3 dir = GetBoneDirection(bone_distal);
					Vector3 end = start + dir * lineLength;
					lineRenderer.enabled = true;
					lineRenderer.SetPosition(0, start);
					lineRenderer.SetPosition(1, end);
					//Debug.DrawLine(start, end, Color.yellow);
					//Debug.DrawRay(start, dir * lineLength, Color.yellow);
				}
			}
			else
			{
				// hide the line
				if (lineRenderer != null)
				{
					lineRenderer.enabled = false;
				}
			}
		}
	}
}
