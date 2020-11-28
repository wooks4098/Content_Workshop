using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    public GameObject[] Star;
    public float time;
    public float startime;


    private void Update()
    {
        time += Time.deltaTime;
        if(time > startime)
        {
            int starcheck = 0;
            for(int i = 0; i<Star.Length; i++)
            {
                if(Star[i].activeSelf == true)
                    starcheck++;
            }
            if (starcheck == 6)
                return;
            while(true)
            {
                int rand = Random.Range(0, 5);
                if (Star[rand].activeSelf == false)
                {
                    Star[rand].SetActive(true);
                    break;
                }
            }
            time = 0;
        }
    }
}
