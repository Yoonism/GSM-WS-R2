using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Vector3    _basePosition;
    [SerializeField] private Vector3    _targetPosition;
    [SerializeField] private Vector3    _currentPosition            = Vector3.zero;
    
    [SerializeField] private float      _shakeRandomizeInterval     = 0.3f;
    [SerializeField] private float      _shakeMagnatude             = 1f;
    [SerializeField] private float      _shakeLerpMultiplier        = 3f;

    [SerializeField] private float      _shakeMagnitudeDecay        = 3f;

    [SerializeField] private float      _oscMagnitude               = 1f;
    [SerializeField] private float      _oscSpeed                   = 1f;
    private double _oscillatorCounter = 0f;
    private float _oscillator = 0f;
    private Vector3 _oscillatorFinal = Vector3.zero;

    private static CameraManager instance = null;

    private void Awake()
    {
        instance = this;
    }
     
    public static CameraManager Instance
    {
        get
        {
            if (null == instance) return null;
            return instance;
        }
    }
    
    private void Start()
    {
        _basePosition = transform.position;
        StartCoroutine(RandomizeTargetPosition());
    }

    private void Update()
    {
        CalculateOscillator();
        CalculateShake();
    }

    private void CalculateOscillator()
    {
        _oscillatorCounter += Time.deltaTime * _oscSpeed;

        _oscillator = (float)Math.Cos(_oscillatorCounter * _oscMagnitude);
        _oscillatorFinal.x = _oscillator;
    }
    
    private void CalculateShake()
    {
        _currentPosition = Vector3.Lerp(_currentPosition, _targetPosition, Time.deltaTime * _shakeLerpMultiplier);
        transform.position = _basePosition + _currentPosition + _oscillatorFinal;

        _shakeMagnatude = Mathf.Lerp(_shakeMagnatude, 0f, Time.deltaTime * _shakeMagnitudeDecay);
    }

    private IEnumerator RandomizeTargetPosition()
    {
        while (true)
        {
            _targetPosition.Set(Random.Range(-_shakeMagnatude, _shakeMagnatude),
                _targetPosition.y, Random.Range(-_shakeMagnatude, _shakeMagnatude));
            
            yield return new WaitForSeconds(_shakeRandomizeInterval);
        }
    }

    public void SetCameraShake(float amount)
    {
        if (amount > _shakeMagnatude) _shakeMagnatude = amount;
    }
}
