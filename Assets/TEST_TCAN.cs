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
          counter++;
          _textMeshPro.text = counter.ToString();
          Destroy(other.gameObject);
     }
}
