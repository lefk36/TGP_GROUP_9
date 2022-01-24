using UnityEngine.Audio;
using System;
using UnityEngine;

public class audioController : MonoBehaviour
{
    public Sound[] sounds;

    public static audioController m_instance;

    // Start is called before the first frame update
    private void Awake()
    {
        if(m_instance == null)
        {
            m_instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach(Sound s in sounds)
        {
            s.m_source = gameObject.AddComponent<AudioSource>();
            s.m_source.clip = s.m_clip;

            s.m_source.volume = s.m_volume;
            s.m_source.pitch = s.m_pitch;
        }
    }

    public void play(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.m_name == name);
        s.m_source.Play();
     
    }
}
