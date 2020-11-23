using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OriginManager : MonoBehaviour
{

    bool checkForDupes;
    GameObject[] characters;

    void OnEnable()
    {
        checkForDupes = true;
        characters = GameObject.FindGameObjectsWithTag("3D Character");
    }

    // Update is called once per frame
    void Update()
    {
        if (checkForDupes)
        {
            if(characters.Length < 2)
            {
                characters = GameObject.FindGameObjectsWithTag("3D Character");
            }
            foreach(GameObject character in characters)
            {
                PhotonView photonView = character.GetComponent<PhotonView>();
                if (!photonView.IsMine)
                {
                    character.GetComponent<Handle3DEyelids>().enabled = false;
                    checkForDupes = false;
                    Debug.Log("Disabled character \"" + character.name + "\"'s Handle3DEyelids script, as it is not the local character");
                }
            }
        }
    }
}
