using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource[] source;

    public Sound[] Sounds;//사운드클래스(이름과 효과음)
    public string[] Playering_name; //재생중인 사운드

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }

        
    }
    private void Start()
    {
        Playering_name = new string[source.Length];
    }
    public void SoundPlay(string SoundName)
    {
        for (int i = 0; i < Sounds.Length; i++)
        {
            if(SoundName == Sounds[i].name)
            {
                for (int j = 0; j < source.Length; j++)
                {
                    if(!source[j].isPlaying)
                    {
                        Playering_name[j] = Sounds[i].name;
                        source[j].clip = Sounds[i].clip;
                        source[j].Play();
                        return;
                    }
                }
            }
        }
    }
    public void StopSound()
    {
        for (int i = 0; i < source.Length; i++)
        {
            source[i].Stop();
        }
    }

}
