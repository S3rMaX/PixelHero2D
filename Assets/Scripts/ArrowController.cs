using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private Rigidbody2D arrowRB;
    [SerializeField]
    private float arrowSpeed;
    private Vector2 arrowDirection;

    public Vector2 ArrowDirection { get => arrowDirection; set => arrowDirection = value; }

    private void Awake()
    {
        arrowRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        arrowRB.velocity = ArrowDirection * arrowSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy (gameObject);
    }
}
