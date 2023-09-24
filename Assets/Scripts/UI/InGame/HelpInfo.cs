using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.InGame {
    public class HelpInfo :MenuWindow {
        public override void Init(bool startOpened = false) {
            base.Init(startOpened);
        }

        private void Start() {
            StartCoroutine(CloseHelpInfo());
        }

        private IEnumerator CloseHelpInfo() {
            yield return new WaitForSeconds(4f);
            Close();
        }
    }
}