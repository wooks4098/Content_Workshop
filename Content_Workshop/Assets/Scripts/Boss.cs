using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    enum Boss_State {Standing, Flying};
    public int Boss_Phase;
    public int HP;
    public int Bullet_power;
    public float maxShotDelay;//총알 딜레이값
    public float curShotDelay;//
    public float Phase_Curtime;//측정용
    public float Phase_time;// 페이즈 바꾸는 시간

    public GameObject Shot_Pos;

    public Bullet_Manager bulletManager;//총알 매니저

    private void Awake()
    {
        Boss_Phase = (int)Boss_State.Standing;
        Phase_Curtime = 0;
    }

    private void FixedUpdate()
    {
        Reload();
        Phase_Curtime += Time.deltaTime;
        switch (Boss_Phase)
        {
            case (int)Boss_State.Standing:
                Phase_Standing();
                break;
            case (int)Boss_State.Flying:
                break;
        }
    }

    void Phase_Standing()
    {
        if (curShotDelay < maxShotDelay)
            return;
        GameObject bullet = bulletManager.MakeBullet("Boss");//총알 생성
        bullet.transform.position = Shot_Pos.transform.position;//총알 위치 지정

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        if (gameObject.transform.rotation.y == 0)
            rigid.AddForce(Vector2.right * 5f, ForceMode2D.Impulse);//총알 파워+방향주기
        else
            rigid.AddForce(Vector2.left * 5f, ForceMode2D.Impulse);//총알 파워+방향주기
        curShotDelay = 0;
    }



    void Reload()
    {
        curShotDelay += Time.deltaTime;

    }



    void Change_Phase() //보스 패턴 변경
    {
        int rand;
        if (Phase_Curtime < Phase_time)
            return;

        rand = Random.Range(0, 10);
        if(rand >= 6)
        {
            //변경하는 코드 추가
        }
        


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

}
