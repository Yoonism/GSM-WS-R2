using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;
using Random = UnityEngine.Random;

public class DropzoneModule : MonoBehaviour
{

    [SerializeField]
    private List<HoldableModule.TrashType> acceptableTrashType = new List<HoldableModule.TrashType>();

    [SerializeField]
    private float cooldownTime = 0f;

    [SerializeField]
    private TextMeshPro _cooldownTimerTMP;
    public bool isCoolingDown = false;

    [SerializeField]
    private bool _canSpawnBugs = false;
    [SerializeField]
    private AnimationCurve _bugSpawnRateCurve;
    [SerializeField]
    private float _bugSpawnCounter;
    [SerializeField]
    private int _bugSpawnNumber = 6;
    [SerializeField]
    private GameObject _bugSpawnPrefab;
    [SerializeField]
    private Transform _bugSpawnPointTransform;

    [SerializeField]
    private Transform _meshTransform;

    private Vector3 _meshStartScale;

    [SerializeField]
    private float _meshMultiplier = 1.5f;

    [SerializeField]
    private GameObject _properDisposalEffect;

    [SerializeField]
    private AudioSource _audioSource;
    
    private void Start()
    {
        _meshStartScale = _meshTransform.localScale;
    }
    private void OnCollisionEnter(Collision other)
    {
        if(isCoolingDown) return;

        HoldableModule otherHoldableModule;
        bool hasComponent = other.transform.TryGetComponent<HoldableModule>(out otherHoldableModule);

        if (!hasComponent) return;

        switch (otherHoldableModule.holdableType)
        {
            case HoldableModule.HoldableType.Trash:
                HandleTrash(otherHoldableModule);
                break;
            default:
                break;
        }
    }

    private void HandleTrash(HoldableModule otherHoldableModule)
    {
        if (acceptableTrashType.Contains(otherHoldableModule.trashType))
        {
            Instantiate(_properDisposalEffect, otherHoldableModule.transform.position, Quaternion.identity);
            otherHoldableModule.DisposeProper();
        }
        else otherHoldableModule.DisposeImproper();
        
        HandleBugSpawn();
        _audioSource.Play();

        if (cooldownTime > 0f) StartCoroutine(HandleCooldown());

        _meshTransform.localScale = _meshStartScale * _meshMultiplier;
    }

    private void Update()
    {
        _meshTransform.localScale = Vector3.Lerp(_meshTransform.localScale, _meshStartScale, Time.deltaTime * 20f);
    }

    private IEnumerator HandleCooldown()
    {
        float time = 0f;
        isCoolingDown = true;

        while (time < cooldownTime)
        {
            time += Time.deltaTime;
            //_cooldownTimerTMP.text = Mathf.Ceil(cooldownTime - time).ToString();
            yield return null;
        }

        isCoolingDown = false;
    }

    private void HandleBugSpawn()
    {
        if (!_canSpawnBugs) return;

        _bugSpawnCounter += 1f;
        float i = Random.Range(0f, 1f);
        
        if (i < _bugSpawnRateCurve.Evaluate(_bugSpawnCounter))
        {
            SpawnBug();
        }
    }

    public void SpawnBug()
    {
        _bugSpawnCounter = 0f;

        for (int i = 0; i < _bugSpawnNumber; i++)
        {
            Instantiate(_bugSpawnPrefab, _bugSpawnPointTransform.position, Quaternion.identity);
        }
    }
}
