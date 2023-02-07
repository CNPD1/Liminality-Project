using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopInteract : MonoBehaviour
{
    [SerializeField] GameObject emailUi;

    GameObject canvas;
    Interact interactScript;

    void Awake()
    {
        interactScript = FindObjectOfType<Interact>();
        canvas = FindObjectOfType<Canvas>().gameObject;
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
            GameObject newEmail = Instantiate(emailUi);

            newEmail.transform.SetParent(canvas.transform);
            newEmail.transform.localPosition = new Vector3(0, 0, 0);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            interactScript.enabled = false;
        }
    }
}
