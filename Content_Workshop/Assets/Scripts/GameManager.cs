using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public Image FadeOut_Img;





    void Awake()
    {

        if (instance != null)
        {
            return;
        }
        instance = this;

        FadeIn();
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

}
