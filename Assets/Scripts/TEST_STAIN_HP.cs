using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TEST_STAIN_HP : MonoBehaviour
{
     [SerializeField]
     private TextMeshPro _TMP;

     [SerializeField]
     private HoldableModule _holdableModule;
     private void Update()
     {
          _TMP.text = _holdableModule.holdableHp.ToString();
     }
}
