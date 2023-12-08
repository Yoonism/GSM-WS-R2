using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultController : MonoBehaviour
{
     public int _done;
     public int _notDone;
     public int total;

     [SerializeField]
     private TextMeshProUGUI[] _tmp;

     [SerializeField]
     private GameObject[] _coinImage;
     
     private void Start()
     {
          _done = MasterController.Instance.masterScore;
          total = MasterController.Instance.maxMasterScore;
          _notDone = total - _done;

          _tmp[0].text = _done.ToString();
          _tmp[1].text = _notDone.ToString();
          _tmp[2].text = total.ToString();

          float ratio = (float)_done / (float)total;

          if (ratio > 0.2f) _coinImage[0].SetActive(true);
          if (ratio > 0.45f) _coinImage[1].SetActive(true);
          if (ratio > 0.7f) _coinImage[2].SetActive(true);
     }
}
