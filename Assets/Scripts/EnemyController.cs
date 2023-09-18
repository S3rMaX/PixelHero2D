using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private PlayerController _playerController;
    private EnemyController _enemyController;
    [Header("Particles Properties")]
    [SerializeField] private GameObject destructionParticlePrefab;
    [SerializeField] private Color particleColor;

    [Header("Bat Properties")] 
    [SerializeField] private GameObject _batGO;
    [SerializeField] private Collider2D _batCollider2D;
    [SerializeField] private CircleCollider2D _batCircleCollider2D;
    [SerializeField] private GameObject _batEnemyGO;
    
    [Header("Bat State Machine Properties")] 
    [SerializeField] public Transform _transformPlayer;
    [SerializeField] private float _distanceFromPlayer;
    public Vector3 initialPoint;
    private Animator animator;
    private SpriteRenderer _spriteRenderer;

    [Header("Nav Mesh")] 
    [SerializeField] private Transform _transformTarget;
    private NavMeshAgent _agent;
    
    
    
    private void Awake()
    {
        _batGO = GameObject.Find("BatEnemy");
        _batEnemyGO = GameObject.FindGameObjectWithTag("Arrow");
        _batCollider2D = GetComponent<Collider2D>();
        _batCircleCollider2D = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        initialPoint = transform.position;
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        DetectDistanceWithPlayer();
    }
    public void DetectDistanceWithPlayer()
    {
        _distanceFromPlayer = Vector2.Distance(transform.position, _transformPlayer.position);
        animator.SetFloat("Distance",_distanceFromPlayer);
    }
    public void Turn(Vector3 target)
    {
        if (transform.position.x < target.x)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
    }
    public void OnArrowHit()
    {
        //Spawn destruction particles
        GameObject particles = Instantiate(destructionParticlePrefab, transform.position, Quaternion.identity);
        ParticleSystem.MainModule mainModule = particles.GetComponent<ParticleSystem>().main;
        mainModule.startColor = particleColor;
        
        Debug.Log("Enemy Death!");
    }


    public void OnPlayerCollision()
    {
        _playerController.RestartLevel();
    }
}
