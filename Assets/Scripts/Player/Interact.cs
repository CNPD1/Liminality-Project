using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    public delegate void InteractEvent(GameObject interacted);
    public event InteractEvent OnInteract;

    [Header("Camera")]
    [SerializeField] Transform CameraPos;

    [Header("Raycast")]
    [SerializeField] RaycastHit rayHit;

    [Header("Cursor")]
    [SerializeField] RawImage cursorRawImage;
    [SerializeField] Texture2D baseCursor;
    [SerializeField] Texture2D interactCursor;

    [Header("Interact")]
    [SerializeField] GameObject interactEmptyObj;
    [SerializeField] Text interactTextLabel;
    public GameObject interactObject;
    InteractableInfo interactableInfo;

    void Update()
    {
        InteractRaycast();
        InteractHandler();

        if(Input.GetMouseButtonDown(0) && interactObject)
        {
            OnInteract(interactObject);
        }
    }

    void InteractRaycast()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));
        Physics.Raycast(Camera.main.transform.position, ray.direction, out rayHit, 5f);
    }

    void InteractHandler()
    {
        cursorRawImage.texture = baseCursor;
        interactEmptyObj.SetActive(false);
        interactObject = null;

        if (rayHit.collider == null)
        {
            return;
        }

        if (rayHit.collider.gameObject.CompareTag("Interactable"))
        {
            interactObject = rayHit.collider.gameObject;
            interactableInfo = interactObject.GetComponent<InteractableInfo>();

            interactEmptyObj.SetActive(true);

            cursorRawImage.texture = interactCursor;
            interactTextLabel.text = interactableInfo.InteractText;
        }
    }
}
