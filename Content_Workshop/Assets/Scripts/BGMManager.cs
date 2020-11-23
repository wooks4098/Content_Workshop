using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;
    private AudioSource source;

    public float BGMVolum;
    public AudioClip[] Audio;
   
    private WaitForSeconds watiTime = new WaitForSeconds(0.01f);

    private void Awake()
    {
        
        source = GetComponent<AudioSource>();
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        BGMVolum = source.volume;
    }
    private void Update()
    {
        
    }

    public void ChangeVolum(float volum)
    {
        BGMVolum = volum;
        source.volume = volum;
    }

    public void Play(int MusicTrack)
    {
        source.clip = Audio[MusicTrack];
        source.Play();
    }
    public void Stop()
    {
        source.Stop();
    }

    //public void FadeOutMusic() //점점 작아지게
    //{
    //    StopAllCoroutines();
    //    StartCoroutine("FadeOutMusicCoroutine");
    //}
    //IEnumerable FadeOutMusicCoroutine()
    //{
    //    for(float i = 1.0f; i>0f; i -= 0.1f)
    //    {
    //        source.volume = i;
    //        yield return watiTime;
    //    }
    //}
    //public void FadeInMusic() // 점점 커지게
    //{
    //    StopAllCoroutines();
    //    StartCoroutine("FadeInMusicCoroutine");
    //}
    //IEnumerable FadeInMusicCoroutine()
    //{
    //    for (float i = 0.0f; i > 1f; i += 0.1f)
    //    {
    //        source.volume = i;
    //        yield return watiTime;
    //    }
    //}







    //public void ChangeBGM()
    //{
    //    switch (SceneManager.GetActiveScene().name)
    //    {
    //        case "Title":
    //        case "Chapter_Select":
    //            if(inGameCheck)
    //            {
    //                source.clip = Audio[0];
    //                inGameCheck = false;
    //            }
    //            break;
    //        case "1-1":
    //            source.clip = Audio[1];
    //            break;
    //        case "2-1":
    //            source.clip = Audio[2];
    //            break;
    //        case "3-1":
    //            source.clip = Audio[3];
    //            break;

    //    }
    //    source.Play();
    //}

}
