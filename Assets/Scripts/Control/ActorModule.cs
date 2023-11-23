using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorModule : MonoBehaviour
{
     public enum HandState
     {
          Empty = 0,
          Holding
     }

     public enum ActorState
     {
          Normal = 0,
          Stunned,
          Animation
     }

     [SerializeField]
     private LayerMask   holdableObjectLayerMask;

     [SerializeField]
     private float       holdableRaycastDistance  = 5f;
     
     [SerializeField]
     private Vector2     _inputAxis               = new Vector2();
     private bool[]      _inputAction             = new bool[2];
     
     private Vector3     _movementTarget          = Vector3.zero;
     private Quaternion  _rotationQuaternion      = Quaternion.identity;
     private Vector3     _desiredForward          = Vector3.zero;
     
     [SerializeField]
     private Rigidbody   _rigidbody;
     [SerializeField]
     private Transform   _holdPositionTransform;
     
     private GameObject _holdableGameObject;
     private HoldableModule _holdableModule;
     private Rigidbody _holdableRigidbody;
     private HoldableModule.HoldableType _holdableType;

     [SerializeField]
     private float       _turnSpeed               = 30f;

     [SerializeField]
     private float       _movementSpeedMultiplier = 0.2f;

     public HandState    handState                = HandState.Empty;
     public ActorState   actorState               = ActorState.Normal;

     public void RecieveInputs(Vector2 inputAxisToRecieve, bool[] inputActionToRecieve)
     {
          _inputAxis = inputAxisToRecieve;
          _inputAction = inputActionToRecieve;
          UpdateActor();
     }

     private void UpdateActor()
     {
          UpdateMovement();
          UpdateAction();
     }
     
     private void UpdateMovement()
     {
          // set movement target
          _movementTarget.Set(_inputAxis.x, 0f, _inputAxis.y);
          _movementTarget.Normalize();

          // calculate forward vector
          _desiredForward = Vector3.RotateTowards(transform.forward, _movementTarget, _turnSpeed * Time.deltaTime, 0f);
          _rotationQuaternion = Quaternion.LookRotation(_desiredForward);
          
          // move rigidbody
          _rigidbody.MovePosition(_rigidbody.position + _movementTarget * _movementSpeedMultiplier);
          _rigidbody.MoveRotation(_rotationQuaternion);
     }

     private void UpdateAction()
     {
          if (_inputAction[0]) ActionHoldObject();
          if (_inputAction[1]) ActionUseObject();
     }

     private void ActionHoldObject()
     {
          switch (handState)
          {
               case HandState.Empty:
                    AttemptHoldObject();
                    handState = HandState.Holding;
                    break;
               case HandState.Holding:
                    AttemptReleaseObject();
                    handState = HandState.Empty;
                    break;
               default:
                    Debug.LogError(gameObject.name + ".ActorModule // Unknown HandState");
                    break;
          }
     }

     private void ActionUseObject()
     {
          if (handState == HandState.Empty) return;

          switch (_holdableType)
          {
               case HoldableModule.HoldableType.Trash:
                    ActionThrowObject();
                    break;
               case HoldableModule.HoldableType.Tool:
                    break;
               default:
                    Debug.LogError(gameObject.name + ".ActorModule // Unknown HoldableType");
                    break;
          }
     }
     
     private void ActionThrowObject()
     {
          AttemptReleaseObject();
          _holdableRigidbody.AddForce(_desiredForward * 2000f);
     }

     private void AttemptHoldObject()
     {
          RaycastHit hit;
          if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, holdableRaycastDistance, holdableObjectLayerMask))
          {
               Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
               
               Transform hitTransform = hit.collider.transform;
               
               _holdableGameObject = hitTransform.parent.gameObject;
               _holdableModule = _holdableGameObject.GetComponent<HoldableModule>();
               _holdableRigidbody = _holdableGameObject.GetComponent<Rigidbody>();
               _holdableModule.AttemptHold();
               
               _holdableGameObject.transform.SetParent(_holdPositionTransform);
               _holdableGameObject.transform.localPosition = Vector3.zero;
          }
     }

     private void AttemptReleaseObject()
     {
          _holdableModule.ReleaseHold();
     }
}
