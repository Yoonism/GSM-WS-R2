using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPersistant : MonoBehaviour
{
     public float gameRule_gridSize = 2f;

     private void Awake()
     {
          DontDestroyOnLoad(gameObject);
     }
}
