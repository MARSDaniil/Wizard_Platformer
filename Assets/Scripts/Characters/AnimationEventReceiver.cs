using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Character {
    public class AnimationEventReceiver :MonoBehaviour {
        private ControllerOfCharacter controller;

        private void Awake() {
            controller = GetComponentInParent<ControllerOfCharacter>();
        }
        
        public void OnFootstep(AnimationEvent evt) {
          //  controller.OnFootstep(evt);
        }

        
        #region - ATTACK EVENTS

        public void OnAttackStart() {
            controller.characterAttack.OnAttackStart();
        }
        public void OnAttackHit() {
            controller.characterAttack.OnAttackHit();
        }
        public void OnAttackEnd() {
            controller.characterAttack.OnAttackEnd();
        }

        public void OnThrow() {
            controller.characterAttack.OnThrow();
        }

        #endregion

        #region - ARCHERY EVENTS -

        public void OnArrowDraw() {
            if (controller) controller.characterAttack.OnArrowDraw();
        }

        public void OnArrowNock() {
            if (controller) controller.characterAttack.OnArrowNock();
        }

        public void OnArrowReady() {
            if (controller) controller.characterAttack.OnArrowReady();
        }

        public void OnArrowPutBack() {
            if (controller) controller.characterAttack.OnArrowPutBack();
        }

        #endregion
    }
}