using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
     public bool nextScene = false;

     private void Update()
     {
          if (nextScene)
          {
               SceneManager.LoadScene("IntroScene");
          }
     }
}
