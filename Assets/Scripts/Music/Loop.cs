using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop : MonoBehaviour
{
    AudioSource[] audioSources;
    int currentSource = 1;

    [SerializeField] float stopTime;

    void Awake()
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
        print(currentSource);
        audioSources[currentSource].Play();
        print(audioSources[currentSource].isPlaying);
        yield return new WaitForSecondsRealtime(stopTime);
        StartCoroutine("Play");
    }
}
