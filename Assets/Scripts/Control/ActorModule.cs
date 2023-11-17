using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorModule : MonoBehaviour
{
     [SerializeField]
     private Vector2     _inputAxis               = new Vector2();
     
     private Vector3     _movementTarget          = Vector3.zero;
     private Quaternion  _rotationQuaternion      = Quaternion.identity;
     private Vector3     _desiredForward          = Vector3.zero;
     [SerializeField]
     private Rigidbody   _rigidbody;

     [SerializeField]
     private float       _turnSpeed               = 30f;

     [SerializeField]
     private float       _movementSpeedMultiplier = 0.2f;

     public void RecieveInputs(Vector2 inputsToRecieve)
     {
          _inputAxis = inputsToRecieve;
          UpdateActor();
     }

     private void UpdateActor()
     {
          UpdateMovement();
     }

     public void UpdateMovement()
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
}
