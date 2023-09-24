using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Character {
    public class CharacterAnimation :MonoBehaviour {
        public Animator animator;

        public int Facing {
            get { return facing; }
            set {
                if (IsDead == true) return;
                if (value == 0) return;
                facing = value;

                animator.transform.localScale = new Vector3(1.0f, 1.0f, facing);

                Vector3 pos = animator.transform.localPosition;
                pos.x = 0.064f * -facing;
                animator.transform.localPosition = pos;
            }
        }
        [SerializeField, HideInInspector]
        private int facing = 1;

        public float MovingBlend {
            get {
                return movingBlend;
            }
            set {
                movingBlend = value;
                animator.SetFloat("MoveBlend", movingBlend);
            }
        }
        [SerializeField, HideInInspector]
        private float movingBlend;
        public void InjuredFront() {
            animator.SetTrigger("InjuredFront");
        }
        public void InjuredBack() {
            animator.SetTrigger("InjuredBack");
        }
       
        public float SpeedVertical {
            get { return speedVertical; }
            set {
                speedVertical = value;
                
                animator.SetFloat("VelocityY", speedVertical);
            }
        }
        private float speedVertical;

        public bool IsGrounded {
            get { return isGrounded; }
            set {
                isGrounded = value;
                animator.SetBool("IsGrounded", isGrounded);
            }
        }
        [SerializeField, HideInInspector]
        private bool isGrounded;
        

        public bool IsAttacking {
            get { return isAttacking; }
            set {
                isAttacking = value;
                animator.SetBool("IsAttacking", isAttacking);
            }
        }
        [SerializeField, HideInInspector]
        private bool isAttacking;

        public bool IsDead {
            get { return isDead; }
            set {
                isDead = value;
                animator.SetBool("IsDead", value);
            }
        }
        private bool isDead = false;
        public bool SetIsDrawingBow {
            set { animator.SetBool("IsDrawingBow", value); }
        }  
        public bool SetIsArrowDrawn {
            set { animator.SetBool("IsArrowDrawn", value); }
        }
        public bool SetIsAttacking {
            set { animator.SetBool("IsAttacking", value); }
        }
        public int SetAttackAction {
            set { animator.SetInteger("AttackAction", value); }
        }

        public enum ExpressionType {
            Normal,
            Injured,
            Dead,
            Shocked,
            Happy,
            Sad,
            Shy,
            Sick,
            CatFace
        }

        public enum AttackActionType {
            Swipe,
            Stab,
            Point,
            Summon,
            MageShoot,
            ArchShoot
        }
    }
}