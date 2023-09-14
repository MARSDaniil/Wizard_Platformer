using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Component;
namespace Game.Character {
    public class CharacterMoving :Move {
        public MovementType defaultMovement = MovementType.Walk;
        [SerializeField] protected CharacterInfo characterInfo;
        protected Collider2D collider2D { get; set; }
        protected Vector2 posBot;
        protected bool isGrounded = true;

        private bool inputRunning = false;
        
        protected bool isDead = false;

        protected bool inputCrouch = false;
        public virtual void Init(Rigidbody2D rigidbody, Collider2D collider, Vector2 BotPos) {
            rb2d = rigidbody;
            collider2D = collider;
            posBot = BotPos;
        }
        public virtual void Move(float hInput,  bool jInput) {
            if (isDead) return;
            
            Vector2 curVel = rb2d.velocity;

            float acc = 0.0f;
            float max = 0.0f;
            float brakeAcc = 0.0f;

            
            if (isGrounded) {
                acc = inputRunning ? characterInfo.runAcc : characterInfo.walkAcc;
                max = inputRunning ? characterInfo.runSpeedMax : characterInfo.walkSpeedMax;
                brakeAcc = characterInfo.groundBrakeAcc;

                if (inputCrouch) {
                    acc = characterInfo.crouchAcc;
                    max = characterInfo.crouchSpeedMax;
                }
            }

            if (Mathf.Abs(hInput) > 0.01f) {
                //if current horizontal speed is out of allowed range, let it fall to the allowed range
                bool shouldMove = true;
                if (hInput > 0 && curVel.x >= max) {
                    curVel.x = Mathf.MoveTowards(curVel.x, max, brakeAcc * Time.deltaTime);
                    shouldMove = false;
                }
                if (hInput < 0 && curVel.x <= -max) {
                    curVel.x = Mathf.MoveTowards(curVel.x, -max, brakeAcc * Time.deltaTime);
                    shouldMove = false;
                }

                //otherwise, add movement acceleration to cureent velocity
                if (shouldMove) curVel.x += acc * Time.deltaTime * hInput;
            }
            //no horizontal movement input, brake to speed zero
            else {
                curVel.x = Mathf.MoveTowards(curVel.x, 0.0f, brakeAcc * Time.deltaTime);
            }

            if (isGrounded && jInput) {
                isGrounded = false;

                curVel.y += characterInfo.jumpSpeed;
            }
            if (jInput && curVel.y > 0) {
                curVel.y += Physics.gravity.y * (characterInfo.jumpGravityMutiplier - 1.0f) * Time.deltaTime;
            }
            else if (curVel.y > 0) {
                curVel.y += Physics.gravity.y * (characterInfo.jumpGravityMutiplier - 1.0f) * Time.deltaTime;
            }
            SetRbVelocity(curVel);
        }
        private void Update() {
            CheckIsGrounded();
        }

        private void CheckIsGrounded() {
            isGrounded = false;
            Vector2 worldPos = transform.position;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPos + posBot, characterInfo.groundCheckRadius);
            for (int i = 0; i < colliders.Length; i++) {
                if (colliders[i].isTrigger) continue;
                if (colliders[i].gameObject != gameObject) isGrounded = true;
            }
        }

        public enum MovementType {
            Walk,
            Run
        }
        private void OnDrawGizmosSelected() {
            //Draw the ground detection circle
            Gizmos.color = Color.white;
            Vector2 worldPos = transform.position;
            Gizmos.DrawWireSphere(worldPos + posBot, characterInfo.groundCheckRadius);
        }

        public void ChangeIsGrounded(bool value) => isGrounded = value;

        public void ChageRun(bool value) => inputRunning = value;
        public void ChangeCrouching(bool value) => inputCrouch = value;
    }
}