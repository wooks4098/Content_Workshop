using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Manager : MonoBehaviour
{
    GameObject[] Bullet;//총알 담을 공간

    public GameObject Bullet_Prefab;//총알 프리펩

    private void Awake()
    {
        Bullet = new GameObject[40];
        Generate();
    }

    void Generate()
    {
        for(int i = 0; i< Bullet.Length; i++)
        {
            Bullet[i] = Instantiate(Bullet_Prefab);
            Bullet[i].SetActive(false);
        }

    }

    public GameObject MakeBullet()
    {
        for(int i = 0; i< Bullet.Length; i++)
        {
            if (Bullet[i].activeSelf == false)
            {
                Bullet[i].SetActive(true);
                Bullet[i].GetComponent<Bullet>().Appear();
                return Bullet[i];
            }
        }
        return null;
    }
}
