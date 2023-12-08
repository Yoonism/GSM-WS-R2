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
     [SerializeField] private GameObject _deathEffect;

     public void TEST_PUSH_DESTROY()
     {
          ControllerPersistant.Instance.DecreaseTrashType(ControllerPersistant.TrashType.Roaches);
          Instantiate(_deathEffect, transform.position, Quaternion.identity);
          ControllerPersistant.Instance.AddScore(20);
          Destroy(gameObject);
     }
}
