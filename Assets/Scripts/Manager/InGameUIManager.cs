using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game {
    public class InGameUIManager :MonoBehaviour {
        [Header("InputSystem")]
        public InGameManager inGameManager;
        [Space]
        [Header("InputSystem")]
        [SerializeField] InputController inputController;

        public void Init() {
            inputController.Init(this);
        }

        public InputController GetInput {
            get { return inputController; }
        }
    }
}