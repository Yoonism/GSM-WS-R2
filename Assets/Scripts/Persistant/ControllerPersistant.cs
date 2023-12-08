using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerPersistant : MonoBehaviour
{
     public float   roundTimeMax        = 90f;
     public float   roundTime;

     public int     trashCountMax       = 0;
     public int     trashCountCurrent   = 0;

     private int    _totalScore         = 0;
     
     public List<HoldableModule> trashRegister = new List<HoldableModule>();
     
     private static ControllerPersistant instance = null;

     public int[] trashTypeCounter = new int[5];

     [SerializeField]
     private UIManager _uiManager;

     [SerializeField]
     private GameObject[] _playerObjects = new GameObject[3];

     [SerializeField]
     private GameObject _endCard;

     public enum TrashType
     {
          Basic= 0,
          Dishes,
          Clothes,
          Stains,
          Roaches
     }

     private void Awake()
     {
               instance = this;
               //DontDestroyOnLoad(this.gameObject);
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
          if (roundTime > 0f)
          {
               roundTime -= Time.deltaTime;
          }
          else
          {
               StopTimer();
          }
     }

     private void StopTimer()
     {
          if (roundTime == 0f) return;

          MasterController.Instance.masterScore = _totalScore;
          _endCard.SetActive(true);
          roundTime = 0f;
     }

     public float GetRoundTimeRatio()
     {
          return roundTime / roundTimeMax;
     }

     public int GetRoundTimeInt()
     {
          return Mathf.CeilToInt(roundTime);
     }

     public void AddScore(int value)
     {
          _totalScore += value;
          
          if(value < 0) _uiManager.SetRedFlash();
     }

     public int GetScore()
     {
          return _totalScore;
     }

     public void IncreaseTrashType(TrashType type)
     {
          trashTypeCounter[(int)type]++;
          _uiManager.UpdateTrashCounter((int)type, trashTypeCounter[(int)type]);
     }
     
     public void DecreaseTrashType(TrashType type)
     {
          trashTypeCounter[(int)type]--;
          _uiManager.UpdateTrashCounter((int)type, trashTypeCounter[(int)type]);
     }
}
