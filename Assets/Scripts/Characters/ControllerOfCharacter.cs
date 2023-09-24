using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Character {
    public class ControllerOfCharacter :MonoBehaviour {

        [Space]
        [Header("Ground")]
        protected Vector2 posBot;
        protected Vector2 posTop;
        [Space]
        [Header("Colliders")]
        [SerializeField] protected CapsuleCollider2D collider2d;
        [SerializeField] protected Rigidbody2D rb2d;



        [HideInInspector]
        public CharacterBody characterBody;
        [HideInInspector]
        public CharacterAttack characterAttack;

        public virtual void Init(InGameManager inGame) {
            characterAttack = GetComponent<CharacterAttack>();
            characterBody = GetComponent<CharacterBody>();


        }

        public GameObject DetachWeapon() {
            if (characterBody.weaponSlot.childCount <= 0) return null;
            GameObject weapon = characterBody.weaponSlot.GetChild(0).gameObject;

            var c = weapon.GetComponent<Collider2D>();
            if (!c) return null;
            c.isTrigger = false;

            var r = weapon.GetComponent<Rigidbody2D>();
            if (!r) return null;
            r.bodyType = RigidbodyType2D.Dynamic;

            weapon.transform.parent = null;

            return weapon;
        }

        public virtual bool IsDead{
            set {

            }
            get {
                return isDead;
            }
        }
        protected bool isDead = false;
    }
}