using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sensitivity : MonoBehaviour
{
    [SerializeField] InputField textBox;

    void Awake()
    {
        textBox = gameObject.transform.Find("TextBox").gameObject.GetComponent<InputField>();

        textBox.text = (PlayerCamera.sensitivity).ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlayerCamera.sensitivity = float.Parse(textBox.text);
        }
    }
}
