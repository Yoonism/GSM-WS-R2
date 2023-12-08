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

     [SerializeField]
     private TextMeshProUGUI[] _trashCounterTMP;

     private void Start()
     {
          _controllerPersistant = ControllerPersistant.Instance;
     }
     
     private void Update()
     {
          UpdateTimer();
          UpdateScore();
     }

     public void UpdateTrashCounter(int type, int value)
     {
          _trashCounterTMP[type].text = value.ToString();
     }

     private void UpdateTimer()
     {
          _timerTMP.text = _controllerPersistant.GetRoundTimeInt().ToString();
     }

     private void UpdateScore()
     {
          _scoreCounterTMP.text = _controllerPersistant.GetScore().ToString();
     }
}
