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

        #endregion
        //generic constructor
        public Item()
        {

        }

        //constructor to enter values
        public Item(int id, string name, string description, string meshName)
        {
            _id = id;
            _name = name;
            _description = description;
            _mesh = meshName;
        }

    } 
}
