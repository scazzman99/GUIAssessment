using System.Collections;
using System.Collections.Generic;
//system.xml and IO used for file saving
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//needed for the mixer
using UnityEngine.Audio;


namespace GUIAssignment
{

    public class OptionsData
    {
        public float volSlider, brightSlider, ambientSlider;
        public KeyCode forward, backward, left, right, jump, crouch, sprint, interact;
        public bool fullScreen;
        public Resolution res;
    }
    public abstract class MenuBase : MonoBehaviour
    {



        #region CoreVariables


        [Header("Settings")]
        //text mesh pro dropdown
        public TMP_Dropdown resolutionDropdown;
        //sliders to adjust volume, brightness and ambient brightness
        public Slider volSlider, brightSlider, ambientSlider;
        //AudioMixer to control volume
        public AudioMixer gameAudio;
        //Array to hold computors usable resolutions
        public Resolution[] resolutions;
        //index of the above array
        public int resIndex = 0;
        //is the game in fullscreen
        public bool isFullscreen;
        //Directional light of the scene to be adjusted by brightness slider
        public Light sceneLight;
        [Header("MenuControl/Bools")]
        //is the options menu showing
        public bool isOptions;
        //is the keybinds menu showing
        public bool isKeybinds;
        //panels of menus
        public GameObject mainMenu, optionsMenu, keybindMenu;
        [Header("Keybind Keys")]
        //key to hold the original value of a keybind we are changing
        public KeyCode holdingKey;
        public KeyCode forward, backward, left, right, jump, crouch, sprint, interact;
        [Header("Keybind Data Structures")]
        //Dictionary that accesses KeyCodes by using an assigned string key
        public Dictionary<string, KeyCode> keyBindsD;
        //list to hold key codes so that they can be turned into strings for the buttons that display current key
        public List<KeyCode> keyBindsL;
        //array of text objects to store buttons displayed text. Set from inspector
        public Text[] keyBindButtonText;
        [Header("Saving Stuff")]
        public OptionsData data = new OptionsData();
        private string filePath, fileName = "GameData";
        #endregion
        [SerializeField]
        private TextAsset
    xmlFile;

     
        #region OptionGet&SetFunctions


        #region GetFunctions

        //Get volume level and set volume slider to this value. NOTE volume slider range is between -80 and 0 to fit mixer range
        public void GetVolume()
        {

            //bool to check if the mixer has returned anything with GetFloat()
            bool hasValue = false;
            //float to hold the output of GetFloat()
            float value;
            hasValue = gameAudio.GetFloat("MasterVolume", out value);
            Debug.Log("MasterVol 1 " + value);

            //if a value was returned
            if (hasValue)
            {
                //set volume slider value to audio mixer volume
                volSlider.value = value;
                Debug.Log("MasterVol 2 " + value);
            }
        }

        //gets intensity of sceneLight and sets brightness slider to this value
        public void GetBrightness()
        {
            //set brightslider value to scenelight intensity
            brightSlider.value = sceneLight.intensity;
        }

        //gets intensity of world ambient lighting and sets ambient slider to this value
        public void GetAmbientBrightness()
        {
            //set ambient slider value to ambinet light intensity
            ambientSlider.value = RenderSettings.ambientIntensity;
        }

        //get all available resolutions for the screen and store them. Set resolution drop down current value to screens current res
        public void GetResolutions()
        {
            //list to hold strings to easily display resolution width and height to user
            List<string> resolutionText = new List<string>();
            //set the resIndex to zero everytime to stop any index misplacement
            resIndex = 0;
            //fill resolution array with the screens available resolutions
            resolutions = Screen.resolutions;
            //Clear the dropdown for filling
            resolutionDropdown.ClearOptions();

            //loop through all resolutions and add their text respresentation to the list
            for (int i = 0; i < resolutions.Length; i++)
            {
                //string for the resolution at i in form AxB
                string resOption = resolutions[i].width + "x" + resolutions[i].height;
                //add the string to the list
                resolutionText.Add(resOption);
                //IF resolution at index i's width AND height match our current resolutions width AND height
                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    //Set resIndex to i
                    resIndex = i;
                }

            }
            //Add the strings to the dropdowns options
            resolutionDropdown.AddOptions(resolutionText);
            //set dropdowns value to our current resolution index
            resolutionDropdown.value = resIndex;
            //refresh the shown value on the dropdown to respresent these changes
            resolutionDropdown.RefreshShownValue();

        }

