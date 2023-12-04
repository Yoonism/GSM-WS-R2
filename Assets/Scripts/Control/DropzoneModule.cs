using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;

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
    
    private void OnCollisionEnter(Collision other)
    {
        if(isCoolingDown) return;
        
        HoldableModule otherHoldableModule = other.transform.GetComponent<HoldableModule>();

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
        if (acceptableTrashType.Contains(otherHoldableModule.trashType)) otherHoldableModule.DisposeProper();
        else otherHoldableModule.DisposeImproper();
        
        HandleBugSpawn();

        if (cooldownTime > 0f) StartCoroutine(HandleCooldown());
    }

    private IEnumerator HandleCooldown()
    {
        float time = 0f;
        isCoolingDown = true;

        while (time < cooldownTime)
        {
            time += Time.deltaTime;
            _cooldownTimerTMP.text = Mathf.Ceil(cooldownTime - time).ToString();
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
        Debug.Log("Bug spawned");
    }
}
