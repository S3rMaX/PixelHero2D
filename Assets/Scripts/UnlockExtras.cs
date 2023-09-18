using UnityEngine;

public class UnlockExtras : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerExtrasTracker playerExtrasTracker;
    [SerializeField] private bool canDoubleJump, canDash, canEnterBallModeandDropBombs;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerExtrasTracker = player.GetComponent<PlayerExtrasTracker>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SetTracker();
        }
        Destroy(gameObject);
    }

    private void SetTracker()
    {
        //if (canDoubleJump) playerExtrasTracker.CanDoubleJump = true;
        if (canDoubleJump) playerExtrasTracker.DoubleJump = true;
        //if (canDash) playerExtrasTracker.CanDash = true;
        if (canDash) playerExtrasTracker.Dash = true;
        //if (canEnterBallMode) playerExtrasTracker.CanEnterBallMode = true;
        if (canEnterBallModeandDropBombs) playerExtrasTracker.BallModeandDropBombs = true;
        //if (canDropBomb) playerExtrasTracker.CanDropBomb = true;
        
    }
}