        #endregion


        #region SetFunctions
        //Sets the volume to the sliders current value using dynamic float
        public void SetVolume(float value)
        {
            //set mixer volume to volSlider value
            gameAudio.SetFloat("MasterVolume", value);
            //save the value to be reloaded on reboot
            //PlayerPrefs.SetFloat("VolumeSetting", value);
        }

        //Sets the brightness of sceneLight to the slider value using dynamic float
        public void SetBrightness(float value)
        {
            sceneLight.intensity = value;
            //save the new value to be carried over scenes
            //PlayerPrefs.SetFloat("BrightnessSetting", value);
        }

        //Sets the intenstity of ambient lighting to the slider value using dynamic float
        public void SetAmbientBrightness(float value)
        {
            RenderSettings.ambientIntensity = value;
            //save the new value to be carried over scenes
            //PlayerPrefs.SetFloat("AmbientSetting", value);
        }

        //Set the screens resolution to the resolution at index of dropdown
        public void SetResolution(int value)
        {
            //get resolution from array of resolutions based on dropdown index
            Resolution selectedRes = resolutions[value];
            //set the screens current resolution to the selectedRes and isFullscreen value
            Screen.SetResolution(selectedRes.width, selectedRes.height, Screen.fullScreen);

        }

        //set the game to fullscreen or not using dynamic bool on a toggle
        public void SetFullScreen(bool value)
        {
            Screen.fullScreen = value;

        }
        #endregion

        #endregion

        #region MenuToggles

        //swap between the main menu and the options menu
        public void MenuToggle()
        {
            if (isOptions)
            {
                //set isOptions to false to reflect menus current state
                isOptions = false;
                //set options menu to inactive and main menu to active
                mainMenu.SetActive(true);
                optionsMenu.SetActive(false);

            }
            else
            {
                //set isOptions to true to reflect menus current state
                isOptions = true;
                //set options menu to active and main menu to inactive
                mainMenu.SetActive(false);
                optionsMenu.SetActive(true);
                //find sliders in scene by using the Find function and attach their slider component
                volSlider = GameObject.Find("VolumeSlider").GetComponent<Slider>();
                brightSlider = GameObject.Find("BrightnessSlider").GetComponent<Slider>();
                ambientSlider = GameObject.Find("AmbientSlider").GetComponent<Slider>();
                resolutionDropdown = GameObject.Find("ResolutionDropdown").GetComponent<TMP_Dropdown>();
                //run get functions for all menu objects
                GetAmbientBrightness();
                GetBrightness();
                GetVolume();
                GetResolutions();

            }
        }

        //swap between the options menu and the keyBind menu
        public void KeybindToggle()
        {
            if (isKeybinds)
            {
                //set isKeybinds to false to reflect menus current state
                isKeybinds = false;
                //set is options to true
                isOptions = true;
                //set keybinds menu to inactive and options menu to active
                optionsMenu.SetActive(true);
                keybindMenu.SetActive(false);
                //find sliders in scene by using the Find function and attach their slider component
                volSlider = GameObject.Find("VolumeSlider").GetComponent<Slider>();
                brightSlider = GameObject.Find("BrightnessSlider").GetComponent<Slider>();
                ambientSlider = GameObject.Find("AmbientSlider").GetComponent<Slider>();
                //run get functions for all menu objects
                GetAmbientBrightness();
                GetBrightness();
                GetVolume();
                GetResolutions();

            }
            else
            {
                //set isKeybinds to true to reflect menus current state
                isKeybinds = true;
                //set is options to false
                isOptions = false;
                //set keybinds menu to active and options menu to inactive
                optionsMenu.SetActive(false);
                keybindMenu.SetActive(true);
            }
        }



        #endregion

        #region KeybindFunctions

