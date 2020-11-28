using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    BGMManager BGM;  //배경음 메니저
    public Slider BGM_Slider; //배경음 조절 슬라이더
    SoundManager Sound; // 효과음 메니저
    public Slider Sound_Slider; // 효과음 조절 슬라이더

    public Image FadeOut_Img;
    static bool StartCheck = false;
    static bool Chap_1_ImgCheck = false;
    public GameObject Chap_1_Image;
    public GameObject Chap_2_Image;
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Pause;

    public static bool Chap1_Check = false;
    public static bool Chap2_Check = false;
    public static bool Chap3_Check = false;
    public Button Chap1_Button;
    public Button Chap2_Button;
    public Button Chap3_Button;

    public GameObject How;

    public Sprite[] Chap_sprite;

    public float time = 0;
    public static bool inGameCheck = true; //게임창에 들어갔는지 체크

    void Awake()
    {
        BGM = FindObjectOfType<BGMManager>();
        Sound = FindObjectOfType<SoundManager>();
        if (SceneManager.GetActiveScene().name != "Title" && SceneManager.GetActiveScene().name != "Chapter_Select")
        {
           
            Sound_Slider.value = Sound.SoundVolum;
            BGM_Slider.value = BGM.BGMVolum;
        }
        //if (SceneManager.GetActiveScene().name == "1-1" && Chap_1_ImgCheck)
        //    Chap_1_Image.SetActive(false);
        //Chap_1_ImgCheck = true;
        if (instance != null)
        {
            return;
        }
        instance = this;


    }

    private void Start()
    {
        BGM_Play();
        FadeIn();

    }

    private void Update()
    {
        Close_HowtoPlay();
        Chap_ButtonSprite();
        Show_Pause();
        Cutscene();
        BGM_Volum();
        Sound_Volum();
    }


    public void Show_Howtoplay()
    {
        How.SetActive(true);
    }

    public void Close_HowtoPlay()
    {
        if (SceneManager.GetActiveScene().name != "Title")
            return;
        if(Input.GetKey(KeyCode.Escape))
            How.SetActive(false);
    }
    public void Close_HowtoPlay_Button()
    {
         How.SetActive(false);
    }
    void Cutscene()
    {
        time += Time.deltaTime;
        if (SceneManager.GetActiveScene().name == "1-1" && time >= 11.3f)
        {
            Player1.SetActive(true);
            Chap_1_Image.SetActive(false);

        }
        if (SceneManager.GetActiveScene().name == "2-1")
        {
            if (time > 19)
            {
                Player2.SetActive(true);
                Player1.SetActive(true);
                Chap_2_Image.SetActive(false);

            }
            else if (time > 18.5f)
            {
                Player1.SetActive(true);
            }
            else if(time >= 5)
            {
                Player1.SetActive(false);
                Chap_2_Image.SetActive(true);

            }
            
           

        }
    }

    #region 챕터선택 UI
    void Chap_ButtonSprite()//챕터 선택 버튼 스프라이트 변경
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "1-1":
                Chap1_Check = true;
                Chap2_Check = true;
                break;
            case "2-1":
            case "2-2":
                Chap3_Check = true;
                break;
            case "3-1":
            case "InGame":

                break;
        }
        if (SceneManager.GetActiveScene().name != "Chapter_Select")
            return;

        if (Chap1_Check)
            Chap1_Button.GetComponent<Image>().sprite = Chap_sprite[1];
        else
            Chap1_Button.GetComponent<Image>().sprite = Chap_sprite[0];

        if (Chap2_Check)
        {
            Chap2_Button.interactable = true;

            Chap2_Button.GetComponent<Image>().sprite = Chap_sprite[3];
        }
        else
        {
            Chap2_Button.interactable = false;

            Chap2_Button.GetComponent<Image>().sprite = Chap_sprite[2];
        }

        if (Chap3_Check)
        {
            Chap3_Button.interactable = true;

            Chap3_Button.GetComponent<Image>().sprite = Chap_sprite[5];
        }
        else
        {
            Chap3_Button.interactable = false;

            Chap3_Button.GetComponent<Image>().sprite = Chap_sprite[4];
        }

    }
    #endregion

    #region 소리
    /// <summary>
    /// 배경음
    /// </summary>
    void BGM_Play()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Title":
            case "Chapter_Select":
                if (inGameCheck)
                {
                    BGM.Play(0);
                    inGameCheck = false;
                }
                break;
            case "1-1":
            case "InGame":
                if (!inGameCheck)
                {
                    BGM.Play(1);
                    inGameCheck = true;
                }
                break;
   
            case "2-1":
                if (!inGameCheck)
                {
                    BGM.Play(2);
                    inGameCheck = true;
                }
                break;
            case "3-1":
                if (!inGameCheck)
                {
                    BGM.Play(3);
                    inGameCheck = true;
                }
                break;

        }
    }

    public void BGM_Volum()
    {
        BGM.ChangeVolum(BGM_Slider.value);
    }
    /// <summary>
    /// 효과음
    /// </summary>
    /// 
    void Sound_Volum()
    {
        Sound.ChangeVolum(Sound_Slider.value);
    }
    #endregion

    #region UI
    void Show_Pause()
    {
        if (SceneManager.GetActiveScene().name != "Title" || SceneManager.GetActiveScene().name != "Chapter_Select")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Pause.activeSelf)
                {
                    Pause.SetActive(false);
                    Time.timeScale = 1;
                }
                else
                {
                    Pause.SetActive(true);
                    Time.timeScale = 0;
                }
            }
        }
    }
    public void Play()
    {
        Pause.SetActive(false);
        Time.timeScale = 1;
    }
    public void RE()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        FadeIn();
    }
    public void Chap()
    {
        SceneManager.LoadScene("Chapter_Select");
        Time.timeScale = 1;
    }
    #endregion

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