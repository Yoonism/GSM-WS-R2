using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndCanvasController : MonoBehaviour
{
     [SerializeField]
     private float _iAlpha = -1f;
     private Color _color = Color.black;

     [SerializeField]
     private Image _image;
     
     private void Update()
     {
          _iAlpha += Time.deltaTime;
          _color.a = _iAlpha;

          _image.color = _color;

          if (_iAlpha > 1.1f)
          {
               SceneManager.LoadScene("ResultScene");
          }
     }
}
