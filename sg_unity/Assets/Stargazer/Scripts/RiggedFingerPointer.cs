/******************************************************************************\
* Copyright (C) Leap Motion, Inc. 2011-2014.                                   *
* Leap Motion proprietary. Licensed under Apache 2.0                           *
* Available at http://www.apache.org/licenses/LICENSE-2.0.html                 *
\******************************************************************************/

using UnityEngine;
using System.Collections;
using Leap;

public class RiggedFingerPointer : RiggedFinger {	
	public override void UpdateFinger() {
		base.UpdateFinger();
    	if (this.fingerType == Finger.FingerType.TYPE_INDEX) {
	      // Thibaud: raycast index finger tip to infinity; use RiggedPepperCutHand + Gizmos
	      Debug.DrawRay(GetBoneCenter(3), GetBoneDirection(3) * 100.0f, Color.blue);
	    }
	}
}
