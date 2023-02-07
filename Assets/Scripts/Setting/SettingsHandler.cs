using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsHandler : MonoBehaviour
{
    GameObject info;

    void Awake()
    {
        info = gameObject.transform.Find("Info").gameObject;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        info.SetActive(true);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                TurnOn();
            }
            else
            {
                TurnOff();
            }
        }
    }

    void TurnOn()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }

        info.SetActive(false);
    }

    void TurnOff()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        info.SetActive(true);
    }
}
