using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
     private ControllerPersistant _controllerPersistant;
     
     [SerializeField]
     private TextMeshProUGUI _timerTMP;
     [SerializeField]
     private TextMeshProUGUI _trashCounterMaxTMP;
     [SerializeField]
     private TextMeshProUGUI _trashCounterCurrentTMP;
     [SerializeField]
     private TextMeshProUGUI _scoreCounterTMP;

     private void Start()
     {
          _controllerPersistant = ControllerPersistant.Instance;
     }
     
     private void Update()
     {
          UpdateTimer();
          UpdateTrashCounter();
          UpdateScore();
     }

     private void UpdateTimer()
     {
          _timerTMP.text = _controllerPersistant.GetRoundTimeInt().ToString();
     }

     private void UpdateTrashCounter()
     {
          _trashCounterCurrentTMP.text = _controllerPersistant.trashCountCurrent.ToString();
          _trashCounterMaxTMP.text = _controllerPersistant.trashCountMax.ToString();
     }

     private void UpdateScore()
     {
          _scoreCounterTMP.text = _controllerPersistant.GetScore().ToString();
     }
}
