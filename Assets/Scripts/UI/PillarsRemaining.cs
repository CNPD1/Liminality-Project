using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PillarsRemaining : MonoBehaviour
{
    CameraBehaviour cameraBehaviour;
    Text text;
    
    int numPillars = 8;

    void Awake()
    {
        cameraBehaviour = FindObjectOfType<CameraBehaviour>();
        text = GetComponent<Text>();

        cameraBehaviour.HitPillar += UpdateText;
    }

    void Start()
    {
        UpdateText(null);
    }

    void UpdateText(GameObject pillarHit)
    {
        numPillars--;
        
        text.text = numPillars.ToString() + " left.";
    }
}
