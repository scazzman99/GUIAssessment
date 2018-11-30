using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using UnityEngine.SceneManagement;


public class CustomizeData
{
    public int armI, handI, legI, torsoI, headI, hairI, mouthI, eyesI, eyebrowsI;
    public int strCur, dexCur, conCur, intCur, wisCur, charCur;
}

public class SetCustom : MonoBehaviour
{

    ColorPicker pick = new ColorPicker();
   
    #region Variables
    
    [Header("Player")]
    public GameObject player;

    [Header("SpriteLists")]
    public List<Sprite> armRList = new List<Sprite>();
    public List<Sprite> armLList = new List<Sprite>();
    public List<Sprite> forearmRList = new List<Sprite>();
    public List<Sprite> forearmLList = new List<Sprite>();
    public List<Sprite> handRList = new List<Sprite>();
    public List<Sprite> handLList = new List<Sprite>();
    public List<Sprite> torsoList = new List<Sprite>();
    public List<Sprite> pelvisList = new List<Sprite>();
    public List<Sprite> legRList = new List<Sprite>();
    public List<Sprite> legLList = new List<Sprite>();
    public List<Sprite> shinRList = new List<Sprite>();
    public List<Sprite> shinLList = new List<Sprite>();
    public List<Sprite> mouthList = new List<Sprite>();
    public List<Sprite> eyesList = new List<Sprite>();
    public List<Sprite> headList = new List<Sprite>();
    public List<Sprite> hairList = new List<Sprite>();
    public List<Sprite> eyebrowsList = new List<Sprite>();
    [Header("Index")]
    public int torsoI;
    public int legI, pelvisI, armI, handI,
    hairI, mouthI, eyesI, headI, eyebrowsI;
    [Header("IndexMax")]
    public int torsoMax;
    public int legMax, pelvisMax, armMax,
    handMax, hairMax, mouthMax, eyesMax, headMax, eyebrowsMax;
    [Header("SpriteRenderers")]
    Dictionary<string, SpriteRenderer> playerSpriteRenders = new Dictionary<string, SpriteRenderer>();
    [Header("PlayerStats")]
    public string playerName;
    //points the player can spend on stats
    public int pointsToSpend = 10;
    //min values for all player stats per class
    public int strMin, dexMin, conMin, intMin, wisMin, charMin;
    //current values for all player stats per class
    public int strCur, dexCur, conCur, intCur, wisCur, charCur;
    //arrays to hold stats and min stats. Must match order of string array stats
    public int[] statVals, minStatVals;
    //array of strings to store stat names
    public string[] stats;
    //the players class
    public CharClasses playerClass = CharClasses.BigBoi;
    public CharClasses[] classes;
    bool showClasses;
    Vector2 scrollPos;

    //an instance of CustomizeData to save to and load into
    CustomizeData data = new CustomizeData();
    //file path and name
    string filePath, fileName = "CharData";

    #endregion

    private void Awake()
    {
        //grab the applications file path and set filePath to returned value
        filePath = Application.persistentDataPath + "/" + fileName + ".xml";
        
    }

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GetSprites();
        GetSpriteRenderers();
        //print(playerSpriteRenders.ContainsKey("ShinL"));
        //define classes array
        
