using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Character.Player {
    public class PlayerContoller :ControllerOfCharacter {

        InGameManager inGameManager;

        InputController inputController;
        [Header("Controller")]
        [SerializeField] PlayerMoving playerMoving;
        [SerializeField] PlayerAnimation playerAnimation;
        [SerializeField] PlayerAttack playerAttack;
        public override void Init(InGameManager manager) {
            base.Init(manager);
            SetTopBottonPosition();
            inGameManager = manager;
            inputController = inGameManager.inGameUIManager.GetInput;
            playerMoving.Init(rb2d, collider2d, posBot);
            playerAttack.Init(this);
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
            //move 
            playerAnimation.SpeedVertical = playerMoving.RbVelocity.y;
            playerAnimation.MovingBlend = playerMoving.MoveBlend;
            playerAnimation.Facing = Mathf.RoundToInt(inputController.HorizontalInput);
            playerAnimation.IsGrounded = playerMoving.IsGrounded;
        }
        

    }
}