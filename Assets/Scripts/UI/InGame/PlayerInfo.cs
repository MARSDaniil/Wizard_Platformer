using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Game.UI.InGame {
    public class PlayerInfo :MenuWindow {

        public Slider healthSlider;
        public Slider defendSlider;

        public TextMeshProUGUI arrowCount;
        public override void Init(bool startOpened = false) {
            base.Init(startOpened);
        }

        public void SetSliderValue(Slider slider, int value) {
            slider.value = value;
        }

        public void SetArrowCount(int value) => arrowCount.text = value.ToString();
    }
}