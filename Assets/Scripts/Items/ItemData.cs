using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "GameItems/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
     public string       itemName       = "";
     public float        itemWeight     = 1f;
     public GameObject   itemPrefab;

     private void Start()
     {

     }
}