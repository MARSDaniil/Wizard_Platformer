using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Character.Player {
    public class PlayerMoving :CharacterMoving {
        // Start is called before the first frame update
        /*
       public void Init(Rigidbody2D rigidbody, Collider2D collider, Vector2 BotPos) {
            base.Init();

        }
        */
        PlayerContoller playerContoller;
        public override void Init(Rigidbody2D rigidbody, Collider2D collider, Vector2 BotPos) {
            playerContoller = GetComponent<PlayerContoller>();
            base.Init(rigidbody, collider, BotPos);
        }
        
        public override void Move(float hInput, bool jInput, bool run) {
            base.Move(hInput, jInput,run);

           
        }

        
    }
}