using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

namespace GUIAssignment
{
    //inherit from MenuBase class to get all of its functions and variables
    public class MainMenu : MenuBase
    {

        private void Start()
        {
            CheckLoad();
            //Load the keybinds
            SetUpKeyBinds();
        }

        #region MainMenuSpecificButtonFunctions

        public void StartGame()
        {
            //load scene with index 1 in build
            SceneManager.LoadScene(1);
        }

        public void ExitGame()
        {
            //quit the game
            Application.Quit();
        }

        #endregion
    }
}