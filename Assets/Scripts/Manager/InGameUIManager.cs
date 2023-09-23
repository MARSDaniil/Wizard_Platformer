using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.UI.InGame;
namespace Game {
    public class InGameUIManager :MonoBehaviour {
        [Header("InputSystem")]
        public InGameManager inGameManager;
        [Space]
        [Header("InputSystem")]
        [SerializeField] InputController inputController;
        [Header("InGame UI")]
        public PlayUI playUI;
        public void Init() {
            inputController.Init(this);
            
            //init UI
            playUI.Init(true);
        }

        public InputController GetInput {
            get { return inputController; }
        }
    }
}