using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EntityCockroachManager : MonoBehaviour
{
     [SerializeField]
     private Rigidbody   _rigidbody;
     [SerializeField]
     private float       _physicsUpdateInterval;
     [SerializeField]
     private Vector3     _physicsForceBase = Vector3.zero;
     [SerializeField]
     private float       _physicsForceMultiplier = 10f;
     private Vector3     _initialJumpForce = new Vector3();
     
     [SerializeField]
     private float _maxVelocity = 7f;

     private Quaternion _currentRotationQuaternion;
     private void Start()
     {
          _rigidbody.maxLinearVelocity = _maxVelocity;
          StartCoroutine(CockroachPhysicsRoutine());
          StartCoroutine(CockroachJumpRoutine());
     }

     private IEnumerator CockroachJumpRoutine()
     {
          yield return new WaitForSeconds(0.02f + Random.Range(0f, 0.06f));
          
          _initialJumpForce.Set(Random.Range(-8f, 8f), 18f, Random.Range(-13f, -4f));
          _rigidbody.AddForce(_initialJumpForce);

          yield return null;
     }
     
     private IEnumerator CockroachPhysicsRoutine()
     {
          yield return new WaitForSeconds(Random.Range(0.01f, 0.3f));
          
          while (true)
          {
               _physicsForceBase.x = Random.Range(-_physicsForceMultiplier, _physicsForceMultiplier);
               _physicsForceBase.z = Random.Range(-_physicsForceMultiplier, _physicsForceMultiplier);
               
               _rigidbody.AddForce(_physicsForceBase);
               yield return new WaitForSeconds(_physicsUpdateInterval);
          }
     }

     private void Update()
     {
          Quaternion _rotationQuaternion = Quaternion.LookRotation(_rigidbody.velocity);
          _currentRotationQuaternion =
               Quaternion.Lerp(_currentRotationQuaternion, _rotationQuaternion, Time.deltaTime * 10f);
          _rigidbody.MoveRotation(_currentRotationQuaternion);
     }
}
