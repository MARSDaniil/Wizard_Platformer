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

            if (isGrounded) playerContoller.playerAnimation.IsCrouching = inputCrouch;

            playerContoller.playerAnimation.SpeedVertical = rb2d.velocity.y;
            playerContoller.playerAnimation.MovingBlend = Mathf.Abs(rb2d.velocity.x) / characterInfo.runSpeedMax;
            playerContoller.playerAnimation.Facing = Mathf.RoundToInt(hInput);

            playerContoller.playerAnimation.IsGrounded = isGrounded;
        }
    }
}