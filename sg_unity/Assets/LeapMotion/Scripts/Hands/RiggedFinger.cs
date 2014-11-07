/******************************************************************************\
* Copyright (C) Leap Motion, Inc. 2011-2014.                                   *
* Leap Motion proprietary. Licensed under Apache 2.0                           *
* Available at http://www.apache.org/licenses/LICENSE-2.0.html                 *
\******************************************************************************/

using UnityEngine;
using System.Collections;
using Leap;

public class RiggedFinger : FingerModel {

  public static readonly string[] FINGER_NAMES = {"Thumb", "Index", "Middle", "Ring", "Pinky"};

  public Transform[] bones = new Transform[NUM_BONES];

  public Vector3 modelFingerPointing = Vector3.forward;
  public Vector3 modelPalmFacing = -Vector3.up;
  
  public Quaternion Reorientation() {
    return Quaternion.Inverse(Quaternion.LookRotation(modelFingerPointing, -modelPalmFacing));
  }

  public override void InitFinger() {
    UpdateFinger();
  }

  public override void UpdateFinger() {
    for (int i = 0; i < bones.Length; ++i) {
      if (bones[i] != null)
        bones[i].rotation = GetBoneRotation(i) * Reorientation();
    }
    if (this.fingerType == Finger.FingerType.TYPE_INDEX) {
      // Thibaud: raycast index finger tip to infinity; use RiggedPepperCutHand + Gizmos
      Vector3 fromPosition = GetJointPosition (NUM_BONES - 1);
      Vector3 toPosition = GetJointPosition (NUM_BONES);
      Vector3 direction = toPosition - fromPosition;
      toPosition = fromPosition + direction*100.0f;
      Debug.DrawLine (fromPosition, toPosition, Color.blue);
    }
  }
}
