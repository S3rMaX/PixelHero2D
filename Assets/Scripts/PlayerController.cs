using System;
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
    private bool isFlippedInX;
    
    private Animator animator;
    private int IdSpeed;
    private int IdIsGrounded;

    private int IdShootArrow;

    private Transform transformArrowPoint;
    [SerializeField]
    private ArrowController arrowController;
    private Transform transformPlayerController;

    private void Awake()
    {
       playerRB = GetComponent<Rigidbody2D>();
       transformPlayerController = GetComponent<Transform>();
       isFlippedInX = CheckAndSetDirection();
    }

    private void Start()
    {
        checkGroundPoint = GameObject.Find("CheckGroundPoint").GetComponent<Transform>();
        animator = GameObject.Find("IddlePlayer").GetComponent<Animator>();
        IdSpeed = Animator.StringToHash("Speed");
        IdIsGrounded = Animator.StringToHash("isGrounded");
        IdShootArrow = Animator.StringToHash("shootArrow");
        transformArrowPoint = GameObject.Find("ArrowPoint").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {   
        MovePlayer();
        JumpPlayer();
        CheckAndSetDirection();
        ShootArrow();
    }

    private void ShootArrow()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ArrowController tempArrowController = Instantiate(arrowController,transformArrowPoint.position,
                                                                              transformArrowPoint.rotation);
            if (isFlippedInX)
            {
                tempArrowController.ArrowDirection = new Vector2(-1, 0);
                tempArrowController.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                tempArrowController.ArrowDirection = new Vector2(1, 0);

            }
            animator.SetTrigger(IdShootArrow);
        }
    }

    private void MovePlayer()
    {
        float inputX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        playerRB.velocity = new Vector2(inputX, playerRB.velocity.y);
        animator.SetFloat(IdSpeed, Mathf.Abs(playerRB.velocity.x));

    }
    private void JumpPlayer()
    {
        isGrounded = Physics2D.OverlapCircle(checkGroundPoint.position, 0.2f, selectedLayerMask);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
        }
        animator.SetBool(IdIsGrounded, isGrounded);

    }
    private bool CheckAndSetDirection()
    {
        if (playerRB.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            isFlippedInX = true;
        }

        else if (playerRB.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
            isFlippedInX = false;
        }
        return isFlippedInX;
    }
}
