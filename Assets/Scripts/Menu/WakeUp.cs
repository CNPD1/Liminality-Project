using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WakeUp : MonoBehaviour
{
    Image buttonImage;
    Button button;
    Text text;

    void Awake()
    {
        buttonImage = GetComponent<Image>();
        button = GetComponent<Button>();
        text = GetComponentInChildren<Text>();

        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            button.enabled = false;
            buttonImage.color = Color.gray;
            text.text = "You are already awake.";
        }
    }

    public void Return()
    {
        SceneManager.LoadScene(0);
    }
}
