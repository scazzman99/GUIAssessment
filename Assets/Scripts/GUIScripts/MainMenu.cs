using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System.IO;

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

            //decide if you should show continue button
            if(!File.Exists(Application.persistentDataPath + "/CharData.xml"))
            {
                GameObject.Find("ContinueButton").SetActive(false);
            }
        }

        #region MainMenuSpecificButtonFunctions

        public void StartGame()
        {
            //load scene with index 1 in build
            SceneManager.LoadScene(1);
        }

        public void LoadGame()
        {
            //load directly into the game scene if a character exists
            SceneManager.LoadScene(2);
        }

        public void ExitGame()
        {
            //quit the game
            Application.Quit();
        }



        #endregion
    }
}