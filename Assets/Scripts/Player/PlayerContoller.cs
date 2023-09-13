using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Character.Player {
    public class PlayerContoller :ControllerOfCharacter {
        [Header("Manager")]
        [Space]
        [Header("Controller")]
        public PlayerMoving playerMoving;
       
        public void Init() {
            SetTopBottonPosition();
            playerMoving.Init(rb2d, collider2d, posBot,
                characterInfo.jumpSpeed, characterInfo.jumpCooldown,
                characterInfo.jumpGravityMutiplier,characterInfo.fallGravityMutiplier
                ,characterInfo.runSpeedMax
                ,characterInfo.groundCheckRadius
                );
        }
        
        private void SetTopBottonPosition() {
            posBot = collider2d.offset - new Vector2(0.0f, collider2d.size.y * 0.5f);
            posTop = collider2d.offset + new Vector2(0.0f, collider2d.size.y * 0.5f);
        }

        
    }
}