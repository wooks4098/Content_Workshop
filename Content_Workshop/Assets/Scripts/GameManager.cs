using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public Image FadeOut_Img;

    static bool Chap_1_ImgCheck = false;
    public GameObject Chap_1_Image;
    public GameObject Chap_2_Image;
    public GameObject Player2;

    public float time = 0;


    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "1-1" && Chap_1_ImgCheck)
            Chap_1_Image.SetActive(false);
        Chap_1_ImgCheck = true;
        if (instance != null)
        {
            return;
        }
        instance = this;

        FadeIn();
    }
    private void Update()
    {

        time += Time.deltaTime;
        if(SceneManager.GetActiveScene().name == "1-1" && time >=11.3f)
            Chap_1_Image.SetActive(false);
        if(SceneManager.GetActiveScene().name == "2-1")
        {
            if(time>= 5)
                Chap_2_Image.SetActive(true);
            if (time > 19)
            {
                Player2.SetActive(true);
                Chap_2_Image.SetActive(false);
            }

        }
    }
    #region Fade In/Out 화면전환효과
    public void FadeOut()
    {
        StartCoroutine(Fade("FadeOut"));
    }
    public void FadeIn()
    {
        StartCoroutine(Fade("FadeIn"));
    }

    IEnumerator Fade(string FadeCheck)
    {
        float FadeOut_Time = 0f;
        float FadeOut_TimeCheck = 1f;
        if (FadeCheck == "FadeOut")
        {
            FadeOut_Img.gameObject.SetActive(true);
            Color alpha = new Color(0, 0, 0, 0);
            while (alpha.a < 1f)
            {
                FadeOut_Time += Time.deltaTime / FadeOut_TimeCheck;
                alpha.a = Mathf.Lerp(0, 1, FadeOut_Time);
                FadeOut_Img.color = alpha;
                yield return null;
            }
            FadeOut_Time = 0;

        }
        else if (FadeCheck == "FadeIn")
        {
            FadeOut_Img.gameObject.SetActive(true);
            Color alpha = new Color(0, 0, 0, 255);
            while (alpha.a > 0f)
            {
                FadeOut_Time += Time.deltaTime / FadeOut_TimeCheck;
                alpha.a = Mathf.Lerp(1, 0, FadeOut_Time);
                FadeOut_Img.color = alpha;
                yield return null;
            }
            FadeOut_Img.gameObject.SetActive(false);
            FadeOut_Time = 0;
        }
        yield return null;
    }
    #endregion

    //public void Die()
    //{
    //    FadeOut();
    //    Invoke("SceneReLoad", 1.3f);
    //}
    //void SceneReLoad()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}
}
