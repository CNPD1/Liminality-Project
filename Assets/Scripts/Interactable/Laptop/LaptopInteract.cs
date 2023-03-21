using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopInteract : MonoBehaviour
{
    [SerializeField] GameObject emailUi;
    Interact interactScript;

    void Awake()
    {
        interactScript = FindObjectOfType<Interact>();
    }

    void OnEnable()
    {
        interactScript.OnInteract += Interact;
    }

    void Interact(GameObject interactObject)
    {
        if(interactObject == gameObject)
        {
            emailUi.SetActive(true);
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            interactScript.enabled = false;
        }
    }
}
