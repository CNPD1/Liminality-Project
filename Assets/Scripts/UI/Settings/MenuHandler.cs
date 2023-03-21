using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            foreach (Transform child in transform)
            {
                GameObject childObject = child.gameObject;
                childObject.SetActive(!childObject.activeSelf);
            }

            Cursor.visible = !Cursor.visible;
            Cursor.lockState = Cursor.lockState = (Cursor.lockState == CursorLockMode.Locked) ? CursorLockMode.None : CursorLockMode.Locked;

            Time.timeScale = 1 - Time.timeScale;
        }
    }
}
