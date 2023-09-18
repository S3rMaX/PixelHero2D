using System;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public static ItemsManager instance;
    
    [SerializeField] private PlayerExtrasTracker _playerExtrasTracker;
    
    [HideInInspector] public int heartsToUnlockDoubleJump = 5;
    [HideInInspector] public int coinsSpinningToUnlockDash = 6;
    [HideInInspector] public int coinsShiningToUnlockBallModeAndDropBombs = 10;

    private void Awake()
    {
        instance = this;
    }

    /*private void Start()
    {
        _playerExtrasTracker = GetComponent<PlayerExtrasTracker>();
    }*/

    public void CountDownItem(string itemType)
    {
        if (itemType == "HeartShining" && heartsToUnlockDoubleJump > 0)
        {
            heartsToUnlockDoubleJump--;
            Debug.Log("HeartShining restantes" + " = " + heartsToUnlockDoubleJump);
        }

        if (itemType == "CoinSpin" && coinsSpinningToUnlockDash > 0)
        {
            coinsSpinningToUnlockDash--;
            Debug.Log("CoinsSpin restantes" + " = " + coinsSpinningToUnlockDash);
        }

        if (itemType == "CoinShine" && coinsShiningToUnlockBallModeAndDropBombs > 0)
        {
            coinsShiningToUnlockBallModeAndDropBombs--;
            Debug.Log("CoinShine restantes" + " = " + coinsShiningToUnlockBallModeAndDropBombs);
        }
        ActivatePowerUp();
    }

    public void ActivatePowerUp()
    {
        if (heartsToUnlockDoubleJump <= 0)
        {
            _playerExtrasTracker.DoubleJump = true;
        }

        if (coinsSpinningToUnlockDash <= 0)
        {
            _playerExtrasTracker.Dash = true;
        }

        if (coinsShiningToUnlockBallModeAndDropBombs <= 0)
        {
            _playerExtrasTracker.BallModeandDropBombs = true;
        }
    }

    /*[SerializeField] private int heartsToUnlockDoubleJump = 5;
    [SerializeField] private int coinsToUnlockDash = 6;
    [SerializeField] private int coinsToUnlockBallModeAndBombs = 10;

    private int collectedHearts = 0;
    private int collectedCoins = 0;
    
    private bool doubleJumpUnlocked = false;
    private bool dashUnlocked = false;
    private bool ballModeAndBombsUnlocked = false;

    private PlayerExtrasTracker _playerExtrasTracker;

    private void Start()
    {
        _playerExtrasTracker = GetComponent<PlayerExtrasTracker>();
    }

    public void CollectHeart()
    {
        collectedHearts++;
        if (collectedHearts >= heartsToUnlockDoubleJump & !doubleJumpUnlocked)
        {
            doubleJumpUnlocked = true;
            _playerExtrasTracker.UnlockDoubleJump();
        }
    }

    public void CollectCoin()
    {
        collectedCoins++;
        if (collectedCoins >= coinsToUnlockDash && !dashUnlocked)
        {
            dashUnlocked = true;
            _playerExtrasTracker.UnlockDash();
        }

        if (collectedCoins >= coinsToUnlockBallModeAndBombs && !ballModeAndBombsUnlocked)
        {
            ballModeAndBombsUnlocked = true;
            _playerExtrasTracker.UnlockBallModeAndBombs();
        }
    }*/
}
