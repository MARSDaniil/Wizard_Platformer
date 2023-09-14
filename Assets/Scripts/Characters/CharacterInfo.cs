using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Character {
    public class CharacterInfo :ScriptableObject {
        [Header("Walk")]
        public float walkSpeedMax;
        public float walkAcc;
        [Space]
        [Header("Run")]
        public float runSpeedMax;
        public float runAcc;
        [Space]
        [Header("Run")]
        public float groundBrakeAcc = 6.0f;
        public float airBrakeAcc = 1.0f;
        [Space]
        [Header("Jump")]
        public float jumpSpeed;
        public float jumpCooldown;
        public float jumpGravityMutiplier = 0.6f;
        public float fallGravityMutiplier = 1.3f;
        [Space]
        [Header("Crouch")]
        public float crouchSpeedMax = 1.0f;
        public float crouchAcc = 8.0f;
        [Space]
        [Header("Ground")]
        public bool isGrounded = true;
        public float groundCheckRadius = 0.17f;
    }
}