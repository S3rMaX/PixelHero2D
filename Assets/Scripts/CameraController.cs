using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController playerController;
    private Transform camTransform;
    private Transform playerTransform;
    private BoxCollider2D levelLimit;


    // Start is called before the first frame update
    void Start()
    {
        levelLimit = GameObject.Find("LevelLimit").GetComponent<BoxCollider2D>();

        camTransform = transform;
        playerController = FindObjectOfType<PlayerController>();
        playerTransform = playerController.transform;
    }

    // Update is called once per frame
    void Update()
    {
        FollowCamera();
    }

    private void FollowCamera()
    {
        if (playerController != null)
        {
            camTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, camTransform.position.z);
        }
    }
}
