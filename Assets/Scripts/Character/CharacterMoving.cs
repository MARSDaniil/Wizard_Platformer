using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Component;
namespace Game.Character {
    public class CharacterMoving :Move {
        private Collider2D collider2D { get; set; }
        private Vector2 posBot;
        private bool isGrounded = true;

        private float jumpSpeed;
        private float jumpCooldown;
        private float jumpGravityMutiplier;
        private float fallGravityMutiplier;
        
        private float maxSpeed;

        private float groundCheckRadius;
        public void Init(Rigidbody2D rigidbody, Collider2D collider, Vector2 BotPos
            , float JumpSpeed, float JumpCooldown, float JumpGravityMutiplier, float FallGravityMutiplier
            , float MaxSpeed
            , float GroundCheckRadius
            ) {
            rb2d = rigidbody;
            collider2D = collider;
            posBot = BotPos;

            //jump parametrs
            jumpSpeed = JumpSpeed;
            jumpCooldown = JumpCooldown;
            jumpGravityMutiplier = JumpGravityMutiplier;
            fallGravityMutiplier = FallGravityMutiplier;

            maxSpeed = MaxSpeed;

            groundCheckRadius = GroundCheckRadius;

        }
        public void Move(float hInput,  bool jInput) {
            Vector2 curVel = rb2d.velocity;

            if (Mathf.Abs(hInput) > 0) {
                rb2d.velocity = new Vector2(hInput * maxSpeed, 0);
                curVel.x = hInput * maxSpeed;
            }
            else {
                rb2d.velocity = new Vector2(0, 0);
                curVel.x = 0;
            }

            if (isGrounded && jInput) {
                isGrounded = false;

                curVel.y += jumpSpeed;
            }
            if (jInput && curVel.y > 0) {
                curVel.y += Physics.gravity.y * (jumpGravityMutiplier - 1.0f) * Time.deltaTime;
            }
            else if (curVel.y > 0) {
                curVel.y += Physics.gravity.y * (fallGravityMutiplier - 1.0f) * Time.deltaTime;
            }
            SetRbVelocity(curVel);
        }
        private void Update() {
            CheckIsGrounded();
        }

        private void CheckIsGrounded() {
            isGrounded = false;
            Vector2 worldPos = transform.position;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPos + posBot, groundCheckRadius);
            for (int i = 0; i < colliders.Length; i++) {
                if (colliders[i].isTrigger) continue;
                if (colliders[i].gameObject != gameObject) isGrounded = true;
            }
        }

        public void ChangeIsGrounded(bool value) => isGrounded = value; 
    }
}