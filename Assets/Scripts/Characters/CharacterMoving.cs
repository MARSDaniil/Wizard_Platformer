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

        public int facing = 1;
        #region - MOVE -
        private float targetMoveBlend;
        protected float moveBlend;

        public bool IsMoving {
            get { return isMoving; }
        }
        public bool IsRunning {
            get { return isRunning; }
            set { isRunning = value; }
        }


        protected bool isMoving;                                      
        protected bool isRunning; 

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

            isMoving = (Mathf.Abs(hInput) > 0.05f);

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
            UpdateMoveBlend();
            RbVelocity = curVel;
        }

        private void UpdateMoveBlend() {
            if (isMoving) {
                targetMoveBlend = 1.0f;
                if (inputRunning) targetMoveBlend = 3.0f;
            }
            else {
                targetMoveBlend = 0.0f;
            }

            moveBlend = Mathf.Lerp(moveBlend, targetMoveBlend, 7.0f * Time.deltaTime);
        }

        public void ChageRun(bool value) => inputRunning = value;
        #endregion


        #region - JUMP -
        /*
        //the actual jump cooldown, used for settings a minimal jump cooldown value, as it can not be too small
        private float JumpCoolDown {
            get { return Mathf.Max(0.05f, jumpCooldown); }
        }

        private void StartJumpCheck() {
            if (jumpEnabled == false || IsDead) return;

            //disable jump while crawling or dodging
            if (isCrawling || isCrawlEntering || isCrawlExiting || isDodging) {
                jumpCdTimer = 0.0f;
                return;
            }

            //jump cooldown
            if (IsInAir == false && jumpCdTimer < JumpCoolDown) jumpCdTimer += Time.deltaTime;

            //start jump
            if (inputJump && jumpCdTimer >= JumpCoolDown) {
                //jump from ground
                //also able to jump within air time tolerance
                if (isGrounded || (0 < airTimer && airTimer <= jumpTolerance)) {
                    IsGrounded = false;
                    IsClimbingLadder = false;

                    jumpCdTimer = 0.0f;

                    //mix surface normal to jump direction
                    Vector2 jumpDir = Vector2.up;
                    float surfaceNormalMix = Mathf.Lerp(0.0f, 1.0f, surfaceAngle / 90.0f);
                    jumpDir = Vector2.Lerp(Vector2.up, surfaceNormal, surfaceNormalMix).normalized;
                    startJumpVel = jumpSpeed * jumpDir;
                }

                //jump from ladder
                if (IsClimbingLadder) {
                    IsGrounded = false;
                    isEnteringLadder = false;
                    isExitingLadder = false;
                    IsClimbingLadder = false;

                    jumpCdTimer = 0.0f;

                    //mix ladder direction or move direction to jump direction
                    Vector2 jumpDir = Vector2.up;
                    if (Mathf.Abs(inputMove.x) < MOVE_THRESHOLD) {
                        jumpDir += new Vector2((int)ladder.direction, 0.0f) * 0.25f;
                    }
                    else {
                        jumpDir += new Vector2(Mathf.Sign(inputMove.x), 0.0f) * 0.5f;
                    }
                    jumpDir = jumpDir.normalized;
                    startJumpVel = jumpSpeed * jumpDir;


                }

                //jump while entering or exiting climbing 
                if (isEnteringLadder || isExitingLadder) {
                    IsGrounded = false;
                    isEnteringLadder = false;
                    isExitingLadder = false;
                    IsClimbingLadder = false;

                    jumpCdTimer = 0.0f;

                    Vector2 jumpDir = Vector2.up;
                    jumpDir += new Vector2(-facing, 0.0f) * 0.5f;
                    jumpDir = jumpDir.normalized;

                    startJumpVel = jumpSpeed * jumpDir;

                }

                //jump while climbing ledge
                if (isClimbingLedge || ledgeClimbLocked) {
                    IsGrounded = false;
                    isClimbingLedge = false;
                    ledgeClimbLocked = false;
                    jumpCdTimer = 0.0f;

                    Vector2 jumpDir = Vector2.up;
                    jumpDir += new Vector2(-facing, 0.0f) * 0.5f;
                    jumpDir = jumpDir.normalized;

                    startJumpVel = jumpSpeed * jumpDir;
                }
            }

        }

        private void JumpUpdate() {
            if (jumpEnabled == false) return;

            //apply start jump vel
            if (startJumpVel.magnitude > 0.01f) {
                Vector2 jumpDir = startJumpVel.normalized;
                float dot = Vector2.Dot(velocity, jumpDir);
                if (dot < 0) velocity -= dot * jumpDir;

                velocity += startJumpVel;
                if (velocity.y > startJumpVel.y * 1.25f) velocity.y = startJumpVel.y * 1.25f;
                startJumpVel = Vector2.zero;

                //apply jump force to standing collider
                Vector2 force = jumpSpeed * CHARACTER_WEIGHT * Physics2D.gravity / standingColliders.Count;
                for (int i = 0; i < standingColliders.Count; i++) {
                    if (standingColliders[i].attachedRigidbody) standingColliders[i].attachedRigidbody.AddForceAtPosition(force, standingPosList[i]);
                }

                //event
                onJump.Invoke();
            }


            //jumping up with continuous jump input
            //set jump gravity so that the longer the jump key is pressed, the higher the character can jump
            if (IsInAir) {
                if (inputJump && velocity.y > 0) {
                    velocity.y += Physics2D.gravity.y * (jumpGravityMutiplier - 1.0f) * Time.fixedDeltaTime;
                }
                //jumping up without input
                else if (velocity.y > 0.01f) {
                    velocity.y += Physics2D.gravity.y * (fallGravityMutiplier - 1.0f) * Time.fixedDeltaTime;
                }
            }
        }
        */
        #endregion

        public bool IsGrounded {
            get { return isGrounded; }
            private set {
                if (isGrounded == value) return;
                isGrounded = value;
            }
        }
        public bool isGrounded;

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

        
        public void ChangeCrouching(bool value) => inputCrouch = value;
    }
}