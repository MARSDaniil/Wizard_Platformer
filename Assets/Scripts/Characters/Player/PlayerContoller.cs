using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Character.Player {
    public class PlayerContoller :ControllerOfCharacter {
        

        public void Init() {
            SetTopBottonPosition();
            characterMoving.Init(rb2d, collider2d, posBot);
            characterAttack.Init(this);
        }
        
        private void SetTopBottonPosition() {
            posBot = collider2d.offset - new Vector2(0.0f, collider2d.size.y * 0.5f);
            posTop = collider2d.offset + new Vector2(0.0f, collider2d.size.y * 0.5f);
        }

        
        
    }
}