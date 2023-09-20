using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Character.Player {
    public class PlayerMoving :CharacterMoving {
        // Start is called before the first frame update
        /*
       public void Init(Rigidbody2D rigidbody, Collider2D collider, Vector2 BotPos) {
            base.Init();

        }
        */
        PlayerContoller playerContoller;
        public void Init(Rigidbody2D rigidbody, Collider2D collider, Vector2 BotPos) {
            playerContoller = GetComponent<PlayerContoller>();
            base.Init(rigidbody, collider, BotPos);
        }
        
        public void Move(float hInput, bool jInput) {
            base.Move(hInput, jInput);

           // if (IsGrounded) playerContoller.playerAnimation.IsCrouching = inputCrouch;

            playerContoller.characterAnimation.SpeedVertical = rb2d.velocity.y;
            playerContoller.characterAnimation.MovingBlend = moveBlend;
            playerContoller.characterAnimation.Facing = Mathf.RoundToInt(hInput);
            playerContoller.characterAnimation.IsGrounded = IsGrounded;
        }
    }
}