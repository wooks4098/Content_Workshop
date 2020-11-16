using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Potal : MonoBehaviour
{
    public bool Player1Check;
    public bool Player2Check;
    public string SceneName;
    

    private void Start()
    {
        Player1Check = false;
        Player2Check = false;
    }

    private void Update()
    {
        
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(SceneName);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.gameObject.tag == "Player" && collision.gameObject.tag == "Player2")
        //    SceneManager.LoadScene("Title");
        if (collision.gameObject.tag == "Player")
            Player1Check = true;

        if(collision.gameObject.tag == "Player2")
            Player2Check = true;
        if(SceneManager.GetActiveScene().name == "1-1")
        {
            if (Player1Check)
            {
                //GameObject.Find("Canvas").GetComponent<FadeFlow>().FadeOut();
                GameManager.instance.FadeOut();
                Invoke("ChangeScene", 1f);
            }
        }
        else
        {
            if (Player1Check && Player2Check)
            {
                //GameObject.Find("Canvas").GetComponent<FadeFlow>().FadeOut();
                GameManager.instance.FadeOut();
                Invoke("ChangeScene", 1f);
            }
        }
        


    }
}
