using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUIAssignment
{
    public class Item
    {
        #region ConstructorPrivateVars
        //using _ to denote private version of variable used in constructor
        int _id;
        string _name, _description, _mesh;
        Texture2D _icon;
        ItemType _type;
        float _health;

        #endregion
        
        //use properties to access private vars from outside script
        #region Public properties Get/Set
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string Mesh
        {
            get { return _mesh; }
            set { _mesh = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Texture2D Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }

        public ItemType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public float Health
        {
            get { return _health; }
            set { _health = value; }
        }

        #endregion

        //constructor to enter values
        public Item(int id, string name, string description, Texture2D icon, float health, ItemType type)
        {
            _id = id;
            _name = name;
            _description = description;
            _icon = icon;
            _type = type;
            _health = health;
        }

    }

    public enum ItemType
    {
        Consumable,
        Misc
    }
}
