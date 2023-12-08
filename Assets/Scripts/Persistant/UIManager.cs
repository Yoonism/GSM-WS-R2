using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
     private ControllerPersistant _controllerPersistant;

     [SerializeField]
     private TextMeshProUGUI[] _timerTMP = new TextMeshProUGUI[2];
     [SerializeField]
     private TextMeshProUGUI _trashCounterMaxTMP;
     [SerializeField]
     private TextMeshProUGUI _trashCounterCurrentTMP;
     [SerializeField]
     private TextMeshProUGUI _scoreCounterTMP;

     [SerializeField]
     private TextMeshProUGUI[] _trashCounterTMP;

     [SerializeField]
     private Image _timerBar;

     [SerializeField]
     private Image _redFlash;

     private Color _redFlashColor = new Color(1f, 0f, 0f, 0f);
     [SerializeField]
     private AudioSource _redFlashAudioSource;

     private void Start()
     {
          _controllerPersistant = ControllerPersistant.Instance;
     }
     
     private void Update()
     {
          UpdateTimer();
          UpdateScore();
          UpdateRedFlash();
     }

     public void UpdateTrashCounter(int type, int value)
     {
          _trashCounterTMP[type].text = value.ToString();
     }

     private void UpdateRedFlash()
     {
          _redFlashColor.a = Mathf.Lerp(_redFlashColor.a, 0f, Time.deltaTime * 10f);
          _redFlash.color = _redFlashColor;
     }

     public void SetRedFlash()
     {
          _redFlashColor.a = 0.5f;
          _redFlashAudioSource.Play();
     }

     private void UpdateTimer()
     {
          int timeValue = _controllerPersistant.GetRoundTimeInt();
          string zeroOut = "0";

          if (timeValue % 60 < 10) zeroOut = "0";
          else zeroOut = "";
          _timerTMP[0].text = "0" + (timeValue / 60).ToString();
          _timerTMP[1].text = zeroOut + (timeValue % 60).ToString();

          _timerBar.fillAmount = ControllerPersistant.Instance.GetRoundTimeRatio();
     }

     private void UpdateScore()
     {
          _scoreCounterTMP.text = _controllerPersistant.GetScore().ToString();
     }
}
