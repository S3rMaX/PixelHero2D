using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExtrasTracker : MonoBehaviour
{
   public static PlayerExtrasTracker instance;
   
   [SerializeField] private bool _doubleJump, _dash, _ballModeandDropBombs;
   
   public bool DoubleJump { get => _doubleJump; set => _doubleJump = value; }
   public bool Dash { get => _dash; set => _dash = value; }
   public bool BallModeandDropBombs { get => _ballModeandDropBombs; set => _ballModeandDropBombs = value; }

   /*public bool doubleJumpUnlocked = false;
   public bool dashUnlocked = false;
   public bool ballModeAndDropBombsUnlocked = false;*/


   /*public void UnlockDoubleJump()
   {
      doubleJumpUnlocked = true;
      Debug.Log("Double Jump unlocked!");
   }

   public void UnlockDash()
   {
      dashUnlocked = true;
      Debug.Log("Dash Unlocked!");
   }

   public void UnlockBallModeAndBombs()
   {
      ballModeAndBombsUnlocked = true;
      Debug.Log("Ball Mode and Bombs unlocked!");
   }*/
}
