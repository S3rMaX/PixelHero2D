using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ItemsManager _itemsManager;
    // Start is called before the first frame update
    private void OnGUI()
    {
        GUILayout.Label("Heart Shining Left: " + _itemsManager.heartsToUnlockDoubleJump);
        GUILayout.Label("Coin Spinning Left: " + _itemsManager.coinsSpinningToUnlockDash);
        GUILayout.Label("Coin Shining Left: " + _itemsManager.coinsShiningToUnlockBallModeAndDropBombs);
    }
}
