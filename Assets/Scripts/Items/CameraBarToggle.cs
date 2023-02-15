using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraBarToggle : MonoBehaviour
{
    float DURATION = 1.8f;

    RectTransform barTransform;
    RawImage cameraIcon;

    float maxWidth;
    float curProgress;

    void Awake()
    {
        barTransform = transform.Find("Bar").gameObject.GetComponent<RectTransform>();
        cameraIcon = GetComponentInChildren<RawImage>();

        maxWidth = barTransform.sizeDelta.x;
    }

    void Update()
    {
        curProgress -= 1 * Time.deltaTime / DURATION;
        barTransform.sizeDelta = new Vector2(curProgress * maxWidth, barTransform.sizeDelta.y);
        cameraIcon.color = new Color(1, 1, 1, curProgress);

        if(curProgress <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        barTransform.sizeDelta = new Vector2(maxWidth, barTransform.sizeDelta.y);
        curProgress = 1;
    }
}
