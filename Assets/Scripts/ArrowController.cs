using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private Rigidbody2D arrowRB;
    [SerializeField]
    private float arrowSpeed;
    private Vector2 arrowDirection;

    private void Awake()
    {
        arrowRB = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        arrowDirection = new Vector2 (1, arrowRB.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        arrowRB.velocity = arrowDirection * arrowSpeed;
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
