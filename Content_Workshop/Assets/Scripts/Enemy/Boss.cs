using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boss : MonoBehaviour
{
    enum Boss_State {None,Standing, Flying};
    public float MaxHp;
    public float HP;
    public Slider Hp_Bar;
    public GameObject Hp_Slider;
    public int Bullet_power; //총알 파워
    public float maxShotDelay;//총알 딜레이값
    public float curShotDelay;//총알 딜레이 측정용

    public float Phase_Curtime;//페이즈 변경 측정용
    public float Phase_time;// 페이즈 바꾸는 시간
    public int Boss_Phase; //포스 페이즈(공격 패턴)

    public float MoveTimer;
    public bool MoveCheck = false;//움직임 체크 (움직였으면 true)
    public bool isLeft = true;

    public GameObject Player_Pos;//플레이어 위치
    public Bullet_Manager bulletManager;//총알 매니저


    public Transform Flying_Pos;//패턴 2 위치
    public Transform Standing_Pos;//패턴 1 위치

    public Sprite[] sp;

    SpriteRenderer spriteRenderer;
    Rigidbody2D Boss_rigid;//물리
    Animator anim;//애니메이션


    private void Awake()
    {
        MaxHp = HP;
        spriteRenderer = GetComponent<SpriteRenderer>();
        Boss_rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Boss_Phase = (int)Boss_State.None;
        Phase_Curtime = 0;
    }

    private void FixedUpdate()
    {


        Hp_Bar.value = HP / MaxHp;
        if (HP == 0)
        {
            Hp_Slider.SetActive(false);
            //죽는 모션

            return;
        }




        Reload();
        Change_Phase();
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

        Moveto_phaseto1();//이동
        anim.Play("Boss_2");

        if (!MoveCheck)
            return;
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
            SpriteRenderer render = bullet.GetComponent<SpriteRenderer>();
            //render.color = new Color(1, 1, 1, 1);
            render.sprite = sp[0];

        }
        curShotDelay = 0;
    }

    void Phase_2_Fling()// 패턴 2
    {
        Moveto_phaseto2();//이동
        anim.Play("Boss_3");

        if (!MoveCheck)
            return;
        if(isLeft)
            gameObject.transform.position += Vector3.left * 3 * Time.deltaTime;
        else
            gameObject.transform.position += Vector3.right * 3 * Time.deltaTime;



        if (curShotDelay < maxShotDelay)
            return;

        int BulletCount = 20;
        for(int i = 0; i< BulletCount; i++)
        {
            GameObject bullet = bulletManager.MakeBullet("Boss");//총알 생성
            bullet.transform.position = transform.position + Vector3.up * 1f + Vector3.left * 0.7f;//위치 설정
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();//총알 물리 생성
            SpriteRenderer render = bullet.GetComponent<SpriteRenderer>();
            if (i %2 == 0)
                //render.color = new Color(1, 0, 0, 1);
            render.sprite = sp[0];
            else
                //render.color = new Color(1, 1, 1, 1);
                render.sprite = sp[1];
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / BulletCount),
                                         Mathf.Sin(Mathf.PI * 2 * i / BulletCount));

            rigid.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);
        }

        curShotDelay = 0;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime; // 발사
        Phase_Curtime += Time.deltaTime;// 패턴 변경
        MoveTimer += Time.deltaTime;
    }

    void Moveto_phaseto1()     //2 -> 1 패턴으로 이동
    {
        Vector2 target = gameObject.transform.position + Vector3.up * 20f;
        Vector2 velo = Vector2.zero;
        transform.position =
            Vector2.Lerp(transform.position, Standing_Pos.position, 0.025f);
        if (MoveTimer > 2.2)
            MoveCheck = true;
    }
    void Moveto_phaseto2()    //1 -> 2 패턴으로 이동
    {
        if (MoveCheck)
            return;
        Vector2 target = gameObject.transform.position + Vector3.up * 20f;
        Vector2 velo = Vector2.zero;
        transform.position =
            Vector2.Lerp(transform.position, Flying_Pos.position, 0.05f);
        if (MoveTimer > 1.7)
            MoveCheck = true;

    }

    void Change_Phase() //보스 패턴 변경
    {
        if (Phase_Curtime < Phase_time)
            return;



        switch (Boss_Phase)
        {
            case (int)Boss_State.None:
                Boss_Phase = (int)Boss_State.Standing; // 페이즈 1로 변경
                Phase_time = 10;
                break;
            case (int)Boss_State.Standing:
                Boss_Phase = (int)Boss_State.Flying; // 페이즈 2로 변경
                Phase_time = 7;
                isLeft = true;
                break;
            case (int)Boss_State.Flying:
                Boss_Phase = (int)Boss_State.Standing; // 페이즈 1로 변경
                Phase_time = 10;
                break;
        }
        MoveCheck = false;
        MoveTimer = 0;
        Phase_Curtime = 0;

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
        if (collision.gameObject.tag == "Wall")
        {
            if (isLeft)
                isLeft = false;
            else
                isLeft = true;
        }
    }


}
