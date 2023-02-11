using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailSound : MonoBehaviour
{
    Interact interactScript;
    AudioSource sound;

    GameObject laptopObj;

    void Awake()
    {
        interactScript = FindObjectOfType<Interact>();
        sound = GetComponent<AudioSource>();
        laptopObj = gameObject.transform.parent.gameObject;

        if(!PlayerPrefs.HasKey("MailRead"))
        {
            sound.Play();
            interactScript.OnInteract += TurnOff;
        }
    }

    void TurnOff(GameObject interactObj)
    {
        if(interactObj == laptopObj)
        {
            sound.loop = false;
            PlayerPrefs.SetInt("MailRead", 1);
        }
    }
}
