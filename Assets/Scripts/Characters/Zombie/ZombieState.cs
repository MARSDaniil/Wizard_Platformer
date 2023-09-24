using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cainos.PixelArtMonster_Dungeon;
namespace Game.Character.Zombie {
    public class ZombieState :CharacterState {

        MonsterController monsterController;
        // Start is called before the first frame update
        public override void Init() {
            base.Init();
            monsterController = GetComponent<MonsterController>();
            if (monsterController == null) Debug.Log("controller null in player state");

            playerInfo.Init(true);
        }

        // Update is called once per frame
        public override void Dead() {
            monsterController.IsDead = true;
        }
    }
}