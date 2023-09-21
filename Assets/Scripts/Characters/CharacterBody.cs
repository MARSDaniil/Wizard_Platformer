using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Character {
    public class CharacterBody :MonoBehaviour {
        public Transform rigHead;
        public Transform rigNeck;
        public Transform rigPelvis;
        public Transform rigSpine1;
        public Transform rigSpine2;
        public Transform rigUpperArmL;
        public Transform rigHandL;
        public Transform rigUpperArmR;
        public Transform rigHandR;
        public Transform rigWeapon;
        [Space]
        public Transform weaponSlot;

        public void SyncWeaponSlot() {
            weaponSlot.transform.position = rigWeapon.transform.position;
            weaponSlot.transform.rotation = rigWeapon.transform.rotation * Quaternion.Euler(0.0f, 0.0f, 180.0f);
        }

        
    }
}