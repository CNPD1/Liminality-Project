using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundVolume : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] InputField textBox;

    void Awake()
    {
        source = GameObject.Find("BackgroundSound").GetComponent<AudioSource>();
        textBox = gameObject.transform.Find("TextBox").gameObject.GetComponent<InputField>();

        if(!PlayerPrefs.HasKey("BackgroundVolume"))
        {
            PlayerPrefs.SetFloat("BackgroundVolume", 0.4f);
        }

        textBox.text = (PlayerPrefs.GetFloat("BackgroundVolume")*100).ToString();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            UpdateVolume(float.Parse(textBox.text)/100);
        }
    }

    void UpdateVolume(float newVolume)
    {
        source.volume = newVolume;
        PlayerPrefs.SetFloat("BackgroundVolume", newVolume);
    }
}
