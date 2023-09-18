using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D arrowRB;
    [SerializeField] private float arrowSpeed;
    private Vector2 _arrowDirection;
    public Vector2 ArrowDirection { get => _arrowDirection; set => _arrowDirection = value; }

    [SerializeField] private GameObject arrowImpact;
    [SerializeField] private GameObject arrowHit;
    [SerializeField] private EnemyController _enemyController;
    private Transform transformArrow;
    private SpriteRenderer arrowSpriteRenderer;

    [SerializeField] private LayerMask layerMaskGround;
    [SerializeField] private LayerMask layerMaskDestroyable;
    
    private void Awake()
    {
        arrowRB = GetComponent<Rigidbody2D>();
        transformArrow = GetComponent<Transform>();
        arrowSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered OnTriggerEnter2D");

        if (((1 << collision.gameObject.layer) & layerMaskGround) !=0)
        {
            Debug.Log("Collided with groundLayer");
            Instantiate(arrowImpact, transformArrow.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
        
        if (((1 << collision.gameObject.layer) & layerMaskDestroyable) !=0)
        {
            Instantiate(arrowImpact, transformArrow.position, Quaternion.identity);
            gameObject.SetActive(false);
            Debug.Log("Collided with Destroyable!");
        }

        if (collision.CompareTag("BatEnemy"))
        {
            EnemyController enemyController = collision.GetComponent<EnemyController>();
            Destroy(collision.gameObject);
            Instantiate(arrowHit,transformArrow.position,Quaternion.identity);
            gameObject.SetActive(false);
            if (enemyController != null)
            {
                enemyController.OnArrowHit();
            }
        }
    }

    private void OnEnable()
    {
        if (PlayerController.instance.isFacingRight)
        {
            arrowRB.velocity = transformArrow.right * arrowSpeed;
            arrowSpriteRenderer.flipX = false;
        }
        else
        {
            arrowRB.velocity = (transformArrow.right * -1)* arrowSpeed;
            arrowSpriteRenderer.flipX = true;
        }
    }

    private void OnBecameInvisible()
    {
        //Destroy (gameObject);
        gameObject.SetActive(false);
    }
}
