using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterController : MonoBehaviour
{
     [SerializeField] private AudioSource _audioSource;
     [SerializeField] private AudioClip _bgmAudioClip;

     public int masterScore = 0;
     public int maxMasterScore = 0;
     
     private static MasterController instance = null;

     private void Awake()
     {
          instance = this;
          DontDestroyOnLoad(this.gameObject);
     }
     
     public static MasterController Instance
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
     private void Start()
     {
          _audioSource.Play();

          SceneManager.LoadScene("TitleScene");
     }
}
