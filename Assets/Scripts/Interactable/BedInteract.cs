using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BedInteract : MonoBehaviour
{
    InteractableInfo interactableInfo;
    Interact interactScript;

    void Awake()
    {
        interactableInfo = GetComponent<InteractableInfo>();
        interactScript = FindObjectOfType<Interact>();
    }

    void OnEnable()
    {
        interactScript.OnInteract += Interact;

        if (PlayerPrefs.HasKey("MailRead"))
        {
            interactableInfo.InteractText = "Go to sleep";
        }
        else
        {
            interactableInfo.InteractText = "I should read the email first.";
        }
    }

    void Interact(GameObject interactObject)
    {
        if(interactObject == gameObject && PlayerPrefs.HasKey("MailRead"))
        {
            SceneManager.LoadScene("Forest");
        }
        else if(interactObject.name == "Laptop")
        {
            interactableInfo.InteractText = "Go to sleep";
        }
    }
}
