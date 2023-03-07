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
    private float cameraSizeHorizontal;
    private float cameraSizeVertical;


    // Start is called before the first frame update
    void Start()
    {
        levelLimit = GameObject.Find("LevelLimit").GetComponent<BoxCollider2D>();
        cameraSizeVertical = Camera.main.orthographicSize;
        cameraSizeHorizontal = Camera.main.orthographicSize * Camera.main.aspect;

        camTransform = transform;
        playerController = FindObjectOfType<PlayerController>();
        playerTransform = playerController.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        FollowCamera();
    }

    private void FollowCamera()
    {
        if (playerController != null)
        {
            camTransform.position = new Vector3(Mathf.Clamp(playerTransform.position.x,
                                                            levelLimit.bounds.min.x + cameraSizeHorizontal,
                                                            levelLimit.bounds.max.x - cameraSizeHorizontal),
                                                Mathf.Clamp(playerTransform.position.y, 
                                                            levelLimit.bounds.min.y + cameraSizeVertical,
                                                            levelLimit.bounds.max.y - cameraSizeVertical),
                                                camTransform.position.z);
        }
    }
}
