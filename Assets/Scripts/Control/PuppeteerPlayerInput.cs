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
     
     private void FixedUpdate()
     {
          RecieveInputs();
          PushInputs();
     }
     
     private void RecieveInputs()
     {
          _inputAxis.x = Input.GetAxis("Horizontal");
          _inputAxis.y = Input.GetAxis("Vertical");

          _inputAction[0] = Input.GetKeyDown(KeyCode.E);
          _inputAction[1] = Input.GetKeyDown(KeyCode.R);
     }

     private void PushInputs()
     {
          _actorModuleToPosess.RecieveInputs(_inputAxis, _inputAction);
     }
}
