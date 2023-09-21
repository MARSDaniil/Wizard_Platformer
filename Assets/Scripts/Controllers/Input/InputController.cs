using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game {
    public class InputController :MonoBehaviour {

        InGameUIManager inGameManagerUI;
        private float horizontalInput;
        private bool jumpInput;
        private bool inputRunning;
        private bool inputCrouch;
        private bool inputAttack = false;
        private bool inputAttackContinuous = false;
        private Vector2 inputTarget;
        public float HorizontalInput {
            get { return horizontalInput; }
        }
        public bool InputRunning {
            get { return inputRunning; }
        }
        public bool InputAttack {
            get { return inputAttack; }
        }
        public bool JumpInput {
            get { return jumpInput; }
        }
        public Vector2 InputTarget {
            get { return inputTarget; }
        }
        public void Init(InGameUIManager inGameUI) {
            inGameManagerUI = inGameUI;
        }

        private void Update() {
#if UNITY_EDITOR || UNITY_STANDALONE //using pc controller
            /*
            PcInput();
            inGameManagerUI.inGameManager.playerContoller.characterMoving.Move(
                horizontalInput, jumpInput);
            inGameManagerUI.inGameManager.playerContoller.characterAttack.Attack(inputAttack, inputAttackContinuous);
            */
#endif

#if UNITY_IOS || UNITY_ANDROID //using mobile controller
                 
#endif

#if UNITY_WEBGL //using webgl build

        if (Application.isMobilePlatform == true) //catch mobile device
        {
        //mobile
        }
        else
        {
        //pc
        }
#endif


        }

        public void PcInput() {
            horizontalInput = Input.GetAxis("Horizontal");
            jumpInput = Input.GetKeyDown(KeyCode.Space);
            /*
            if (Input.GetKeyDown(KeyCode.S)) {
                inGameManagerUI.inGameManager.playerContoller.characterMoving.ChangeCrouching(true);
            }
            else if (Input.GetKeyUp(KeyCode.S)) {
                inGameManagerUI.inGameManager.playerContoller.characterMoving.ChangeCrouching(false);
            }
            */
            if (Input.GetKeyDown(KeyCode.LeftShift)) inputRunning = true;
            else if (Input.GetKeyUp(KeyCode.LeftShift)) inputRunning = false;

            inputAttack = Input.GetKey(KeyCode.Mouse0);

            inputTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}