        public void SetUpKeyBinds()
        {

            //parse the result of getstring to a keycode. If no keycode is found at string key 'Forward; then set to 'W'.
            //forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", "W"));
            // backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Backward", "S"));
            // left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A"));
            //right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D"));
            //jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space"));
            //crouch = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Crouch", "LeftControl"));
            //sprint = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Sprint", "LeftShift"));
            //interact = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "F"));

            //add the keycodes to the list of keybinds
            keyBindsL.Add(forward);
            keyBindsL.Add(backward);
            keyBindsL.Add(left);
            keyBindsL.Add(right);
            keyBindsL.Add(jump);
            keyBindsL.Add(crouch);
            keyBindsL.Add(sprint);
            keyBindsL.Add(interact);

            //set the text for the buttons to string of keycode from keyBindsL.
            for (int i = 0; i < keyBindsL.Capacity; i++)
            {
                keyBindButtonText[i].text = keyBindsL[i].ToString();
            }

            //initialise the dictionary
            keyBindsD = new Dictionary<string, KeyCode>();
            //add the keybinds to the dictionary with string keys that represent the control
            keyBindsD["Forward"] = keyBindsL[0];
            keyBindsD["Backward"] = keyBindsL[1];
            keyBindsD["Left"] = keyBindsL[2];
            keyBindsD["Right"] = keyBindsL[3];
            keyBindsD["Jump"] = keyBindsL[4];
            keyBindsD["Crouch"] = keyBindsL[5];
            keyBindsD["Sprint"] = keyBindsL[6];
            keyBindsD["Interact"] = keyBindsL[7];
        }

        public void GetButtonToChange()
        {
            //get the name of the button pressed by using EventSystem. keybind button names set up in inspector
            string buttonName = EventSystem.current.currentSelectedGameObject.name;
            //if the name of the button that called this function isnt NULL AND the dictionary of keybinds contains its name
            Debug.Log(buttonName);
            if (buttonName != null && keyBindsD.ContainsKey(buttonName))
            {
                //make a neew keycode the keycode stored at button name
                KeyCode keyBind = keyBindsD[buttonName];
                //run the function set key to get the key into the holdingKey keycode
                SetKeyBind(keyBind);
            }


        }

        public void SetKeyBind(KeyCode keyBind)
        {
            //set a default value for index. if this value doesnt change they keybind wont start
            int index = -1;

            //loop through keybindsL to find the index our keybind is at
            for (int i = 0; i < keyBindsL.Capacity; i++)
            {
                //if the keybinds match
                if (keyBindsL[i] == keyBind)
                {
                    //set the index to i
                    index = i;
                    //leave the for loop
                    break;
                }
            }

            //If the index is positive, binary search has found something
            if (index >= 0)
            {
                //IF the list doesnt contain a None keycode and thus is only changing one key right now
                if (!keyBindsL.Contains(KeyCode.None))
                {
                    Debug.Log("DID NOT CONTAIN NONE");
                    //set the holding key value to the keycode at index
                    holdingKey = keyBindsL[index];
                    //set the keybind at index to none
                    keyBindsL[index] = KeyCode.None;
                    //set the buttons' text to display nothing whilst changing
                    keyBindButtonText[index].text = keyBindsL[index].ToString();
                }
            }
        }

        #region KeybindSaveAndDefault

       

        public void KeyCodeDefaults()
        {
            //set list values to default
            keyBindsL[0] = KeyCode.W;
            keyBindsL[1] = KeyCode.S;
            keyBindsL[2] = KeyCode.A;
            keyBindsL[3] = KeyCode.D;
            keyBindsL[4] = KeyCode.Space;
            keyBindsL[5] = KeyCode.LeftControl;
            keyBindsL[6] = KeyCode.LeftShift;
            keyBindsL[7] = KeyCode.F;

            //loop through list of keybinds and set the text for the buttons to string of keycode from list.
            for (int i = 0; i < keyBindsL.Capacity; i++)
            {
                keyBindButtonText[i].text = keyBindsL[i].ToString();
            }

            //Update the dictionary to reflect the default values
            UpdateDictionary();
        }

        #endregion

        #endregion

