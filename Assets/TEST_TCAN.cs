using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TEST_TCAN : MonoBehaviour
{
     private int counter = 0;
     [SerializeField]
     private TextMeshPro _textMeshPro;
     private void OnCollisionEnter(Collision other)
     {
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
          if (otherHoldableModule.trashType == HoldableModule.TrashType.Basic)
          {
               counter++;
               _textMeshPro.text = counter.ToString();
               Destroy(otherHoldableModule.gameObject);
          }
          else
          {
               //Debug.Log("Collision with non-basic trash");
          }
     }
}
