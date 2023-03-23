using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    [SerializeField] private ItemsManager itemsManager;

    void Start()
    {
        itemsManager = FindObjectOfType<ItemsManager>();   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            itemsManager.AddItem("CoinSpin", 1);
            itemsManager.GetItemCount("CoinSpin");
            Destroy(gameObject);
        }
    }
}
