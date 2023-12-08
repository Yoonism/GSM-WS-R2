using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SlideController : MonoBehaviour
{
     [SerializeField]
     private Image[] _slides = new Image[4];

     [SerializeField]
     private int phase = 0;

     private void Update()
     {
          if (Input.GetKeyDown(KeyCode.Space))
          {
               phase++;
          }

          Color _vColor = new Color(0f, 0f, 0f, 2f * Time.deltaTime);

          for (int i = 0; i < 4; i++)
          {
               if (i <= phase)
               {
                    _slides[i].color += _vColor;
               }
          }

          if (phase > 2 && _slides[3].color.a >= 1f)
          {
               SceneManager.LoadScene("SampleScene");
          }
     }
}