        classes = new CharClasses[] { CharClasses.BigBoi, CharClasses.Engineer, CharClasses.GenericMarine, CharClasses.Pilot, CharClasses.SpaceWizard, CharClasses.SpookyM8 };
        stats = new string[] { "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" };
        minStatVals = new int[stats.Length];
        statVals = new int[stats.Length];
        GetStats(playerClass);

    }

    // Update is called once per frame
    void Update()
    {

    }

    #region GetSprites and SpriteRenderers

    //get all sprites to be swapped out
    public void GetSprites()
    {
        Sprite sprite = null;
        //loop through all sprites in resources and add them to their respective lists

        //Grabbing sprites from 0 to torsoMax from their listed folder and adding them to torsoList
        for (int i = 0; i < torsoMax; i++)
        {
            sprite = Resources.Load("CharacterSprites/Torso/TorsoArmour_" + i) as Sprite;
            torsoList.Add(sprite);
        }

        //Grabbing sprites from 0 to pelvisMax from their listed folder and adding them to pelvisList
        for (int i = 0; i < pelvisMax; i++)
        {
            sprite = Resources.Load("CharacterSprites/Legs/Pelvis/Pelvis_" + i) as Sprite;
            pelvisList.Add(sprite);
        }

        #region Arms(all parts)
        //Grabbing sprites from 0 to armMax from their listed folder and adding them to Lists for left and right upper arms, forearms and hands
        for (int i = 0; i < armMax; i++)
        {
            sprite = Resources.Load("CharacterSprites/Arms/UpperArms/Right/UpperArmR_" + i) as Sprite;
            armRList.Add(sprite);
            sprite = Resources.Load("CharacterSprites/Arms/UpperArms/Left/UpperArmL_" + i) as Sprite;
            armLList.Add(sprite);
            sprite = Resources.Load("CharacterSprites/Arms/Forearms/Right/ForearmR_" + i) as Sprite;
            forearmRList.Add(sprite);
            sprite = Resources.Load("CharacterSprites/Arms/Forearms/Left/ForearmL_" + i) as Sprite;
            forearmLList.Add(sprite);
            sprite = Resources.Load("CharacterSprites/Arms/Hands/Right/HandR_" + i) as Sprite;
            handRList.Add(sprite);
            sprite = Resources.Load("CharacterSprites/Arms/Hands/Left/HandL_" + i) as Sprite;
            handLList.Add(sprite);
        }
        #endregion

        #region Legs(and shins)
        //Grabbing sprites from 0 to legMax from their listed folder and adding them to Lists for left and right legs and shins
        for (int i = 0; i < legMax; i++)
        {
            sprite = Resources.Load("CharacterSprites/Legs/Legs/Leg_" + i) as Sprite;
            legRList.Add(sprite);
            legLList.Add(sprite);
            sprite = Resources.Load("CharacterSprites/Legs/Shins/Shin_" + i) as Sprite;
            shinLList.Add(sprite);
            shinRList.Add(sprite);
        }
        #endregion

        #region HeadFeatures
        //Grabbing sprites from 0 to headMax from their listed folder and adding them to headList
        for (int i = 0; i < headMax; i++)
        {
            sprite = Resources.Load("CharacterSprites/Head/Heads/Head_" + i) as Sprite;
            headList.Add(sprite);
        }

        //Grabbing sprites from 0 to mouthMax from their listed folder and adding them to mouthList
        for (int i = 0; i < mouthMax; i++)
        {
            sprite = Resources.Load("CharacterSprites/Head/Mouths/Mouth_" + i) as Sprite;
            mouthList.Add(sprite);
        }

        //Grabbing sprites from 0 to eyesMax from their listed folder and adding them to eyesList
        for (int i = 0; i < eyesMax; i++)
        {
            sprite = Resources.Load("CharacterSprites/Head/Eyes/Eyes_" + i) as Sprite;
            eyesList.Add(sprite);
        }

        //Grabbing sprites from 0 to eyebrowsMax from their listed folder and adding them to eyebrowsList
        for (int i = 0; i < eyebrowsMax; i++)
        {
            sprite = Resources.Load("CharacterSprites/Head/Eyebrows/Eyebrows_" + i) as Sprite;
            eyebrowsList.Add(sprite);
        }

        //Grabbing sprites from 0 to hairMax from their listed folder and adding them to hairList
        for (int i = 0; i < hairMax; i++)
        {
            sprite = Resources.Load("CharacterSprites/Head/Hair/Hair_" + i) as Sprite;
            hairList.Add(sprite);
        }
        #endregion
    }

    public void GetSpriteRenderers()
    {
        //create an array of sprite renderes made from children of player
        SpriteRenderer[] renderers = player.GetComponentsInChildren<SpriteRenderer>();
        //for each sprite renderer
        for (int i = 0; i < renderers.Length; i++)
        {
            //add the renderers object name and renderer to the playerSpriteRenders
            playerSpriteRenders.Add(renderers[i].name, renderers[i]);
        }
    }

    #endregion

    #region OnGUI
    private void OnGUI()
    {
        //get screen width and height fractions to space buttons
        float scrW = Screen.width / 16f;
        float scrH = Screen.height / 9f;
        //set up int to space customisation buttons downwards
        int buttonAdjust = 0;

        #region CustomizationButtons

        #region HeadButtons
        //if "-" button is pressed for head
        if (GUI.Button(new Rect(scrW * 0.5f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 0.5f, scrH * 0.5f), "-"))
        {
            //take 1 from headI
            headI--;
            //if headI is less than 0
            if (headI < 0)
            {
                //set headI to headMax - 1 to loop around
                headI = headMax - 1;
            }

            //Change the sprite with sprite at index headI in headList. String used here must match object in inspector
            SetSprite("Head", headI);
        }

        GUI.Box(new Rect(scrW * 1f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 2, scrH * 0.5f), "Head: " + headI);

        //if "+" button is pressed for head
        if (GUI.Button(new Rect(scrW * 3f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 0.5f, scrH * 0.5f), "+"))
        {
            //add 1 to headI
            headI++;
            //if headI is equal to or greater than headMax
            if (headI >= headMax)
            {
                //set headI to 0 to loop around
                headI = 0;
            }

            //Change the sprite with sprite at index headI in headList. String used here must match object in inspector
            SetSprite("Head", headI);
        }
        #endregion

        //add one to button adjust so the next row of buttons does not overlap the previous row
        buttonAdjust++;

        #region HairButtons
        //if "-" button is pressed for head
        if (GUI.Button(new Rect(scrW * 0.5f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 0.5f, scrH * 0.5f), "-"))
        {
            //take 1 from hairI
            hairI--;
            //if hairI is less than 0
            if (hairI < 0)
            {
                //set hairI to headMax - 1 to loop around
                hairI = hairMax - 1;
            }

            //Change the sprite with sprite at index hairI in hairList. String used here must match object in inspector
            SetSprite("Hair", hairI);
        }

        GUI.Box(new Rect(scrW * 1f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 2, scrH * 0.5f), "Hair: " + hairI);

        //if "+" button is pressed for hair
        if (GUI.Button(new Rect(scrW * 3f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 0.5f, scrH * 0.5f), "+"))
        {
            //add 1 to hair
            hairI++;
            //if hairI is equal to or greater than hairMax
            if (hairI >= hairMax)
            {
                //set hairI to 0 to loop around
                hairI = 0;
            }

            //Change the sprite with sprite at index headI in headList. String used here must match object in inspector
            SetSprite("Hair", hairI);
        }
        #endregion

        buttonAdjust++;

        #region EyesButtons
        //if "-" button is pressed for head
        if (GUI.Button(new Rect(scrW * 0.5f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 0.5f, scrH * 0.5f), "-"))
        {
            //take 1 from headI
            eyesI--;
            //if headI is less than 0
            if (eyesI < 0)
            {
                //set headI to headMax - 1 to loop around
                eyesI = eyesMax - 1;
            }

            //Change the sprite with sprite at index headI in headList. String used here must match object in inspector
            SetSprite("Eyes", eyesI);
        }

        GUI.Box(new Rect(scrW * 1f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 2, scrH * 0.5f), "Eyes: " + eyesI);

        //if "+" button is pressed for head
        if (GUI.Button(new Rect(scrW * 3f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 0.5f, scrH * 0.5f), "+"))
        {
            //add 1 to headI
            eyesI++;
            //if headI is equal to or greater than headMax
            if (eyesI >= eyesMax)
            {
                //set headI to 0 to loop around
                eyesI = 0;
            }

            //Change the sprite with sprite at index headI in headList. String used here must match object in inspector
            SetSprite("Eyes", eyesI);
        }
        #endregion

        buttonAdjust++;

        #region EyebrowButtons
        //if "-" button is pressed for head
        if (GUI.Button(new Rect(scrW * 0.5f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 0.5f, scrH * 0.5f), "-"))
        {
            //take 1 from headI
            eyebrowsI--;
            //if headI is less than 0
            if (eyebrowsI < 0)
            {
                //set headI to headMax - 1 to loop around
                eyebrowsI = eyebrowsMax - 1;
            }

            //Change the sprite with sprite at index headI in headList. String used here must match object in inspector
            SetSprite("Eyebrows", eyebrowsI);
        }

        GUI.Box(new Rect(scrW * 1f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 2, scrH * 0.5f), "Eyebrows: " + eyebrowsI);

        //if "+" button is pressed for head
        if (GUI.Button(new Rect(scrW * 3f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 0.5f, scrH * 0.5f), "+"))
        {
            //add 1 to headI
            eyebrowsI++;
            //if headI is equal to or greater than headMax
            if (eyebrowsI >= eyebrowsMax)
            {
                //set headI to 0 to loop around
                eyebrowsI = 0;
            }

            //Change the sprite with sprite at index headI in headList. String used here must match object in inspector
            SetSprite("Eyebrows", eyebrowsI);
        }
        #endregion

        buttonAdjust++;

        #region MouthButtons
        //if "-" button is pressed for mouth
        if (GUI.Button(new Rect(scrW * 0.5f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 0.5f, scrH * 0.5f), "-"))
        {
            //take 1 from mouthI
            mouthI--;
            //if mouthI is less than 0
            if (mouthI < 0)
            {
                //set mouthI to mouthMax - 1 to loop around
                mouthI = mouthMax - 1;
            }

            //Change the sprite with sprite at index mouthI in mouthList. String used here must match object in inspector
            SetSprite("Mouth", mouthI);
        }

        GUI.Box(new Rect(scrW * 1f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 2, scrH * 0.5f), "Mouth: " + mouthI);

        //if "+" button is pressed for mouth
        if (GUI.Button(new Rect(scrW * 3f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 0.5f, scrH * 0.5f), "+"))
        {
            //add 1 to mouth
            mouthI++;
            //if mouthI is equal to or greater than mouthMax
            if (mouthI >= mouthMax)
            {
                //set mouthI to 0 to loop around
                mouthI = 0;
            }

            //Change the sprite with sprite at index mouthI in mouthList. String used here must match object in inspector
            SetSprite("Mouth", mouthI);
        }
        #endregion

        buttonAdjust++;

        //for torso we will simultaniously change torso and pelvis using torsoI
        #region Torso
        //if "-" button is pressed for torso
        if (GUI.Button(new Rect(scrW * 0.5f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 0.5f, scrH * 0.5f), "-"))
        {
            //take 1 from torsoI
            torsoI--;
            //if torsoI is less than 0
            if (torsoI < 0)
            {
                //set torsoI to torsoMax - 1 to loop around
                torsoI = torsoMax - 1;
            }

            //Change the sprite with sprite at index torsoI in torsoList. String used here must match object in inspector
            SetSprite("Torso", torsoI);
            //Change the sprite with sprite at index torsoI in pelvisList. String used here must match object in inspector
            SetSprite("Pelvis", torsoI);

        }

        GUI.Box(new Rect(scrW * 1f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 2, scrH * 0.5f), "Torso: " + torsoI);

        //if "+" button is pressed for torso
        if (GUI.Button(new Rect(scrW * 3f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 0.5f, scrH * 0.5f), "+"))
        {
            //add 1 to torso
            torsoI++;
            //if torsoI is equal to or greater than torsoMax
            if (torsoI >= torsoMax)
            {
                //set torsoI to 0 to loop around
                torsoI = 0;
            }

            //Change the sprite with sprite at index torsoI in torsoList. String used here must match object in inspector
            SetSprite("Torso", torsoI);
            //Change the sprite with sprite at index torsoI in pelvisList. String used here must match object in inspector
            SetSprite("Pelvis", torsoI);
        }
        #endregion

        buttonAdjust++;

        //will change left and right legs and shins at the same time using legI
        #region Legs
        //if "-" button is pressed for legs
        if (GUI.Button(new Rect(scrW * 0.5f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 0.5f, scrH * 0.5f), "-"))
        {
            //take 1 from legI
            legI--;
            //if legI is less than 0
            if (legI < 0)
            {
                //set legI to legMax - 1 to loop around
                legI = legMax - 1;
            }

            //Change the sprite with sprite at index legI in shinRList and shinLList. String used here must match object in inspector
            SetSprite("ShinL", legI);
            SetSprite("ShinR", legI);
            //Change the sprite with sprite at index legI in legRList and legLList. String used here must match object in inspector
            SetSprite("LegL", legI);
            SetSprite("LegR", legI);


        }

        GUI.Box(new Rect(scrW * 1f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 2, scrH * 0.5f), "Legs: " + legI);

        //if "+" button is pressed for torso
        if (GUI.Button(new Rect(scrW * 3f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 0.5f, scrH * 0.5f), "+"))
        {
            //add 1 to leg
            legI++;
            //if legI is equal to or greater than legMax
            if (legI >= legMax)
            {
                //set legI to 0 to loop around
                legI = 0;
            }

            //Change the sprite with sprite at index legI in shinRList and shinLList. String used here must match object in inspector
            SetSprite("ShinL", legI);
            SetSprite("ShinR", legI);
            //Change the sprite with sprite at index legI in legRList and legLList. String used here must match object in inspector
            SetSprite("LegL", legI);
            SetSprite("LegR", legI);
        }
        #endregion

        buttonAdjust++;

        //will change left and right arms & forearms at the same time using armI
        #region Arms
        //if "-" button is pressed for arms
        if (GUI.Button(new Rect(scrW * 0.5f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 0.5f, scrH * 0.5f), "-"))
        {
            //take 1 from armI
            armI--;
            //if legI is less than 0
            if (armI < 0)
            {
                //set armI to armMax - 1 to loop around
                armI = armMax - 1;
            }

            //Change the sprite with sprite at index armI in armRList and armLList. String used here must match object in inspector
            SetSprite("ArmL", armI);
            SetSprite("ArmR", armI);
            //Change the sprite with sprite at index armI in forearmRList and forearmLList. String used here must match object in inspector
            SetSprite("ForearmL", armI);
            SetSprite("ForearmR", armI);


        }

        GUI.Box(new Rect(scrW * 1f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 2, scrH * 0.5f), "Arms: " + armI);

        //if "+" button is pressed for torso
        if (GUI.Button(new Rect(scrW * 3f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 0.5f, scrH * 0.5f), "+"))
        {
            //add 1 to arm
            armI++;
            //if armI is equal to or greater than armMax
            if (armI >= armMax)
            {
                //set armI to 0 to loop around
                armI = 0;
            }

            //Change the sprite with sprite at index armI in armRList and armLList. String used here must match object in inspector
            SetSprite("ArmL", armI);
            SetSprite("ArmR", armI);
            //Change the sprite with sprite at index armI in forearmRList and forearmLList. String used here must match object in inspector
            SetSprite("ForearmL", armI);
            SetSprite("ForearmR", armI);
        }
        #endregion

        buttonAdjust++;

        //set both left and right hands at the same time using handI
        #region Hands
        //if "-" button is pressed for hands
        if (GUI.Button(new Rect(scrW * 0.5f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 0.5f, scrH * 0.5f), "-"))
        {
            //take 1 from handI
            handI--;
            //if handI is less than 0
            if (handI < 0)
            {
                //set handI to handMax - 1 to loop around
                handI = handMax - 1;
            }

            //Change the sprite with sprite at index handI in handList. String used here must match object in inspector
            SetSprite("HandL", handI);
            SetSprite("HandR", handI);
        }

        GUI.Box(new Rect(scrW * 1f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 2, scrH * 0.5f), "Hand: " + handI);

        //if "+" button is pressed for hand
        if (GUI.Button(new Rect(scrW * 3f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 0.5f, scrH * 0.5f), "+"))
        {
            //add 1 to hand
            handI++;
            //if handI is equal to or greater than handMax
            if (handI >= handMax)
            {
                //set handI to 0 to loop around
                handI = 0;
            }

            //Change the sprite with sprite at index handI in handRList and handLList. String used here must match object in inspector
            SetSprite("HandR", handI);
            SetSprite("HandL", handI);
            

        }
        #endregion

        #endregion

        buttonAdjust++;

        //make a toggle that opens and closes 
        //showClasses = GUI.Toggle(new Rect(scrW * 1f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 2f, scrH * 0.5f), showClasses, "Classes");
        if(GUI.Button(new Rect(scrW * 1f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 2f, scrH * 0.5f), playerClass.ToString()))
        {
            showClasses = !showClasses;
        }

       
        buttonAdjust++;

        #region ClassScroll
        //if showClasses
        if (showClasses)
        {
            //begin the scroll window
            scrollPos = GUI.BeginScrollView(new Rect(scrW * 0.5f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 3f, scrH * 2f), scrollPos, new Rect(0f, 0f, 0f, scrH * 2 + (classes.Length - 4) * scrH * 0.5f), false, true);

            //loop through the array of classes
            for (int i = 0; i < classes.Length; i++)
            {
                //if class button is pressed
                if (GUI.Button(new Rect(scrW * 0.5f, 0f + i * scrH * 0.5f, scrW * 2f, scrH * 0.5f), classes[i].ToString()))
                {
                    playerClass = classes[i];
                    //run function to change default stats etc
                    GetStats(playerClass);
                }
            }

            //end the scroll window
            GUI.EndScrollView();
        }
        #endregion

        buttonAdjust = 0;

        #region StatButtons
        for (int i = 0; i < stats.Length; i++)
        {
            if (statVals[i] > minStatVals[i])
            {
                if (GUI.Button(new Rect(scrW * 12.5f, scrH + i * (scrH * 0.5f), scrW * 0.5f, scrH * 0.5f), "-"))
                {
                    //decrease the stat
                    statVals[i]--;
                    //increase points to spend
                    pointsToSpend++;
                }
            }

            GUI.Box(new Rect(scrW * 13f, scrH + i * (scrH * 0.5f), scrW * 2, scrH * 0.5f), stats[i] + ": " + statVals[i]);

            if (pointsToSpend > 0)
            {
                //if "+" button is pressed for hand
                if (GUI.Button(new Rect(scrW * 15f, scrH + i * (scrH * 0.5f), scrW * 0.5f, scrH * 0.5f), "+"))
                {
                    //up the stat being spent
                    statVals[i]++;
                    //deplete the points to spend
                    pointsToSpend--;
                }
            }
            buttonAdjust++;

        }
        #endregion

        playerName = GUI.TextField(new Rect(scrW * 5f, scrH * 7f, scrW * 6f, scrH*0.75f), playerName, 16);

        if (pointsToSpend == 0 && playerName != "")
        {
            if (GUI.Button(new Rect(scrW * 7f, scrH * 8f, scrW * 2f, scrH * 0.5f), "Save and Play"))
            {
                SaveData();
                SceneManager.LoadScene(2);
            } 
        }

    }
    #endregion

    //will be called for every part of a group that needs setting. may be called several times from one button
    public void SetSprite(string group, int index)
    {
        Sprite[] spriteToArray;

        switch (group)
        {
            case "Head":
                spriteToArray = headList.ToArray();
                playerSpriteRenders[group].sprite = spriteToArray[index];
                break;
            case "Mouth":
                spriteToArray = mouthList.ToArray();
                playerSpriteRenders[group].sprite = spriteToArray[index];
                break;
            case "Eyes":
                spriteToArray = eyesList.ToArray();
                playerSpriteRenders[group].sprite = spriteToArray[index];
                break;
            case "Eyebrows":
                spriteToArray = eyebrowsList.ToArray();
                playerSpriteRenders[group].sprite = spriteToArray[index];
                break;
            case "Hair":
                spriteToArray = hairList.ToArray();
                playerSpriteRenders[group].sprite = spriteToArray[index];
                break;
            case "Torso":
                spriteToArray = torsoList.ToArray();
                playerSpriteRenders[group].sprite = spriteToArray[index];
                break;
            case "ArmL":
                spriteToArray = armLList.ToArray();
                playerSpriteRenders[group].sprite = spriteToArray[index];
                break;
            case "ArmR":
                spriteToArray = armRList.ToArray();
                playerSpriteRenders[group].sprite = spriteToArray[index];
                break;
            case "ForearmR":
                spriteToArray = forearmRList.ToArray();
                playerSpriteRenders[group].sprite = spriteToArray[index];
                break;
            case "ForearmL":
                spriteToArray = forearmLList.ToArray();
                playerSpriteRenders[group].sprite = spriteToArray[index];
                break;
            case "HandL":
                spriteToArray = handLList.ToArray();
                playerSpriteRenders[group].sprite = spriteToArray[index];
                break;
            case "HandR":
                spriteToArray = handRList.ToArray();
                playerSpriteRenders[group].sprite = spriteToArray[index];
                break;
            case "Pelvis":
                spriteToArray = pelvisList.ToArray();
                playerSpriteRenders[group].sprite = spriteToArray[index];
                break;
            case "LegL":
                spriteToArray = legLList.ToArray();
                playerSpriteRenders[group].sprite = spriteToArray[index];
                break;
            case "LegR":
                spriteToArray = legRList.ToArray();
                playerSpriteRenders[group].sprite = spriteToArray[index];
                break;
            case "ShinL":
                spriteToArray = shinLList.ToArray();
                playerSpriteRenders[group].sprite = spriteToArray[index];
                break;
            case "ShinR":
                spriteToArray = shinRList.ToArray();
                playerSpriteRenders[group].sprite = spriteToArray[index];
                break;
        }
    }

    //gets the min stats for a class and sets the players current stats to minimums. Also restores point count to 10
    private void GetStats(CharClasses playerClass)
    {
        switch (playerClass)
        {
            case CharClasses.BigBoi:
                minStatVals[0] = 13; minStatVals[1] = 7; minStatVals[2] = 13; minStatVals[3] = 7; minStatVals[4] = 7; minStatVals[5] = 10;
                break;

            case CharClasses.Engineer:
                minStatVals[0] = 9; minStatVals[1] = 9; minStatVals[2] = 10; minStatVals[3] = 12; minStatVals[4] = 8; minStatVals[5] = 7;
                break;

            case CharClasses.GenericMarine:
                minStatVals[0] = 11; minStatVals[1] = 11; minStatVals[2] = 10; minStatVals[3] = 9; minStatVals[4] = 7; minStatVals[5] = 9;
                break;

            case CharClasses.Pilot:
                minStatVals[0] = 7; minStatVals[1] = 8; minStatVals[2] = 10; minStatVals[3] = 10; minStatVals[4] = 7; minStatVals[5] = 12;
                break;

            case CharClasses.SpaceWizard:
                minStatVals[0] = 6; minStatVals[1] = 8; minStatVals[2] = 8; minStatVals[3] = 12; minStatVals[4] = 12; minStatVals[5] = 9;
                break;

            case CharClasses.SpookyM8:
                minStatVals[0] = 8; minStatVals[1] = 8; minStatVals[2] = 8; minStatVals[3] = 9; minStatVals[4] = 10; minStatVals[5] = 2;
                break;
        }

        
        for (int i = 0; i < minStatVals.Length; i++)
        {
            statVals[i] = minStatVals[i];
        }
        pointsToSpend = 10;
    }

    //Saves the stat data and customization data
    private void SaveData()
    {
        data.armI = armI;
        data.handI = handI;
        data.headI = headI;
        data.mouthI = mouthI;
        data.eyebrowsI = eyebrowsI;
        data.eyesI = eyesI;
        data.hairI = hairI;
        data.legI = legI;
        data.torsoI = torsoI;
        data.strCur = statVals[0];
        data.dexCur = statVals[1];
        data.conCur = statVals[2];
        data.intCur = statVals[3];
        data.wisCur = statVals[4];
        data.charCur = statVals[5];

        var serializer = new XmlSerializer(typeof(CustomizeData));

        using(var stream = new FileStream(filePath, FileMode.Create))
        {
            serializer.Serialize(stream, data);
        }
    }

}

public enum CharClasses
{
    BigBoi,
    SpaceWizard,
    GenericMarine,
    SpookyM8,
    Engineer,
    Pilot
}
