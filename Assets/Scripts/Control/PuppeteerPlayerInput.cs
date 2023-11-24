using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppeteerPlayerInput : MonoBehaviour
{
     [SerializeField]
     private Vector2     _inputAxis     = new Vector2();

     [SerializeField]
     private bool[]      _inputAction   = new bool[2];

     [SerializeField]
     private ActorModule _actorModuleToPosess;

     public bool J2X = false;
     
     private void Update()
     {
          RecieveInputs();
          PushInputs();
     }
     
     private void RecieveInputs()
     {
          if (!J2X)
          {
               _inputAxis.x = Input.GetAxis("Horizontal");
               _inputAxis.y = Input.GetAxis("Vertical");
               
               _inputAction[0] = Input.GetKeyDown(KeyCode.E);
               _inputAction[1] = Input.GetKeyDown(KeyCode.R);
          }
          else
          {
               _inputAxis.x = Input.GetAxis("J2X");
               _inputAxis.y = Input.GetAxis("J2Y");
               
               _inputAction[0] = Input.GetButtonDown("J2A");
               _inputAction[1] = Input.GetButtonDown("J2B");
          }


     }

     private void PushInputs()
     {
          _actorModuleToPosess.RecieveInputs(_inputAxis, _inputAction);
     }
}
