using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPool : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private List<GameObject> arrowList;

    private static ArrowPool instance;
    public static ArrowPool Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AddArrowsToPool(poolSize);
    }

    private void AddArrowsToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab);
            arrow.SetActive(false);
            arrowList.Add(arrow);
            arrow.transform.parent = transform;
        }
    }

    public GameObject RequestArrow()
    {
        for (int i = 0; i < arrowList.Count; i++)
        {
            if (!arrowList[i].activeSelf)
            {
                arrowList[i].SetActive(true);
                return arrowList[i];
            }
        }
        AddArrowsToPool(1);
        arrowList[arrowList.Count -1].SetActive(true);
        return arrowList[arrowList.Count - 1];
    }
}
