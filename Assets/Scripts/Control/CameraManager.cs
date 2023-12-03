using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Vector3    _basePosition;
    [SerializeField] private Vector3    _targetPosition;
    [SerializeField] private Vector3    _currentPosition            = Vector3.zero;
    
    [SerializeField] private float      _shakeRandomizeInterval     = 0.3f;
    [SerializeField] private float      _shakeMagnatude             = 1f;
    [SerializeField] private float      _shakeLerpMultiplier        = 3f;

    private void Start()
    {
        _basePosition = transform.position;
        StartCoroutine(RandomizeTargetPosition());
    }

    private void Update()
    {
        CalculateShake();
    }

    private void CalculateShake()
    {
        _currentPosition = Vector3.Lerp(_currentPosition, _targetPosition, Time.deltaTime * _shakeLerpMultiplier);
        transform.position = _basePosition + _currentPosition;
    }

    private IEnumerator RandomizeTargetPosition()
    {
        while (true)
        {
            _targetPosition.Set(Random.Range(-_shakeMagnatude, _shakeMagnatude),
                Random.Range(-_shakeMagnatude, _shakeMagnatude), Random.Range(-_shakeMagnatude, _shakeMagnatude));
            
            yield return new WaitForSeconds(_shakeRandomizeInterval);
        }
    }
}
