using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Bullet_Manager : MonoBehaviour
{
    GameObject[] Bullet;//총알 담을 공간
    GameObject[] Boss_Bullet;//보스 총알 담을 공간

    public GameObject Bullet_Prefab;//총알 프리펩
    public GameObject Boss_Bullet_Prefab;//보스 총알 프리펩

    private void Awake()
    {
        Bullet = new GameObject[20];
        Boss_Bullet = new GameObject[360];
        Generate();
    }

    void Generate()
    {
        for(int i = 0; i< Bullet.Length; i++)
        {
            Bullet[i] = Instantiate(Bullet_Prefab);
            Bullet[i].SetActive(false);
        }
        //if (SceneManager.GetActiveScene().name != "InGame")
        //    return;
        for (int i = 0; i < Boss_Bullet.Length; i++)
        {
            Boss_Bullet[i] = Instantiate(Boss_Bullet_Prefab);
            Boss_Bullet[i].SetActive(false);
        }

    }

    public GameObject MakeBullet(string BulletName)
    {
        switch (BulletName)
        {
            case "Player":
                for (int i = 0; i < Bullet.Length; i++)
                {
                    if (Bullet[i].activeSelf == false)
                    {
                        Bullet[i].SetActive(true);
                        Bullet[i].GetComponent<Bullet>().Appear();
                        return Bullet[i];
                    }
                }
            break;
            case "Boss":
                for (int i = 0; i < Boss_Bullet.Length; i++)
                {
                    if (Boss_Bullet[i].activeSelf == false)
                    {
                        Boss_Bullet[i].SetActive(true);
                        return Boss_Bullet[i];
                    }
                }
                break;

        }

        
        return null;
    }
}
