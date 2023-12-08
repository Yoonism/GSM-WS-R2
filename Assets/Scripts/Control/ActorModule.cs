using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

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

     public string actorName = "Player";

     [SerializeField]
     private LayerMask   holdableObjectLayerMask;
     [SerializeField]
     private float       holdableRaycastDistance  = 5f;

     [SerializeField]
     private float      _physicsForceValue = 30f;
     [SerializeField]
     private float      _physicsMaxVelocity = 8f;

     [SerializeField]
     private GameObject  actorStunEffect;
     [SerializeField]
     private Vector3     actorStunEffectOffset;
     
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
     [SerializeField]
     private Animator    _animator;
     
     private GameObject  _holdableGameObject;
     private HoldableModule _holdableModule;
     private Rigidbody   _holdableRigidbody;
     private HoldableModule.HoldableType _holdableType;

     [SerializeField]
     private float       _turnSpeed               = 30f;

     [SerializeField]
     private float       _movementSpeedMultiplier = 10f;

     public HandState    handState                = HandState.Empty;
     public ActorState   actorState               = ActorState.Normal;

     [SerializeField] private AudioSource _audioSource;
     [SerializeField] private AudioClip _ACGrabObject;
     [SerializeField] private AudioClip _ACThrowObject;
     [SerializeField] private AudioClip _ACStun;

     public bool isStunImmune = false;

     public void RecieveInputs(Vector2 inputAxisToRecieve, bool[] inputActionToRecieve)
     {
          _inputAxis = inputAxisToRecieve;
          _inputAction = inputActionToRecieve;
          UpdateActor();
     }

     private void UpdateActor()
     {
          if (actorState != ActorState.Normal) return;
          //UpdateMovement();
          UpdateMovementPhysics();
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
          _rigidbody.Move(_rigidbody.position + _movementTarget * _movementSpeedMultiplier * Time.deltaTime, _rotationQuaternion);
          //_rigidbody.MoveRotation(_rotationQuaternion);
     }

     private void UpdateMovementPhysics()
     {
          // set movement target
          _movementTarget.Set(_inputAxis.x, 0f, _inputAxis.y);
          _movementTarget.Normalize();

          // calculate forward vector
          _desiredForward = Vector3.RotateTowards(transform.forward, _movementTarget, _turnSpeed * Time.deltaTime, 0f);
          _rotationQuaternion = Quaternion.LookRotation(_desiredForward);

          _rigidbody.MoveRotation(_rotationQuaternion);
          _rigidbody.AddForce(_movementTarget * _physicsForceValue);

          _rigidbody.maxLinearVelocity = _physicsMaxVelocity;
          
          // handle animation
          if (_rigidbody.velocity.magnitude > 2f) _animator.SetBool("isWalking", true);
          else _animator.SetBool("isWalking", false);
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
                    break;
               case HandState.Holding:
                    AttemptReleaseObject();
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
          Vector3 throwForce = transform.TransformDirection(Vector3.forward) + new Vector3(0f, 0.3f, 0f);
          AttemptReleaseObject();
          _holdableRigidbody.AddForce(throwForce * 1000f);
          _holdableModule.StartThrowRoutine();

          _audioSource.PlayOneShot(_ACThrowObject);
          
          StartCoroutine(ProcessStunImmunity());
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
               _holdableGameObject.transform.localEulerAngles = Vector3.zero;

               handState = HandState.Holding;

               _audioSource.PlayOneShot(_ACGrabObject);
          }
     }

     private void AttemptReleaseObject()
     {
          _holdableModule.ReleaseHold();
          handState = HandState.Empty;
     }

     private void ActorStun()
     {
          if (actorState != ActorState.Stunned && !isStunImmune) StartCoroutine(ProcessStun());
     }

     private IEnumerator ProcessStun()
     {
          _audioSource.PlayOneShot(_ACStun);
          Instantiate(actorStunEffect, transform.position + actorStunEffectOffset, Quaternion.identity);
          isStunImmune = true;
          Vector3 forceVelocity = new Vector3();
          forceVelocity.x = Random.Range(200f, 500f) * Mathf.Round(Random.Range(-1f, 1f));
          forceVelocity.y = Random.Range(80f, 120f);
          forceVelocity.z = Random.Range(200f, 500f) * Mathf.Round(Random.Range(-1f, 1f));

          _rigidbody.AddForce(forceVelocity);

          CameraManager.Instance.SetCameraShake(3f);
          
          actorState = ActorState.Stunned;
          _rigidbody.freezeRotation = false;
          yield return new WaitForSeconds(3f);

          _rigidbody.freezeRotation = true;
          actorState = ActorState.Normal;

          isStunImmune = false;
          yield return null;
     }

     private IEnumerator ProcessStunImmunity()
     {
          if (isStunImmune) yield return null;

          isStunImmune = true;
          yield return new WaitForSeconds(0.1f);
          isStunImmune = false;
          
          yield return null;
     }

     private void OnTriggerEnter(Collider other)
     {
          float impactMagnitude = other.attachedRigidbody.velocity.magnitude;

          if (impactMagnitude > 12f) ActorStun();
     }
}
