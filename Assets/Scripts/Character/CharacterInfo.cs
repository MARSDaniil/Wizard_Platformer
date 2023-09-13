using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Character {
    [CreateAssetMenu(fileName = "Player Config", menuName = "Configs/Characters/Player")]
    public class CharacterInfo :ScriptableObject {
        [Header("Run")]
        public float runSpeedMax;
        public float runAcc;
        [Space]
        [Header("Jump")]
        public float jumpSpeed;
        public float jumpCooldown;
        public float jumpGravityMutiplier;
        public float fallGravityMutiplier;
        [Space]
        [Header("Ground")]
        public bool isGrounded = true;
        public float groundCheckRadius = 0.17f;
        

    }
}