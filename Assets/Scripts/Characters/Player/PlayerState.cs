using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Character.Player {
    public class PlayerState :CharacterState {
        // Start is called before the first frame update
        PlayerContoller playerContoller;
        

        public override void Init(PlayerContoller contoller) {
            base.Init();
            playerContoller = contoller;
        }

        public override void Dead() {

        }
    }
}