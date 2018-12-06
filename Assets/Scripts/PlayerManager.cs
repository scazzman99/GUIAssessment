using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUIAssignment
{
    public class PlayerManager : MonoBehaviour
    {

        #region Variables

        [Header("Stats")]
        //char stats that will affect health, stamina and mana
        public int strCur;
        public int dexCur, conCur, intCur, wisCur, charCur;
        [Header("Health, stamina and mana values")]
        //health, stamina and mana, as well as their max values
        public float health;
        public float stamina, mana, maxMana, maxHealth, maxStamina;
        [Header("GUI styles")]
        //bar textures
        public GUIStyle healthBar;
        public GUIStyle stamBar, manaBar;
        [Header("MiniMap and Icon")]
        //Render texture for the mini map
        public RenderTexture miniMap;
        public RenderTexture icon;


        //icon camera
        public Camera iconCam;
        float baseHealth = 100f, baseStamina = 100f, baseMana = 100f;
        //screen width and height
        float scrW, scrH;
        #endregion



        // Use this for initialization
        void Start()
        {
            //pause menu is set off on start
            PauseMenu.isPaused = false;
            //get values for max health, stamina and mana
            GetStatVals();
            //set the current health, stamina and mana to their max values
            health = maxHealth;
            stamina = maxStamina;
            mana = maxMana;
            //set the icon camera background to be green
            iconCam.backgroundColor = Color.green;
            

        }

        void GetStatVals()
        {
            maxHealth = baseHealth + (conCur * 5);
            maxStamina = baseStamina + (conCur * 4);
            maxMana = baseMana + (wisCur * 5);

        }

        private void OnGUI()
        {
            scrW = Screen.width / 16f;
            scrH = Screen.height / 9f;

            //if neither the pause menu or inventory are showing
            if (!PauseMenu.isPaused && !Inventory.showInv)
            {
                #region Health, stamina and mana bars
                //empty rect to have health bar sit on top of
                GUI.Box(new Rect(scrW, scrH * 8f, scrW * 4f, scrH * 0.5f), "");
                //Health bar fill
                GUI.Box(new Rect(scrW, scrH * 8f, scrW * 4f * (health / maxHealth), scrH * 0.5f), "Health: " + health + "/" + maxHealth, healthBar);

                //do the same for mana and stamina bars

                //empty rect to have health bar sit on top of
                GUI.Box(new Rect(scrW * 6f, scrH * 8f, scrW * 4f, scrH * 0.5f), "");
                //Stamina bar fill
                GUI.Box(new Rect(scrW * 6f, scrH * 8f, scrW * 4f * (stamina / maxStamina), scrH * 0.5f), "Stamina: " + stamina + "/" + maxStamina, stamBar);

                GUI.Box(new Rect(scrW * 11f, scrH * 8f, scrW * 4f, scrH * 0.5f), "");
                //Mana bar fill
                GUI.Box(new Rect(scrW * 11f, scrH * 8f, scrW * 4f * (mana / maxMana), scrH * 0.5f), "Mana: " + mana + "/" + maxMana, manaBar);
                #endregion

                //Draw the mini map to screen
                GUI.DrawTexture(new Rect(scrW * 13.75f, scrH * 0.25f, scrW * 2, scrH * 2), miniMap);
                //draw char icon
                GUI.DrawTexture(new Rect(scrW * 0.5f, scrH * 0.25f, scrW * 1.5f, scrH * 1.5f), icon);
                //change the background colour of the camera based on character health
                iconCam.backgroundColor = Color.Lerp(Color.red, Color.green, (health / maxHealth));
            }

        }
    } 
}
