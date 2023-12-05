using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EntityCockroachManager : MonoBehaviour
{
     [SerializeField]
     private Rigidbody _rigidbody;
     [SerializeField]
     private float _physicsUpdateInterval;
     [SerializeField]
     private Vector3 _physicsForceBase = Vector3.zero;

     [SerializeField]
     private Vector3 _physicsTorqueBase = Vector3.zero;
     [SerializeField]
     private float _physicsForceMultiplier = 10f;

     [SerializeField]
     private float _maxVelocity = 7f;

     private Quaternion _currentRotationQuaternion;
     private void Start()
     {
          _rigidbody.maxLinearVelocity = _maxVelocity;
          StartCoroutine(CockroachPhysicsRoutine());
     }

     private IEnumerator CockroachPhysicsRoutine()
     {
          while (true)
          {
               _physicsForceBase.x = Random.Range(-_physicsForceMultiplier, _physicsForceMultiplier);
               _physicsForceBase.z = Random.Range(-_physicsForceMultiplier, _physicsForceMultiplier);
               
               _rigidbody.AddForce(_physicsForceBase);
               _rigidbody.AddTorque(_physicsTorqueBase);
               yield return new WaitForSeconds(_physicsUpdateInterval);
          }
     }

     private void Update()
     {
          Quaternion _rotationQuaternion = Quaternion.LookRotation(_rigidbody.velocity);
          _currentRotationQuaternion =
               Quaternion.Lerp(_currentRotationQuaternion, _rotationQuaternion, Time.deltaTime * 40f);
          _rigidbody.MoveRotation(_currentRotationQuaternion);
     }
}
