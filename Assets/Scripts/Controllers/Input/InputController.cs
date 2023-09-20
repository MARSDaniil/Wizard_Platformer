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
        public void Init(InGameUIManager inGameUI) {
            inGameManagerUI = inGameUI;
        }

        private void Update() {
#if UNITY_EDITOR || UNITY_STANDALONE //using pc controller
            PcInput();
            inGameManagerUI.inGameManager.playerContoller.characterMoving.Move(
                horizontalInput, jumpInput);
            inGameManagerUI.inGameManager.playerContoller.characterAttack.Attack(inputAttack, inputAttackContinuous);
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

        private void PcInput() {
            horizontalInput = Input.GetAxis("Horizontal");
            jumpInput = Input.GetKeyDown(KeyCode.Space);

            if (Input.GetKeyDown(KeyCode.S)) {
                inGameManagerUI.inGameManager.playerContoller.characterMoving.ChangeCrouching(true);
            }
            else if (Input.GetKeyUp(KeyCode.S)) {
                inGameManagerUI.inGameManager.playerContoller.characterMoving.ChangeCrouching(false);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift)) {
                inGameManagerUI.inGameManager.playerContoller.characterMoving.ChageRun(true);
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift)) {
                inGameManagerUI.inGameManager.playerContoller.characterMoving.ChageRun(false);
            }
            /*
            //attack
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                inputAttack = Input.GetKeyDown(KeyCode.Mouse0);
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0)) {
                inputAttack = Input.GetKeyUp(KeyCode.Mouse0);
                inGameManagerUI.inGameManager.playerContoller.characterAttack.IsDrawingBow = false;
            }
            else if(Input.GetKey(KeyCode.Mouse0)) {
                inputAttackContinuous = Input.GetKey(KeyCode.Mouse0);
            }
            */
            inputAttack = Input.GetKey(KeyCode.Mouse0);
        }
    }
}