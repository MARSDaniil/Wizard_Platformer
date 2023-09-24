using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.UI.InGame;
namespace Game.Character {
    public class CharacterState :MonoBehaviour {

        [SerializeField] protected CharacterHealth characterHealth;
        public PlayerInfo playerInfo;

        
        public int CurrHealth {
            get { return currHealth; }
            set {
                if (value + currHealth > 100) {
                    value = 100 - currHealth;
                }
                currHealth += value;
                ChangeBar(playerInfo.healthSlider, currHealth);

                if(currHealth <= 0) {
                    Dead();
                }
            }
        }
        protected int currHealth;

        public int CurrProtect {
            get { return currProtect; }
            set {
                if (value + currProtect > 100) {
                    value = 100 - currProtect;
                }
                currProtect += value;
                ChangeBar(playerInfo.defendSlider, currProtect);
            }
        }
        protected int currProtect;

        public virtual void Init() {
            characterHealth.currHealth = characterHealth.maxHealth;
            currHealth = characterHealth.currHealth;

            currProtect = 25;

            ChangeBar(playerInfo.healthSlider, currHealth);
            ChangeBar(playerInfo.defendSlider, currProtect);

            
        }

        public virtual void ChangeState() {
            if(currProtect >= 0) {
                CurrProtect = (int)Random.Range(-20, -30);
            }
            else if(currHealth >= 0) {
                CurrHealth = (int)Random.Range(-40, -60);
            }
        }

        public virtual void ChangeCountOfArrow(int value) => playerInfo.SetArrowCount(value);

        public virtual void ChangeBar(Slider slider, int value) => playerInfo.SetSliderValue(slider, value);

        public virtual void Dead() {  }

    }
}