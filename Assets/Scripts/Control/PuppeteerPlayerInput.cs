using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppeteerPlayerInput : MonoBehaviour
{
     [SerializeField]
     private Vector2     _inputAxis     = new Vector2();

     [SerializeField]
     private ActorModule _actorModuleToPosess;
     
     private void Update()
     {
          RecieveInputs();
          PushInputs();
     }
     
     private void RecieveInputs()
     {
          _inputAxis.x = Input.GetAxis("Horizontal");
          _inputAxis.y = Input.GetAxis("Vertical");
     }

     private void PushInputs()
     {
          _actorModuleToPosess.RecieveInputs(_inputAxis);
     }
}
