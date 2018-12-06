using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


namespace GUIAssignment
{
    public class PauseMenu : MenuBase
    {


        #region Variables
        //bool to see if the game is paused
        public static bool isPaused = false;
        //the canvas the pasue menu is on
        public GameObject pauseCanvas;
        #endregion

        #region Start&Update
        // Use this for initialization
        void Start()
        {

            CheckLoad();
            //Load the keybinds
            SetUpKeyBinds();
            

        }

        // Update is called once per frame
        void Update()
        {
            //if Escape key is pressed
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //if the game is paused
                if (isPaused)
                {
                    //if we are in the options menu
                    if (isOptions)
                    {
                        //toggle between main menu and options
                        MenuToggle();
                    }
                    //if we are in the keybind menu
                    else if (isKeybinds)
                    {
                        //toggle the menu between keybind and options
                        KeybindToggle();
                    }
                    else
                    {
                        //set time scale to 1
                        Time.timeScale = 1;
                        //turn the canvas off
                        pauseCanvas.SetActive(false);
                        //set isPaused to false
                        isPaused = false;
                    }

                }
                else
                {
                    //set time scale to 0
                    Time.timeScale = 0;
                    //turn the canvas on
                    pauseCanvas.SetActive(true);
                    //set isPaused to true
                    isPaused = true;
                }
            }
        }
        #endregion

        #region PauseMenuSpecificButtonFunctions

        public void ResumeGame()
        {
            //set time scale back to 1
            Time.timeScale = 1;
            //turn the canvas off
            pauseCanvas.SetActive(false);
            //set isPaused to false;
            isPaused = false;
        }

        public void ExitToMain()
        {
            //resume time upon returning to main menu
            Time.timeScale = 1;
            //reload the main menu
            SceneManager.LoadScene(0);
            
        }

        #endregion





    }
}
