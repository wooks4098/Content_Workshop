using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    enum Boss_State {None,Standing, Flying}; 
    public int Boss_Phase; //포스 페이즈(공격 패턴)
    public int HP; 
    public int Bullet_power; //총알 파워
    public float maxShotDelay;//총알 딜레이값
    public float curShotDelay;//총알 딜레이 측정용
    public float Phase_Curtime;//페이즈 변경 측정용
    public float Phase_time;// 페이즈 바꾸는 시간

    private Transform Standing_Pos;//패턴 1 위치

    public GameObject Player_Pos;//플레이어 위치
    public Bullet_Manager bulletManager;//총알 매니저


    SpriteRenderer spriteRenderer;



    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        Boss_Phase = (int)Boss_State.Standing;
        Phase_Curtime = 0;
        Standing_Pos = gameObject.transform;
    }

    private void FixedUpdate()
    {
        Reload();
        Phase_Curtime += Time.deltaTime;
        switch (Boss_Phase)
        {
            case (int)Boss_State.Standing:
                Phase_1_Standing();
                break;
            case (int)Boss_State.Flying:
                Phase_2_Fling();
                break;
        }
    }

    void Phase_1_Standing() // 패턴 1
    {
        if (curShotDelay < maxShotDelay)
            return;
        int pos = -6;
        for (int i = 0; i < 3; i++)
        {
            GameObject bullet = bulletManager.MakeBullet("Boss");
            bullet.transform.position = transform.position + Vector3.up * 1f;//위치 설정

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = Player_Pos.transform.position - transform.position + Vector3.up * 1f;
            Vector2 ranVec = new Vector2(0, pos);
            pos += 6;
            dirVec += ranVec;
            rigid.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);

        }
        curShotDelay = 0;
    }

    void Phase_2_Fling()// 패턴 2
    {
        if (curShotDelay < maxShotDelay)
            return;
        int BulletCount = 20;
        for(int i = 0; i< BulletCount; i++)
        {
            GameObject bullet = bulletManager.MakeBullet("Boss");//총알 생성
            bullet.transform.position = transform.position + Vector3.up * 1f;//위치 설정
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();//총알 물리 생성

            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / BulletCount),
                                         Mathf.Sin(Mathf.PI * 2 * i / BulletCount));

            rigid.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);
        }

        curShotDelay = 0;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;

    }

    void Moveto_phase2()//1 -> 2 패턴으로 이동
    {

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

    void Damage()
    {
        HP--;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        Invoke("DamageOut", 0.05f);

    }
    void DamageOut()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Damage();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

}
