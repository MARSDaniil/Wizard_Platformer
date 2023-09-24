using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Character.Player {
    public class PlayerState :CharacterState {
        // Start is called before the first frame update
        PlayerContoller playerContoller;
        

        public override void Init() {
            base.Init();
            playerContoller = GetComponent<PlayerContoller>();
            if (playerContoller == null) Debug.Log("controller null in player state");
        }

        public override void Dead() {
            playerContoller.IsDead = true;
        }
    }
}