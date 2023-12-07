using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionModule : MonoBehaviour
{
     public enum InteractionType
     {
          Stain = 0,
          Trash,
          Tool,
          Bug
     }

     public InteractionType interactionType = InteractionType.Trash;

     public void TEST_PUSH_DESTROY()
     {
          Destroy(gameObject);
     }
}
