using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.UI.InGame {
    public class PlayUI :MenuWindow {
        [SerializeField] PlayerInfo playerInfo;
        public override void Init(bool startOpened = false) {
            base.Init(startOpened);
            playerInfo.Init(true);
        }

    }
}