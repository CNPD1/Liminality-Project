using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BedInteract : MonoBehaviour
{
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
            SceneManager.LoadScene(1);
        }
    }
}
