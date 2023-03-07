using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRB;
    
    [SerializeField]
    private float moveSpeed;
    
    [SerializeField]
    private float jumpForce;

    private Transform checkGroundPoint;
    
    [SerializeField]
    private LayerMask selectedLayerMask;
    
    [SerializeField]
    private bool isGrounded;
    
    private Animator animator;
    private int IdSpeed;
    private int IdIsGrounded;

    private void Awake()
    {
            playerRB = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        checkGroundPoint = GameObject.Find("CheckGroundPoint").GetComponent<Transform>();
        animator = GameObject.Find("IddlePlayer").GetComponent<Animator>();
        IdSpeed = Animator.StringToHash("Speed");
        IdIsGrounded = Animator.StringToHash("isGrounded");
    }

    // Update is called once per frame
    void Update()
    {   
        MovePlayer();
        JumpPlayer();
    }

    private void JumpPlayer()
    {
        isGrounded = Physics2D.OverlapCircle(checkGroundPoint.position, 0.2f, selectedLayerMask);
        animator.SetBool(IdIsGrounded, isGrounded);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
        }
    }

    private void MovePlayer()
    {
        if (playerRB.velocity.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        else if (playerRB.velocity.x > 0)
            transform.localScale = Vector3.one;

        float inputX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        playerRB.velocity = new Vector2(inputX, playerRB.velocity.y);
        animator.SetFloat(IdSpeed, Mathf.Abs(playerRB.velocity.x));
    }
}
