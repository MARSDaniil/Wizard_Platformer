using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Character {
    [CreateAssetMenu(fileName = "HealthCharacterConfig", menuName = "Configs/Characters")]
    public class CharacterHealth :ScriptableObject {
        [Header("Health")]
        public int maxHealth = 100;
        public int currHealth = 100;
        [Header("Protect")]
        public int maxProtect = 100;
        public int currProtect = 0;
    }
}