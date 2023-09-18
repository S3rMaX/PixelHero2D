using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    
    //Player Movement
    [Header("Player Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask selectedLayerMask;
    public bool isFacingRight = true;
    private Rigidbody2D playerRB;
    [SerializeField] private Transform checkGroundPoint;
    private Transform transformPlayerController;
    private bool isGrounded;
    private Animator animatorStandingPlayer;
    private Animator animatorBallPlayer;
    private int IdSpeed, IdIsGrounded, IdShootArrow, IdCanDoubleJump;
    private float ballModeCounter;
    [SerializeField] private float waitForBallMode;
    [SerializeField] private float isGroundedRange;

    //Player Shoot
    [Header("Player Shoot")]
    [SerializeField] private ArrowController arrowController;
    private Transform transformArrowPoint;
    private Transform transfromBombPoint;
    [SerializeField] private GameObject prefabBomb;

    //Player Dust
    [Header("Player Dust")]
    [SerializeField]private GameObject dustJump;
    private Transform transformDustPoint;
    private bool isIdle, canDoubleJump;

    //Player Dash
    [Header("Player Dash")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    private float dashCounter;
    [SerializeField] private float waitForDash;
    private float afterDashCounter;

    //Player Dash After Image
    [Header("Player Dash After Image")]
    [SerializeField] private SpriteRenderer playerSR;
    [SerializeField] private SpriteRenderer afterImageSR;
    [SerializeField] private float afterImageLifeTime;
    [SerializeField] private Color afterImageColor;
    [SerializeField] private float afterImageTimeBetween;
    private float afterImageCounter;

    //Player Sprites
    [SerializeField] private GameObject standingPlayer;
    [SerializeField] private GameObject ballPlayer;

    //Player Extras
    private PlayerExtrasTracker playerExtrasTracker;
    
    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        transformPlayerController = GetComponent<Transform>();
        playerExtrasTracker = GetComponent<PlayerExtrasTracker>();
        instance = this;
    }

    private void Start()
    {
        standingPlayer = GameObject.Find("IddlePlayer");
        ballPlayer = GameObject.Find("BallPlayer");
        ballPlayer.SetActive(false);
        transfromBombPoint = GameObject.Find("BombPoint").GetComponent<Transform>();
        animatorStandingPlayer = standingPlayer.GetComponent<Animator>();
        animatorBallPlayer = ballPlayer.GetComponent<Animator>();
        IdSpeed = Animator.StringToHash("speed");
        IdIsGrounded = Animator.StringToHash("isGrounded");
        IdShootArrow = Animator.StringToHash("shootArrow");
        IdCanDoubleJump = Animator.StringToHash("canDoubleJump");
        transformArrowPoint = GameObject.Find("ArrowPoint").GetComponent<Transform>();
        transformDustPoint = GameObject.Find("DustPoint").GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        Dash();
        JumpPlayer();
        Shoot();
        PlayDust();
        BallMode();
    }
    private void Dash()
    {
        if (afterDashCounter > 0)
            afterDashCounter -= Time.deltaTime;
        else
        {
            //if ((Input.GetButtonDown("Fire2") && standingPlayer.activeSelf) && playerExtrasTracker.CanDash)
            if ((Input.GetButtonDown("Fire2") && standingPlayer.activeSelf) && playerExtrasTracker.Dash)
            {
                dashCounter = dashTime;
                //ShowAfterImage();
            }
        }
        
        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if (isFacingRight)
            {
                playerRB.velocity = new Vector2(dashSpeed * transformPlayerController.localScale.x,playerRB.velocity.y);
            }
            else
            {
                playerRB.velocity = new Vector2(dashSpeed * (transformPlayerController.localScale.x * -1),playerRB.velocity.y);
            }
            afterImageCounter -= Time.deltaTime;
            if (afterImageCounter <= 0)
            {
                ShowAfterImage();
                FlipDirection();
            }
            afterDashCounter = waitForDash;
        }
        else
        {
            MovePlayer();
        }
    }
    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1") && standingPlayer.activeSelf)
        {
            //Instantiate(arrowController, transformArrowPoint.position, transformArrowPoint.rotation);
            GameObject arrow = ArrowPool.Instance.RequestArrow();
            arrow.transform.position = transformArrowPoint.position;
            animatorStandingPlayer.SetTrigger(IdShootArrow);
        }

        if ((Input.GetButtonDown("Fire1") && ballPlayer.activeSelf) && playerExtrasTracker.BallModeandDropBombs)
                Instantiate(prefabBomb, transformPlayerController.position, Quaternion.identity);
    }
    private void FlipDirection()
    {
        if (playerRB.velocity.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (playerRB.velocity.x < 0 && isFacingRight)
        {
            Flip();
        }
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transformPlayerController.Rotate(0f, 180f, 0f);
    }
    private void MovePlayer()
    {
        float inputX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        playerRB.velocity = new Vector2(inputX, playerRB.velocity.y);
        if (standingPlayer.activeSelf)
        {
            animatorStandingPlayer.SetFloat(IdSpeed, Mathf.Abs(playerRB.velocity.x));
        }
        if (ballPlayer.activeSelf)
        {
            animatorBallPlayer.SetFloat(IdSpeed, Mathf.Abs(playerRB.velocity.x));
        }
        FlipDirection();
    }
    private void JumpPlayer()
    {
        //isGrounded = Physics2D.OverlapCircle(checkGroundPoint.position, 0.2f, selectedLayerMask);
        isGrounded = Physics2D.Raycast(checkGroundPoint.position, Vector2.down, isGroundedRange, selectedLayerMask);
        //if (Input.GetButtonDown("Jump") && (isGrounded || canDoubleJump && playerExtrasTracker.CanDoubleJump))
        if (Input.GetButtonDown("Jump") && (isGrounded || canDoubleJump && playerExtrasTracker.DoubleJump))
        {
            if (isGrounded)
            {
                canDoubleJump = true;
                Instantiate(dustJump, transformDustPoint.position, Quaternion.identity);
            }
            else
            {
                canDoubleJump = false;
                animatorStandingPlayer.SetTrigger(IdCanDoubleJump);
            }
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
        }
        animatorStandingPlayer.SetBool(IdIsGrounded, isGrounded);
    }
    private void PlayDust()
    {
        if (playerRB.velocity.x != 0 && isIdle)
        {
            isIdle = false;
            if (isGrounded)
                Instantiate(dustJump, transformDustPoint.position, Quaternion.identity);
        }

        if (playerRB.velocity.x == 0)
        {
            isIdle = true;
        }
    }
    private void ShowAfterImage()
    {
        SpriteRenderer afterImage = Instantiate(afterImageSR,transformPlayerController.position,transformPlayerController.rotation);
        afterImage.sprite = playerSR.sprite;
        afterImage.transform.localScale = transformPlayerController.localScale;
        afterImage.color = afterImageColor;
        Destroy(afterImage.gameObject, afterImageLifeTime);
        afterImageCounter = afterImageTimeBetween;
    }
    private void BallMode()
    {
        float inputVertical = Input.GetAxisRaw("Vertical");
        //if ((inputVertical <= -.9f && !ballPlayer.activeSelf || inputVertical >= .9f && ballPlayer.activeSelf) && playerExtrasTracker.CanEnterBallMode)
        if ((inputVertical <= -.9f && !ballPlayer.activeSelf || inputVertical >= .9f && ballPlayer.activeSelf) && playerExtrasTracker.BallModeandDropBombs)
        
        {
            ballModeCounter -= Time.deltaTime;
            if(ballModeCounter < 0)
            {
                ballPlayer.SetActive(!ballPlayer.activeSelf);
                standingPlayer.SetActive(!standingPlayer.activeSelf);
            }
        }
        else
        {
            ballModeCounter = waitForBallMode;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(checkGroundPoint.position, isGroundedRange);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BatEnemy"))
        {
            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.OnPlayerCollision();
            }
        }
    }
}