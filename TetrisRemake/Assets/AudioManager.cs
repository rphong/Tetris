using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
        }
    }

    // Update is called once per frame
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        s.source.volume = s.volume;
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        s.source.volume = s.volume;
        s.source.Stop();
    }
}
