using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerHelper : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.P;
    public AudioSource audiosource;
    

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            Play();
        }
    }

    public void Play()
    {
        audiosource.Play();
    }

   



}
