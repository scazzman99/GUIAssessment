using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCustom : MonoBehaviour
{


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



    #endregion
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GetSprites();
        GetSpriteRenderers();
        //print(playerSpriteRenders.ContainsKey("ShinL"));
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
                eyebrowsI = hairMax - 1;
            }

            //Change the sprite with sprite at index headI in headList. String used here must match object in inspector
            SetSprite("Hair", eyebrowsI);
        }

        GUI.Box(new Rect(scrW * 1f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 2, scrH * 0.5f), "Hair: " + eyebrowsI);

        //if "+" button is pressed for head
        if (GUI.Button(new Rect(scrW * 3f, scrH + buttonAdjust * (scrH * 0.5f), scrW * 0.5f, scrH * 0.5f), "+"))
        {
            //add 1 to headI
            eyebrowsI++;
            //if headI is equal to or greater than headMax
            if (eyebrowsI >= hairMax)
            {
                //set headI to 0 to loop around
                eyebrowsI = 0;
            }

            //Change the sprite with sprite at index headI in headList. String used here must match object in inspector
            SetSprite("Hair", eyebrowsI);
        }
        #endregion

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

            //Change the sprite with sprite at index headI in headList. String used here must match object in inspector
            SetSprite("Mouth", hairI);
        }
        #endregion
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
