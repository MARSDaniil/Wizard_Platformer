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
    }
}