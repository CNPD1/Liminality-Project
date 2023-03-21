using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePDF : MonoBehaviour
{
    GameObject pdf;

    void Awake()
    {
        pdf = transform.Find("PDF").gameObject;
    }

    public void Toggle()
    {
        pdf.SetActive(!pdf.activeSelf);
    }
}
