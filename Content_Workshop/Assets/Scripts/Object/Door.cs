using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string Dore_Color; //문 특성
    public bool IsRight;//시계방향으로 도는지 True면 시계방향으로

    public void FadeIn()
    {
        StartCoroutine(Door_Trun());
    }
    IEnumerator Door_Trun()
    {
        float FadeOut_Time = 0f;
        float FadeOut_TimeCheck = 0.7f;
        float angle = 0;
        float angle2 = 0;
        Vector3 N_angle;
        N_angle = transform.rotation.eulerAngles;
        while (angle < 90f)
        {
            FadeOut_Time += Time.deltaTime / FadeOut_TimeCheck;
            angle = Mathf.Lerp(0, 90, FadeOut_Time);
            if(IsRight)
                transform.rotation = Quaternion.AngleAxis(N_angle.z + angle, Vector3.forward);
            else
                transform.rotation = Quaternion.AngleAxis(N_angle.z - angle, Vector3.forward);

            yield return null;
        }

        yield return null;
    }

}
