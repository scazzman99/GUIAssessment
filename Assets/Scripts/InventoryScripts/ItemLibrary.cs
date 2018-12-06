using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUIAssignment
{
    //its a static class as it will use static methods
    public static class ItemLibrary
    {

        

        public static Item CreateItem(int ID)
        {
            #region Variables
            //all of these are used to set an items variables
            int id = 0;
            string name = "";
            string description = "";
            //icon is a string here as we will load it from resources
            string icon = "";
            float health = 0f;
            ItemType type = ItemType.Misc;
            #endregion

            switch (ID)
            {
                case 0:
                    id = 0;
                    name = "Space Apple";
                    description = "Is Irradiated";
                    icon = "SpaceApple_Icon";
                    type = ItemType.Consumable;
                    health = 30f;
                    break;

                case 1:
                    id = 1;
                    name = "Poison Space Juice";
                    description = "Maybe avoid drinking this";
                    icon = "PoisonSpaceJuice_Icon";
                    type = ItemType.Consumable;
                    health = -90f;
                    break;

                default:
                    id = 0;
                    name = "Space Apple";
                    description = "Is Irradiated";
                    icon = "SpaceApple_Icon";
                    type = ItemType.Consumable;
                    health = 30f;
                    break;
            }

            Texture2D tex = Resources.Load("Icons/" + icon) as Texture2D;
                        
            return new Item(id, name, description, tex, health, type);
        }

    } 
}