        #region Other
        private void OnGUI()
        {
            //if the keybind menu is active
            if (isKeybinds)
            {
                //set current event to variable
                Event currentEvent = Event.current;
                //if the event is a key
                if (currentEvent.isKey)
                {
                    //loop through the list of keybinds
                    for (int i = 0; i < keyBindsL.Capacity; i++)
                    {
                        //if the index has keycode none at it
                        if (keyBindsL[i] == KeyCode.None)
                        {
                            //if the keybind list does not contain key the user wants to set a control as. If it does contain the event keycode, abort and dump the holding key back into the button
                            if (!keyBindsL.Contains(currentEvent.keyCode))
                            {
                                //set the keycode at i to the event keycode
                                keyBindsL[i] = currentEvent.keyCode;
                                //set the text at i to the string of event keycode
                                keyBindButtonText[i].text = keyBindsL[i].ToString();
                                //set holding key to none for the next time
                                holdingKey = KeyCode.None;
                                //update the dictionary
                                UpdateDictionary();
                            }
                            //else
                            else
                            {
                                //set the keycode at i to the holding key
                                keyBindsL[i] = holdingKey;
                                //set the text at i to the string of holding key
                                keyBindButtonText[i].text = keyBindsL[i].ToString();
                                //set holding key to none for the next time
                                holdingKey = KeyCode.None;
                            }
                            //leave the for loop
                            break;
                        }
                    }
                }
            }
        }

        //updates every value for the dictionary
        private void UpdateDictionary()
        {
            //add the keybinds to the dictionary with string keys that represent the control
            keyBindsD["Forward"] = keyBindsL[0];
            keyBindsD["Backward"] = keyBindsL[1];
            keyBindsD["Left"] = keyBindsL[2];
            keyBindsD["Right"] = keyBindsL[3];
            keyBindsD["Jump"] = keyBindsL[4];
            keyBindsD["Crouch"] = keyBindsL[5];
            keyBindsD["Sprint"] = keyBindsL[6];
            keyBindsD["Interact"] = keyBindsL[7];
        }
        #endregion

        #region Saving&Loading
        public void SaveSettings()
        {
            //set values in options data to meu values
            data.volSlider = volSlider.value;
            data.brightSlider = brightSlider.value;
            data.ambientSlider = ambientSlider.value;
            data.forward = keyBindsD["Forward"];
            data.backward = keyBindsD["Backward"];
            data.left = keyBindsD["Left"];
            data.right = keyBindsD["Right"];
            data.jump = keyBindsD["Jump"];
            data.crouch = keyBindsD["Crouch"];
            data.sprint = keyBindsD["Sprint"];
            data.interact = keyBindsD["Interact"];
            data.res = resolutions[resIndex];

            var serializer = new XmlSerializer(typeof(OptionsData));

            //while using file stream
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(stream, data);
            }

        }

        public void LoadSettings()
        {
            //open the file stream and read in to data
            var serializer = new XmlSerializer(typeof(OptionsData));

            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                //read data from file in as OptionsData
                data = serializer.Deserialize(stream) as OptionsData;
            }

            //set menu variables and keybinds to read data
            forward = data.forward;
            backward = data.backward;
            left = data.left;
            right = data.right;
            jump = data.jump;
            crouch = data.crouch;
            sprint = data.sprint;
            interact = data.interact;

            float saveVolume = data.volSlider;
            gameAudio.SetFloat("MasterVolume", saveVolume);

            bool hasVol;
            float volVar;
            hasVol = gameAudio.GetFloat("MasterVolume", out volVar);
            if (hasVol)
            {
                Debug.Log("MasterVol is set to " + volVar);
            }

            sceneLight.intensity = data.brightSlider;
            RenderSettings.ambientIntensity = data.ambientSlider;
            Screen.fullScreen = data.fullScreen;
        }
        #endregion

        private void Awake()
        {
            CheckLoad();
        }

        public void CheckLoad()
        {
            forward = KeyCode.W;
            backward = KeyCode.S;
            left = KeyCode.A;
            right = KeyCode.D;
            jump = KeyCode.Space;
            crouch = KeyCode.LeftControl;
            sprint = KeyCode.LeftShift;
            interact = KeyCode.E;
            //get the scene light to be used in brightness set and get
            sceneLight = GameObject.FindGameObjectWithTag("SceneLight").GetComponent<Light>();
            //get file path for save file
            filePath = Application.persistentDataPath + "/" + fileName + ".xml";
            //if the file exists
           if (File.Exists(filePath))
            {
                //load the file
                LoadSettings();
            }
            else
            {
                //set keycodes to defaults
                forward = KeyCode.W;
                backward = KeyCode.S;
                left = KeyCode.A;
                right = KeyCode.D;
                jump = KeyCode.Space;
                crouch = KeyCode.LeftControl;
                sprint = KeyCode.LeftShift;
                interact = KeyCode.E;
            }
        }
    }
}
