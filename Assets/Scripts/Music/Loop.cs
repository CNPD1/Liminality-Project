using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop : MonoBehaviour
{
    AudioSource[] audioSources;
    int currentSource;

    [SerializeField] float stopTime;

    void Start()
    {
        audioSources = GetComponentsInChildren<AudioSource>();

        StartCoroutine("Play");
    }

    void Update()
    {
        foreach(AudioSource source in audioSources)
        {
            source.volume = PlayerPrefs.GetFloat("BackgroundVolume");
        }
    }

    IEnumerator Play()
    {
        currentSource = 1 - currentSource;

        audioSources[currentSource].Play();
        yield return new WaitForSecondsRealtime(stopTime);
        StartCoroutine("Play");
    }
}
