using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Character.Player;
namespace Game {
    public class InGameManager :MonoBehaviour {
        [Header("Manager")]
        public InGameUIManager inGameUIManager;
        [Space]
        [Header("Player")]
        public PlayerContoller playerContoller;

     

        private void Awake() {
            Init();
        }

        private void Init() {
            inGameUIManager.Init();
            playerContoller.Init();
        }
    }
}
