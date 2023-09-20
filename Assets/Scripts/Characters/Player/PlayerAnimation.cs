using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Character.Player {
    public class PlayerAnimation :CharacterAnimation {
        public bool IsCrouching {
            get { return isCrouching; }
            set {
                isCrouching = value;
                animator.SetBool("IsCrouching", isCrouching);
            }
        }
        [SerializeField, HideInInspector]
        private bool isCrouching;

        public AttackActionType AttackAction {
            get { return attackAction; }
            set {
                attackAction = value;

                animator.SetInteger("AttackAction", (int)attackAction);
            }
        }
        private AttackActionType attackAction = AttackActionType.ArchShoot;

    }
}