using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Character.Player {
    public class PlayerContoller :ControllerOfCharacter {

        InGameManager inGameManager;

        InputController inputController;
        [Header("Controller")]
        public PlayerMoving playerMoving;
        public PlayerAnimation playerAnimation;
        public PlayerAttack playerAttack;
        public PlayerState playerState;
        public override void Init(InGameManager manager) {
            base.Init(manager);
            SetTopBottonPosition();
            inGameManager = manager;
            inputController = inGameManager.inGameUIManager.GetInput;
            playerMoving.Init(rb2d, collider2d, posBot);
            playerAttack.Init(this);

            playerState.Init();
        }
        
        private void SetTopBottonPosition() {
            posBot = collider2d.offset - new Vector2(0.0f, collider2d.size.y * 0.5f);
            posTop = collider2d.offset + new Vector2(0.0f, collider2d.size.y * 0.5f);

            
        }

        private void Update() {
            inputController.PcInput();
            playerMoving.Move(inputController.HorizontalInput, inputController.JumpInput,
                inputController.InputRunning);
            
            playerAttack.Attack(inputController.InputAttack);
            playerMoving.InputTarget = inputController.InputTarget;
            playerAttack.ArcheryUpdate();
            UpdateAnimation();
        }
        private void FixedUpdate() {
            characterBody.SyncWeaponSlot();
        }
        private void LateUpdate() {
            playerMoving.PointAtTarget(playerAttack.IsDrawingBow);
        }
        private void UpdateAnimation() {
            if (isDead) return;
            playerAnimation.SpeedVertical = playerMoving.RbVelocity.y;
            playerAnimation.MovingBlend = playerMoving.MoveBlend;
            playerAnimation.Facing = Mathf.RoundToInt(inputController.HorizontalInput);
            playerAnimation.IsGrounded = playerMoving.IsGrounded;
        }
        private void OnTriggerEnter2D(Collider2D collision) {

            if (collision.gameObject.TryGetComponent<ArrowDetect>(out ArrowDetect arrowDetect)) {
                playerState.ChangeState();
            }

        }

        public override bool IsDead {
            get { return isDead; }
            set {
                playerAttack.IsDead = value;
                playerMoving.IsDead = value;

                playerAnimation.IsDead = value;
                rb2d.simulated = !value;

                DetachWeapon();
            }
        }

    }
}