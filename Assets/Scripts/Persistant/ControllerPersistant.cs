using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPersistant : MonoBehaviour
{
     public float roundTimeMax = 90f;
     public float roundTime;

     public int trashCountMax = 0;
     public int trashCountCurrent = 0;

     public List<HoldableModule> trashRegister = new List<HoldableModule>();
     
     private static ControllerPersistant instance = null;

     private void Awake()
     {
               instance = this;
               DontDestroyOnLoad(this.gameObject);
               roundTime = roundTimeMax;
     }
     
     public static ControllerPersistant Instance
     {
          get
          {
               if (null == instance)
               {
                    return null;
               }
               return instance;
          }
     }

     public int RegisterTrash(HoldableModule holdableModule)
     {
          trashRegister.Add(holdableModule);
          trashCountMax++;
          trashCountCurrent++;
          return trashRegister.Count - 1;
     }

     public void UnregisterTrash(HoldableModule holdableModule)
     {
          trashCountCurrent--;
          trashRegister.Remove(holdableModule);
     }

     private void FixedUpdate()
     {
          roundTime -= Time.deltaTime;
     }

     public float GetRoundTimeRatio()
     {
          return roundTime / roundTimeMax;
     }

     public int GetRoundTimeInt()
     {
          return Mathf.CeilToInt(roundTime);
     }
}
