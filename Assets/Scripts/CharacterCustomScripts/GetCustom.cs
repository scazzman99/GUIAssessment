using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class GetCustom : MonoBehaviour
{

    GameObject player;
    Dictionary<string, SpriteRenderer> playerSpriteRenders = new Dictionary<string, SpriteRenderer>();
    CustomizeData data = new CustomizeData();
    //file path and name
    string filePath, fileName = "CharData";

    public PlayerManager playerManager;
    int legI, armI, handI,
    hairI, mouthI, eyesI, headI, eyebrowsI, torsoI;
    CharClasses playerClass;
    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        //grab the applications file path and set filePath to returned value
        filePath = Application.persistentDataPath + "/" + fileName + ".xml";
       
        if (File.Exists(filePath))
        {
            
            LoadData();
        }
        else
        {
            playerManager.strCur = 7;
            playerManager.dexCur = 7;
            playerManager.conCur = 7;
            playerManager.intCur = 7;
            playerManager.wisCur = 7;
            playerManager.charCur = 7;
        }
    }
    // Use this for initialization
    void Start()
    {
        //script is attached to player

        
        GetSpriteRenderers();
        LoadSprites();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetSpriteRenderers()
    {
        //create an array of sprite renderes made from children of player
        SpriteRenderer[] renderers = this.GetComponentsInChildren<SpriteRenderer>();
        //for each sprite renderer
        for (int i = 0; i < renderers.Length; i++)
        {
            //add the renderers object name and renderer to the playerSpriteRenders
            playerSpriteRenders.Add(renderers[i].name, renderers[i]);
        }
    }

    public void LoadSprites()
    {
        playerSpriteRenders["ArmL"].sprite = Resources.Load("CharacterSprites/Arms/UpperArms/Left/UpperArmL_" + armI) as Sprite;
        playerSpriteRenders["ArmR"].sprite = Resources.Load("CharacterSprites/Arms/UpperArms/Right/UpperArmR_" + armI) as Sprite;
        playerSpriteRenders["ForearmL"].sprite = Resources.Load("CharacterSprites/Arms/Forearms/Left/ForearmL_" + armI) as Sprite;
        playerSpriteRenders["ForearmR"].sprite = Resources.Load("CharacterSprites/Arms/Forearms/Right/ForearmR_" + armI) as Sprite;
        playerSpriteRenders["HandL"].sprite = Resources.Load("CharacterSprites/Arms/Hands/Left/HandL_" + handI) as Sprite;
        playerSpriteRenders["HandR"].sprite = Resources.Load("CharacterSprites/Arms/Hands/Right/HandR_" + handI) as Sprite;
        playerSpriteRenders["Torso"].sprite = Resources.Load("CharacterSprites/Torso/TorsoArmour_" + torsoI) as Sprite;
        playerSpriteRenders["Pelvis"].sprite = Resources.Load("CharacterSprites/Legs/Pelvis/Pelvis_" + torsoI) as Sprite;
        playerSpriteRenders["LegL"].sprite = Resources.Load("CharacterSprites/Legs/Legs/Leg_" + legI) as Sprite;
        playerSpriteRenders["LegR"].sprite = Resources.Load("CharacterSprites/Legs/Legs/Leg_" + legI) as Sprite;
        playerSpriteRenders["ShinL"].sprite = Resources.Load("CharacterSprites/Legs/Shins/Shin_" + legI) as Sprite;
        playerSpriteRenders["ShinR"].sprite = Resources.Load("CharacterSprites/Legs/Shins/Shin_" + legI) as Sprite;
        playerSpriteRenders["Head"].sprite = Resources.Load("CharacterSprites/Head/Heads/Head_" + headI) as Sprite;
        playerSpriteRenders["Eyes"].sprite = Resources.Load("CharacterSprites/Head/Eyes/Eyes_" + eyesI) as Sprite;
        playerSpriteRenders["Eyebrows"].sprite = Resources.Load("CharacterSprites/Head/Eyebrows/Eyebrows_" + eyebrowsI) as Sprite;
        playerSpriteRenders["Mouth"].sprite = Resources.Load("CharacterSprites/Head/Mouths/Mouth_" + mouthI) as Sprite;
        playerSpriteRenders["Hair"].sprite = Resources.Load("CharacterSprites/Head/Hair/Hair_" + hairI) as Sprite;
    }

    //private void OnDisable()
    //{
    //    Debug.Log("I left you");
    //    this.enabled = true;
    //    LoadData();
    //}

    void LoadData()
    {
        var serializer = new XmlSerializer(typeof(CustomizeData));

        using (var stream = new FileStream(filePath, FileMode.Open))
        {
            data = serializer.Deserialize(stream) as CustomizeData;
        }

        playerManager.strCur = data.strCur;
        playerManager.dexCur = data.dexCur;
        playerManager.conCur = data.conCur;
        playerManager.intCur = data.intCur;
        playerManager.wisCur = data.wisCur;
        playerManager.charCur = data.charCur;
        legI = data.legI;
        armI = data.armI;
        torsoI = data.torsoI;
        handI = data.handI;
        hairI = data.hairI;
        eyesI = data.eyesI;
        eyebrowsI = data.eyebrowsI;
        mouthI = data.mouthI;
        headI = data.headI;
    }
}
