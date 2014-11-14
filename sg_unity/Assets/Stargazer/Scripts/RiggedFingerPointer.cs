/*
 * Draw a LineRenderer from the tip of the index finger to the intersection with the SkyBox.
 * Use RiggedPepperCutHand
 * Thibaud
 * 
 * PENDING:
 * - Move the LineRenderer's initialization outside of the Update()
 * - The LineRenderer doesn't show on the left hand, only on the right hand
 * - Calculate the intersection with the SkyBox
 */

using UnityEngine;
using System;
using System.Collections;
using Leap;

public class RiggedFingerPointer : RiggedFinger
{

	// pitch to horizon (the pitch value is relative to the head-mounted Leap Motion: zero is finger pointing up, Math.PI/2 is pointing at horizon in front, Math.PI is pointing down, any other value is pointing behind)
	const double horizon = Math.PI/2;

	// Bone.BoneType.TYPE_DISTAL
	const int bone_distal = 3;

	// PENDING: calculate the intersection with the SkyBox
	public int lineLength = 800;

	// left/right lineRenderer
	private LineRenderer lineRenderer;

	public override void UpdateFinger()
	{
		base.UpdateFinger();
		// is this the index finger?
		if (this.fingerType == Finger.FingerType.TYPE_INDEX)
		{
			// which hand is this?
			if (this.finger_.Hand.IsLeft)
			{
				lineRenderer = FingerLineRenderers.Left;
			}
			else if (this.finger_.Hand.IsRight)
			{
				lineRenderer = FingerLineRenderers.Right;
			}
			else
			{
				return;
			}
			// is the finger extended and pointing above the horizon?
			if (this.finger_.IsExtended && this.finger_.Direction.Pitch < horizon)
			{
				if (lineRenderer != null) {
					// show the LineRenderer
					Vector3 start = GetBoneCenter(bone_distal);
					Vector3 dir = GetBoneDirection(bone_distal);
					Vector3 end = start + dir * lineLength;
					lineRenderer.enabled = true;
					lineRenderer.SetPosition(0, start);
					lineRenderer.SetPosition(1, end);
					RaycastHit hit;
					if (Physics.Raycast (start, dir*lineLength, out hit))
					{
						if (hit.collider)
						{
							print ("Hit: " + hit.collider.gameObject.name);
							//lineRenderer.SetPosition(1, new Vector3(0, 0, hit.distance));
						}
					}
					else
					{
						//lineRenderer.SetPosition(1, new Vector3(0, 0, 5000));
					}
				}
			}
			else
			{
				if (lineRenderer != null)
				{
					// hide the LineRenderer
					lineRenderer.enabled = false;
				}
			}
		}
	}
}
