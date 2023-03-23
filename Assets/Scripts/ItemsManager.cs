using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemsManager : MonoBehaviour
{
    [SerializeField] private Dictionary<string, int> _itemsCollected = new Dictionary<string, int>();

    public void AddItem(string itemName, int amount)
    {
        if (_itemsCollected.ContainsKey(itemName))
        {
            _itemsCollected[itemName] += amount;
        }
        else
        {
            _itemsCollected.Add(itemName, amount);
        }
    }

    public int GetItemCount(string itemName)
    {
        int amount = 0;
        if (_itemsCollected.ContainsKey(itemName))
        {
            amount = _itemsCollected[itemName];
        }
        Debug.Log("Total Items:" + amount);
        return amount;
        
    }
}
