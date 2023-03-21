using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject controls;

    public void Toggle()
    {
        menu.SetActive(!menu.activeSelf);
        controls.SetActive(!controls.activeSelf);
    }
}
