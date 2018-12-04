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
            string mesh = "";
            //icon is a string here as we will load it from resources
            string icon = "";
            #endregion

            switch (ID)
            {
                case 0:
                    id = 0;
                    name = "Space Apple";
                    description = "Is Irradiated";
                    icon = "SpaceApple_Icon";
                    mesh = "SpaceApple_Mesh";
                    break;


                default:
                    id = 0;
                    name = "Space Apple";
                    description = "Is Irradiated";
                    icon = "SpaceApple_Icon";
                    mesh = "SpaceApple_Mesh";
                    break;
            }

            //use generic constructor
            Item item = new Item
            {
                Name = name,
                Description = description,
                Id = id,
                Icon = Resources.Load("Icons/" + icon) as Texture2D

            };

            
            return item;
        }

    } 
}
