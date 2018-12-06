using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUIAssignment
{
    public class InventoryData
    {

    }


    public class Inventory : MonoBehaviour
    {

        #region Variables
        //this list in universal and only one will ever exist so we make it static.
        public static List<Item> inventory = new List<Item>();
        //bool is static as we need to check it and change it from pause menu scripts etc
        public static bool showInv = false;
        //Item we are currently hovering over/interacting with
        public Item currentItem;

        //coordinates for the scroll view we will make in OnGUI
        Vector2 scrollPos = Vector2.zero;
        Vector2 scrR;
        public float inventoryCap;
        PlayerManager playerManager;
        public GUISkin skin;
        #endregion


        #region Start and Update
        // Use this for initialization
        void Start()
        {
            
            scrR = new Vector2(Screen.width / 16, Screen.height / 9);
            inventoryCap = scrR.y / 0.25f;
            playerManager = GetComponent<PlayerManager>();

            
            inventory.Add(ItemLibrary.CreateItem(0));
            inventory.Add(ItemLibrary.CreateItem(1));
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.I) && !PauseMenu.isPaused)
            {
                ToggleInventory();
            }

            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                if (showInv)
                {
                    inventory.Add(ItemLibrary.CreateItem(0));
                    inventory.Add(ItemLibrary.CreateItem(0));
                    inventory.Add(ItemLibrary.CreateItem(0));
                    inventory.Add(ItemLibrary.CreateItem(0));
                    inventory.Add(ItemLibrary.CreateItem(0));
                    inventory.Add(ItemLibrary.CreateItem(0));
                    inventory.Add(ItemLibrary.CreateItem(0));
                    inventory.Add(ItemLibrary.CreateItem(0));
                    inventory.Add(ItemLibrary.CreateItem(0));
                    inventory.Add(ItemLibrary.CreateItem(0));
                }
            }
        }

        #endregion

        
        void ToggleInventory()
        {
            //if the inventory is being shown
            if (showInv)
            {
                //flip the bool to toggle inventory
                showInv = !showInv;
                //Allow time to move
                Time.timeScale = 1;
                //lock the cursor to the screen and hide the cursor
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            //else
            else
            {
                showInv = !showInv;
                //stop time while in menu
                Time.timeScale = 0;
                //unrestrict the cursor movement
                Cursor.lockState = CursorLockMode.None;
                //allow the cursor to be seen
                Cursor.visible = true;
            }
        }

        private void OnGUI()
        {
            float scrW = Screen.width / 16;
            float scrH = Screen.height / 9;
            GUI.skin = skin;

            if (!PauseMenu.isPaused)
            {
                if (showInv)
                {
                    GUI.Box(new Rect(0f, 0f, Screen.width, Screen.height), "Inventory");

                    //get the inventory to display
                    GetInventoryList();

                    if(currentItem != null)
                    {
                        GUI.DrawTexture(new Rect(9f * scrR.x, 2f * scrR.y, scrR.x * 2f, scrR.y * 2f), currentItem.Icon);
                        Debug.Log(currentItem.Icon);
                        GUI.BeginGroup(new Rect(8f * scrR.x, 4.25f * scrR.y, 4f * scrR.x, scrR.y * 1.75f), "");
                        GUI.Box(new Rect(0f, 0f, 4 * scrR.x, scrR.y), currentItem.Description);
                        if(currentItem.Type == ItemType.Consumable)
                        {
                            if(GUI.Button(new Rect(0f, 1f * scrR.y, 2f * scrR.x, scrR.y * 0.5f), "Consume"))
                            {
                                playerManager.health += currentItem.Health;
                                inventory.Remove(currentItem);
                                currentItem = null;
                            }
                        }

                        if(GUI.Button(new Rect(2f * scrR.x, 1f * scrR.y, 2f * scrR.x, scrR.y * 0.5f), "Discard"))
                        {
                            inventory.Remove(currentItem);
                            currentItem = null;
                        }
                        GUI.EndGroup();
                        
                    }
                }
            }
        }

        //get the inventory to display when menu is open
        void GetInventoryList()
        {
            
            //if the inventory size is greater than what will fit on screen;
            if (inventory.Count <= 35)
            {
                for (int i = 0; i < inventory.Count; i++)
                {
                    if (GUI.Button(new Rect(0.5f * scrR.x, 0.25f * scrR.y + (i * 0.25f * scrR.y), 3f * scrR.x, 0.25f * scrR.y), new GUIContent(inventory[i].Name, inventory[i].Description)))
                    {

                        currentItem = inventory[i];
                        Debug.Log(currentItem.Name);
                        Debug.Log(currentItem != null);
                    }
                }
            }
            else
            {
                //start the scroll view. Have it need to scroll just after we run out of space
                scrollPos = GUI.BeginScrollView(new Rect(0, 0.25f * scrR.y, scrR.x * 5f, scrR.y * 8.75f), scrollPos, new Rect(0, 0, 0, 8.75f * scrR.y + ((inventory.Count - 35) * 0.25f * scrR.y)), false, true);

                for (int i = 0; i < inventory.Count; i++)
                {
                    if(GUI.Button(new Rect(0,0 + (i * 0.25f * scrR.y), 3f * scrR.x, scrR.y * 0.25f), new GUIContent(inventory[i].Name, inventory[i].Description)))
                    {
                        currentItem = inventory[i];
                        GUI.tooltip = inventory[i].Description;
                        Debug.Log(currentItem.Name);
                    }
                }

                GUI.EndScrollView();
            }
        }


    }
}
