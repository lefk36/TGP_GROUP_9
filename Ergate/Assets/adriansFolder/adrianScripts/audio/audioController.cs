using UnityEngine.Audio;
using System;
using UnityEngine;

public class audioController : MonoBehaviour
{
    public Sound[] sounds;
    

    public AudioMixerGroup m_soundEffects;
    public AudioMixerGroup m_music;

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
        foreach(Sound s in sounds)
        {
            s.m_source = gameObject.AddComponent<AudioSource>();
            s.m_source.outputAudioMixerGroup = s.m_mixerGroup;
           
            
            s.m_source.clip = s.m_clip;
            s.m_source.loop = s.m_loop;

            s.m_source.volume = s.m_volume;
            s.m_source.pitch = s.m_pitch;
        }

        play("ambientMusic");

        
    }
    public void pauseClip(string name)
    {
        Sound sound;
        for (int i = 0; i < sounds.Length; i++)
        {
            if (name == sounds[i].m_name)
            {
                sound = sounds[i];
                sound.m_source.Pause();
                
                break;
            }
            else
            {
                Debug.Log("no sound exists with that name");
            }
        }
    }
   

    public void play(string name)
    {
        Sound sound;
        for(int i = 0; i < sounds.Length; i++)
        {
            if(name == sounds[i].m_name)
            {
                sound = sounds[i];
                sound.m_source.Play();
                break;
            }
            else
            {
                Debug.Log("no sound exists with that name");
            }
        }

        
        
     
    }

    
}
