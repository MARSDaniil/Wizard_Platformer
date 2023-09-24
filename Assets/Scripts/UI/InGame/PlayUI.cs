using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace Game.UI.InGame {
    public class PlayUI :MenuWindow {
        [SerializeField] PlayerInfo playerInfo;
        [SerializeField] HelpInfo helpInfo;

        [Space]
        [SerializeField] Button restartButton; 
        public override void Init(bool startOpened = false) {
            base.Init(startOpened);
            playerInfo.Init(true);
            helpInfo.Init(true);

            restartButton.onClick.AddListener(RestartLevel);
        }

        private void RestartLevel() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
        }
    }
}