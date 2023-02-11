using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sensitivity : MonoBehaviour
{
    [SerializeField] InputField textBox;
    PlayerCamera playerCamera;

    void Awake()
    {
        textBox = gameObject.transform.Find("TextBox").gameObject.GetComponent<InputField>();

        playerCamera = FindObjectOfType<PlayerCamera>();
    }

    void Start()
    {
        textBox.text = (PlayerPrefs.GetFloat("Sensitivity")).ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            playerCamera.UpdateValue(float.Parse(textBox.text));
        }
    }
}
