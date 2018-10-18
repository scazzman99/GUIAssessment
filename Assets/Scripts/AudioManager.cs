using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GUIAssignment
{
    public class AudioManager : MonoBehaviour
    {

        void Start()
        {
            //Keep the audio manager between scenes
            DontDestroyOnLoad(this);

            //if there is more than 1 of the audio manager
            if(FindObjectsOfType(this.GetType()).Length > 1)
            {
                //eliminate 1
                Destroy(gameObject);
            }
        }

    }
}
