using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_STAIN_HP : MonoBehaviour
{
     [SerializeField]
     private HoldableModule _holdableModule;

     [SerializeField]
     private Transform _meshTransform;

     private float sizeRatio = 1f;
     private void Update()
     {
          float ratio = (float)_holdableModule.holdableHp / (float)_holdableModule.holdableHpMax;
          sizeRatio = Mathf.Lerp(sizeRatio, ratio, Time.deltaTime * 4f);
          _meshTransform.localScale = new Vector3(sizeRatio, sizeRatio, sizeRatio);
     }
}
