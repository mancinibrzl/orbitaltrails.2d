using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRandomPlayAudioClips : MonoBehaviour
{
    public List<AudioClip> audioClipList;

    public List<AudioSource> audioSourceList;

    private int index = 0;

    public void PlayRandom()
    {
        if (index >= audioSourceList.Count) index = 0;

        var audioSource = audioSourceList[index];


        audioSource.clip = audioClipList[Random.Range(0, audioClipList.Count)];
        audioSource.Play();

        index++;
    }
}
