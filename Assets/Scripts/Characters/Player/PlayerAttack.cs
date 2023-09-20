using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Character.Player {
    public class PlayerAttack :CharacterAttack {
        
        public void Init(ControllerOfCharacter character) {
            base.Init(character);
            
        }

        public void Attack(bool inputAttack, bool inputAttackContinuous) {
            base.Attack(inputAttack, inputAttackContinuous);
           // playerContoller.playerAnimation.IsAttacking = inputAttackContinuous;
        }
    }
}