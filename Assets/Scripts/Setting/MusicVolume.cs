using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolume : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] InputField textBox;

    void Awake()
    {
        textBox = gameObject.transform.Find("TextBox").gameObject.GetComponent<InputField>();

        textBox.text = (musicSource.volume * 100).ToString();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            musicSource.volume = float.Parse(textBox.text)/100;
        }
    }
}
