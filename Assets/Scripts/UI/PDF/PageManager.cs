using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageManager : MonoBehaviour
{
    RawImage rawImage;

    [SerializeField] Texture2D[] pages;
    GameObject nextButton;
    GameObject backButton;

    int currentPage;

    void Awake()
    {
        rawImage = GetComponent<RawImage>();

        nextButton = transform.Find("Next").gameObject;
        backButton = transform.Find("Back").gameObject;
    }

    public void NextPage()
    {
        currentPage++;

        if(currentPage == pages.Length - 1)
        {
            nextButton.SetActive(false);
        }

        backButton.SetActive(true);

        rawImage.texture = pages[currentPage];
    }

    public void BackPage()
    {
        currentPage--;

        if (currentPage == 0)
        {
            backButton.SetActive(false);
        }

        nextButton.SetActive(true);

        rawImage.texture = pages[currentPage];
    }
}
