using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Game.Weapons;
namespace Game.Character {
    public class CharacterAttack :MonoBehaviour {


        public float arrowSpeed = 20.0f;
        public GameObject arrowPrefab;

        
        
        




        public Animator animator;
        protected ControllerOfCharacter characterController;

        [Space]
        [Header("Events")]
        public UnityEvent onAttackStart;
        public UnityEvent onAttackHit;
        public UnityEvent onAttackEnd;
        public UnityEvent onBowPull;
        public UnityEvent onBowShoot;
        public UnityEvent onThrow;
        public bool IsCrawling {
            get {
                return isCrawling;
            }
        }
        private bool isCrawling;

        public bool IsDead {
            get { return isDead; }
            set {
                if (isDead == value) return;
                isDead = value;

                IsDrawingBow = false;
                IsArrowDrawn = false;
                IsStringPulled = false;
            }
        }
        private bool isDead;


        private Projectile projectile;
        private WeaponBow bow;
        private bool isArrowReady;


        public bool IsAttacking {
            get { return isAttacking; }
        }
        private bool isAttacking;
        public int AttackActionIndex {
            get { return attackActionIndex; }
        }
        private int attackActionIndex;

        public bool IsDrawingBow {
            get { return isDrawingBow; }
            set {
                if (isDrawingBow == value) return;
               
                isDrawingBow = value;
                
                
                if (isDrawingBow == false) {
                    //arrow ready
                    if (isArrowReady && projectile) {
                        //unable to shoot, destroy arrow projectile
                        if (isCrawling || isDead) {
                            Destroy(projectile.gameObject);
                        }
                        //shoot arrow out
                        else {
                            projectile.transform.SetParent(null, true);
                            projectile.transform.localScale = Vector3.one;
                            projectile.Launched = true;
                            projectile.Velocity = arrowSpeed * projectile.transform.right;

                            onBowShoot.Invoke();
                        }
                    }

                    isArrowReady = false;
                    IsStringPulled = false;
                }

                animator.SetBool("IsDrawingBow", isDrawingBow);
            }
        }
        private bool isDrawingBow;

        public bool IsStringPulled {
            get { return isStringPulled; }
            set {
                if (isStringPulled == value) return;
                isStringPulled = value;

                if (isDrawingBow == false) isStringPulled = false;

                bow.IsStringPulled = isStringPulled;
            }
        }
        private bool isStringPulled;

        public bool IsArrowDrawn {
            get { return isArrowDrawn; }
            set {
                isArrowDrawn = value;
                animator.SetBool("IsArrowDrawn", isArrowDrawn);
            }
        }
        private bool isArrowDrawn;

        public void OnArrowDraw() {
            if (bow == null) return;
            if (arrowPrefab == null) return;
            if (isDrawingBow == false) return;

            projectile = Instantiate(arrowPrefab, characterController.characterBody.rigHandL).GetComponent<Projectile>();
            projectile.transform.localPosition = Vector3.zero;
            projectile.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
            //projectile.transform.localRotation = Quaternion.identity;

            IsArrowDrawn = true;
        }

        public void OnArrowNock() {
            IsStringPulled = true;

            onBowPull.Invoke();
        }
        
        public void OnArrowReady() {
            if (isDrawingBow) isArrowReady = true;
        }
        public void OnArrowPutBack() {
            IsArrowDrawn = false;

            if (projectile) {
                Destroy(projectile.gameObject);
                projectile = null;
            }
        }
        
        public void OnThrow() {
            /*
            if (Weapon == null) return;
            var weapon = characterController.DetachWeapon().GetComponent<Rigidbody2D>();
            
            weapon.velocity = characterController.characterMoving.RbVelocity;
            weapon.angularVelocity = -1 * 200;
            weapon.AddForce(new Vector2(1,1) * 10.0f);

            /*
               weapon.angularVelocity = -facing * throwAngularSpeed;

               weapon.AddForce(PointAtTargetDirection * throwForce);
             */

            onThrow.Invoke();
        
        }
        public virtual GameObject Weapon {
            get { return null; }
        }
        public void ArcheryUpdate() {
            if (bow && IsStringPulled) bow.StringPullPos = characterController.characterBody.rigHandL.position;
        }
        
        public virtual void Attack(bool inputAttack) {
            isAttacking = false;
            //attackActionIndex = inputMelee ? (int)attackActionMelee : (int)attackAction;
            attackActionIndex = 21;

            
                if (isCrawling == false && Weapon && Weapon.TryGetComponent<WeaponBow>(out bow)) {
                    IsDrawingBow = inputAttack;
                }
            /*
            else if (isCrawling) {
                //isAttacking = inputAttack;
                attackActionIndex = (int)attackActionMelee;
            }
            */
            isAttacking = inputAttack 
                //|| inputMelee
                ;
            animator.SetInteger("AttackAction", attackActionIndex);
            animator.SetBool("IsAttacking", isAttacking);
        }
        public enum AttackActionMeleeType {
            None = 0,

            Swipe = 1,
            Stab = 2
        }

        public enum AttackActionType {
            None = 0,

            Swipe = 1,
            Stab = 2,

            PointAtTarget = 11,
            Summon = 12,
            Throw = 13,

            Archery = 21
        }

        public virtual void Init(ControllerOfCharacter controller) {
            characterController = controller;
        }


        #region - ATTACK EVENTS
        public void OnAttackStart() {
            onAttackStart.Invoke();
        }

        public void OnAttackHit() {
            onAttackHit.Invoke();
        }

        public void OnAttackEnd() {
            onAttackEnd.Invoke();
        }

        #endregion
    }
}