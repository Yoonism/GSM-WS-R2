using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemModule : MonoBehaviour
{
     [SerializeField]
     private ItemData _itemData;

     private string _itemName = "";
     private float _itemWeight = 1f;
     private GameObject _itemPrefab;

     private void Start()
     {
          InitializeItem();
     }

     private void InitializeItem()
     {
          _itemName = _itemData.itemName;
          _itemWeight = _itemData.itemWeight;
          _itemPrefab = Instantiate(_itemData.itemPrefab, transform);
     }
}
