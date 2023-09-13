using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game {
    public class InputController :MonoBehaviour {

        InGameUIManager inGameManagerUI;
        private float horizontalInput;
        private bool jumpInput;
        public void Init(InGameUIManager inGameUI) {
            inGameManagerUI = inGameUI;
        }

        private void Update() {
#if UNITY_EDITOR || UNITY_STANDALONE //using pc controller
            PcInput();
            inGameManagerUI.inGameManager.playerContoller.playerMoving.Move(horizontalInput, jumpInput);
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
        }
    }
}