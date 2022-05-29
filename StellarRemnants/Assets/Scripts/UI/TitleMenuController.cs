using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StellarRemnants.UI {
    public class TitleMenuController : MonoBehaviour {
        public void Start() {
            Application.targetFrameRate = 60;
        }



        public void OnJoin() {
            SceneManager.LoadScene(1);
        }

        public void OnQuitPress() {
            Application.Quit();
        }

        public void OnHost() {
            SceneManager.LoadScene(1);
        }
    }
}