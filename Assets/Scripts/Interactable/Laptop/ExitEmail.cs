using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitEmail : MonoBehaviour
{
    Interact interactScript;

    void Awake()
    {
        interactScript = FindObjectOfType<Interact>();
    }

    public void Exit()
    {
        GameObject email = gameObject.transform.parent.parent.gameObject;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        interactScript.enabled = true;

        email.SetActive(false);
    }
}
