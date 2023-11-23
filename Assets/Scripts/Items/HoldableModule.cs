using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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
          Stain,
          Tool
     }

     public HoldableState     holdableState       = HoldableState.Free;
     public HoldableType      holdableType        = HoldableType.Trash;
     public int               holdableHp          = 100;

     [SerializeField]
     private Collider _holdableScanCollider;
     [SerializeField]
     private float _holdableScanInterval = 0.1f;
     [SerializeField]
     private LayerMask _holdableScanLayerMask;

     private void Start()
     {
          RegisterHoldable();

          if (holdableType == HoldableType.Tool) StartCoroutine(HoldableScan());
     }

     private void RegisterHoldable()
     {
          switch (holdableType)
          {
               case HoldableType.Trash:
               case HoldableType.Stain:
                    ControllerPersistant.Instance.RegisterTrash(this);
                    break;
               case HoldableType.Tool:
                    break;
               default:
                    break;
          }
     }
     
     private void UnregisterHoldable()
     {
          switch (holdableType)
          {
               case HoldableType.Trash:
               case HoldableType.Stain:
                    ControllerPersistant.Instance.UnregisterTrash(this);
                    break;
               case HoldableType.Tool:
                    break;
               default:
                    break;
          }
     }

     public bool AttemptHold()
     {
          if (holdableType == HoldableType.Stain) return false;
          
          _rigidbody.isKinematic = true;
          _rigidbody.detectCollisions = false;
          holdableState = HoldableState.Held;
          return true;
     }

     public bool ReleaseHold()
     {
          _rigidbody.isKinematic = false;
          _rigidbody.detectCollisions = true;
          holdableState = HoldableState.Free;
          transform.SetParent(null);
          return true;
     }

     private IEnumerator HoldableScan()
     {
          while (true)
          {
               RaycastHit m_Hit;
               bool m_HitDetect = Physics.BoxCast(transform.position, transform.localScale, Vector3.down, out m_Hit, transform.rotation, 10f, _holdableScanLayerMask);
               if (m_HitDetect)
               {
                    m_Hit.transform.GetComponent<HoldableModule>().PushDamage(10);
               }
               
               yield return new WaitForSeconds(_holdableScanInterval);
          }
     }

     public int PushDamage(int damage)
     {
          if (holdableType != HoldableType.Stain) return 0;

          holdableHp -= damage;
          if (holdableHp <= 0)
          {
               Destroy(gameObject);
          }

          return 0;
     }

     private void OnDestroy()
     {
          UnregisterHoldable();
     }
}
