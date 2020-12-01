using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player_2P : MonoBehaviour
{
    public Slider HP_Slider;
    public static float HP;
    public float MaxHP;
    public float ShowHP;//HP보는용도
    bool isRight = true;

    public float Speed;//이동속도
    public float JumpPower;//점프파워
    public int JumpCount;//점프카운트
    public LayerMask PlatFormCheck_Layer;//플랫폼 체크 레이어
    public LayerMask Object_Layer;//오브젝트 레이어
    public GameObject TileCheck; //플랫폼 체크 위치
    float Jump_timecheck = 0;//점프 쿨타임용
    public int State;//곰돌이 상태 꿈의 균열  0기본 1 Red 2blue

    Rigidbody2D rigid;//물리
    Animator anim;//애니메이션
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Player_HpSet();
    }
    private void Start()
    {
        State = 0;
    }
    private void FixedUpdate()
    {
        Hp_Bar();
        Move();
        switch(State)
        {
            case 0:
                Physics2D.IgnoreLayerCollision(10, 17, false);//꿈의 균열 Blue 레이어 충돌
                Physics2D.IgnoreLayerCollision(10, 18, false);//꿈의 균열 Red 레이어 충돌
                break;
            case 1:
                Physics2D.IgnoreLayerCollision(10, 18);//꿈의 균열 Red 레이어 무시
                Physics2D.IgnoreLayerCollision(10, 17,false);//꿈의 균열 Blue 레이어 충돌
                //Physics2D.Layer
                break;
            case 2:
                Physics2D.IgnoreLayerCollision(10, 18,false);//꿈의 균열 Red 레이어 충돌
                Physics2D.IgnoreLayerCollision(10, 17);//꿈의 균열 Blue 레이어 무시
                break;
        }
        //Physics2D.IgnoreLayerCollision(10,9);//1P레이어 무시
        //Physics2D.IgnoreLayerCollision(10,11);//꿈의 균열레이어 무시
        //Physics2D.IgnoreLayerCollision(10,8);//타일 레이어 무시
    }
    void Player_HpSet()//초기값 세팅
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "1-1":
            case "2-1":
            case "3-1":
                HP = 3;
                MaxHP = HP;
                break;
            case "Boss":
            case "InGame":
                HP = 10;
                MaxHP = 10;
                break;

        }
    }
        void Hp_Bar()
    {
        float hp = HP / MaxHP;
        ShowHP = HP;
        Camera m_cam = Camera.main;
        HP_Slider.value = hp;
        if (hp == 0)
            HP_Slider.transform.Find("Fill Area").gameObject.SetActive(false);
        else
            HP_Slider.transform.Find("Fill Area").gameObject.SetActive(true);
        Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2f, gameObject.transform.position.z);
        if (isRight)
            HP_Slider.transform.position = m_cam.WorldToScreenPoint(pos + new Vector3(-0.0f, 0, 0));
        else
            HP_Slider.transform.position = m_cam.WorldToScreenPoint(pos + new Vector3(0.0f, 0, 0)); //gameObject.transform.position + new Vector3(0,5,0);
    }

    void Move()
    {
        anim.SetInteger("State", State);
        if (rigid.velocity.y == 0 && rigid.velocity.x == 0)
        {
            anim.SetBool("IsWalk", false);
            anim.SetBool("IsJump", false);
            anim.SetBool("Is2Jump", false);
        }
        Jump_timecheck += Time.deltaTime;
        //이동
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.position += Vector3.left * Speed * Time.deltaTime;
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            anim.SetBool("IsWalk", true);
            isRight = false;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.position += Vector3.right * Speed * Time.deltaTime;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            anim.SetBool("IsWalk", true);
            isRight = true;
        }
        //점프
        if (rigid.velocity.y <= 0)//레이캐스트를 사용하여 타일에 닿았는지 확인
        {
            if (Physics2D.Raycast(TileCheck.transform.position, Vector3.down, 0.15f, PlatFormCheck_Layer)
                ||  Physics2D.Raycast(TileCheck.transform.position, Vector3.down, 0.15f, Object_Layer))
            {
                JumpCount = 2;
                anim.SetBool("Is2Jump", false);
                anim.SetBool("IsJump", false);
            }
        }
        if ((Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.Keypad0)) && JumpCount >= 1 && Jump_timecheck >= 0.3f)
        {
            if(JumpCount == 2)
                anim.SetBool("IsJump", true);
            else
                anim.SetBool("Is2Jump", true);
            SoundManager.instance.SoundPlay("2P_Jump");
            rigid.velocity = Vector2.zero;
            rigid.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            JumpCount--;
            Jump_timecheck = 0;

        }
        Debug.DrawRay(TileCheck.transform.position, Vector2.down * 0.15f, Color.red);//레이케스트 보여주는 코드

    }
    #region 데미지
    void Damaged(Vector2 TargetPos)
    {
        HP--;

        if (HP <= 0)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            int dirc = transform.position.x - TargetPos.x > 0 ? 1 : -1;

            rigid.AddForce(new Vector2(dirc, 1) * 10, ForceMode2D.Impulse);
            Invoke("Die", 0.6f);
        }
        else
        {
            //SoundManager.instance.SoundPlay("Player_Damage");
            gameObject.layer = 14;
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            int dirc = transform.position.x - TargetPos.x > 0 ? 1 : -1;
            if (SceneManager.GetActiveScene().name == "Boss" || SceneManager.GetActiveScene().name == "InGame")
                rigid.AddForce(new Vector2(-1, 1) * 15, ForceMode2D.Impulse);
            else
                rigid.AddForce(new Vector2(dirc, 1) * 10, ForceMode2D.Impulse);
            Invoke("OffDamaged", 1.5f);
        }

    }
    void OffDamaged()//데미지 종료
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
    void Die()//죽음
    {
        gameObject.SetActive(false);
        GameManager.instance.FadeOut();
        Invoke("SceneLoad", 1.3f);
        if (SceneManager.GetActiveScene().name == "Boss")
            HP = 10;
        else
            HP = 3;
        MaxHP = HP;
    }

    void SceneLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #endregion


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            Damaged(collision.transform.position);

        }
        if (collision.gameObject.tag == "Foot_Blue")
            State = 2;
        if (collision.gameObject.tag == "Foot_Red")
            State = 1;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster")
            Damaged(collision.transform.position);
        if (collision.gameObject.tag == "Foot_Blue")
            State = 2;
        if (collision.gameObject.tag == "Foot_Red")
            State = 1;
    }

}
