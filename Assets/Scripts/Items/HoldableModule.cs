using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldableModule : MonoBehaviour
{
     [SerializeField]
     private Rigidbody _rigidbody;
     public enum HoldableState
     {
          Free = 0,
          Held,
          Stun
     }

     public enum HoldableType
     {
          Trash = 0,
          Tool
     }

     public HoldableState holdableState      =    HoldableState.Free;
     public HoldableType holdableType        =    HoldableType.Trash;

     public bool AttemptHold()
     {
          _rigidbody.isKinematic = true;
          _rigidbody.detectCollisions = false;
          holdableState = HoldableState.Held;
          return false;
     }

     public bool ReleaseHold()
     {
          _rigidbody.isKinematic = false;
          _rigidbody.detectCollisions = true;
          holdableState = HoldableState.Free;
          transform.SetParent(null);
          return false;
     }
}
