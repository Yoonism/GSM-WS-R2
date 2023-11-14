using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour
{
     #region ---------- [ Structs ]
     
     [System.Serializable]
     public struct GameBlock
     {
          public GameObject        gameBlockObject;
          public GameBlockManager  gameBlockManager;

          public GameBlock(GameObject newGameBlockObject, GameBlockManager newGameBlockmanager)
          {
               gameBlockObject     = newGameBlockObject;
               gameBlockManager    = newGameBlockmanager;
          }
     }
     
     #endregion
     
     #region ---------- [ Game field ]

     public Vector2Int        fieldSize = new Vector2Int(20, 10);
     private GameBlock[,]      _gameFieldArray;
     
     #endregion

     private void Start()
     {
          InitializeGameFieldArray();
     }

     private void InitializeGameFieldArray()
     {
          _gameFieldArray = new GameBlock[fieldSize.x, fieldSize.y];

          Debug.Log("GameField // Game field initialized with " + fieldSize.x * fieldSize.y + " spaces");
     }
}