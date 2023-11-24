using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_STAIN_HP : MonoBehaviour
{
     [SerializeField]
     private HoldableModule _holdableModule;

     private float sizeRatio = 1f;
     private void Update()
     {
          float ratio = (float)_holdableModule.holdableHp / (float)_holdableModule.holdableHpMax;
          sizeRatio = Mathf.Lerp(sizeRatio, ratio, Time.deltaTime * 4f);
          transform.localScale = new Vector3(sizeRatio, sizeRatio, sizeRatio);
     }
}
