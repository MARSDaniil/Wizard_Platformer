using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Game.UI.InGame {
    public class CharacterInfo :MenuWindow {
        public Slider healthSlider;
        public Slider defendSlider;
        public override void Init(bool startOpened = false) {
            base.Init(startOpened);
        }
        public void SetSliderValue(Slider slider, int value) {
            slider.value = value;
        }
    }
}