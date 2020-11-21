using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player_1P : MonoBehaviour
{
    public static int HP;
    public int ShowHP;//HP보는용도
    public float Speed;//이동속도
    public float JumpPower;//점프파워
    public int JumpCount;//점프카운트
    public LayerMask PlatFormCheck_Layer;//플랫폼 체크 레이어
    public LayerMask Object_Layer;//오브젝트 레이어
    public GameObject TileCheck; //플랫폼 체크 위치
    
    public GameObject Gun_Position;//버블건 위치
    public GameObject Bullet_Prefab;//총알 프리펩
    public float maxShotDelay;//총알 딜레이값
    public float curShotDelay;//
    public Bullet_Manager bulletManager;//총알 매니저
    public float BulletPower;//총알 파워

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

    private void FixedUpdate()
    {
        Move();
        Physics2D.IgnoreLayerCollision(9, 10);//2P레이어 무시
        Physics2D.IgnoreLayerCollision(9, 12);//2P타일 레이어 무시
        Shoot();
        Reload();
        ShowHP = HP;
    }

    void Player_HpSet()//초기값 세팅
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "1-1":
            case "2-1":
            case "3-1":
                HP = 2;
                break;

        }
        if(SceneManager.GetActiveScene().name == "Boss"||SceneManager.GetActiveScene().name == "InGame")
        {
            HP = 10;
            BulletPower = 50;
        }


    }

    #region 버블건 
    void Reload()
    {
        curShotDelay += Time.deltaTime;

    }

    void Shoot()
    {
        if (!Input.GetKey(KeyCode.U) && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.V))
            return;

        if (curShotDelay < maxShotDelay)
            return;

        GameObject bullet = bulletManager.MakeBullet("Player");//총알 생성
        bullet.transform.position = Gun_Position.transform.position;//총알 위치 지정

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        if (gameObject.transform.rotation.y == 0)
            rigid.AddForce(Vector2.right * BulletPower, ForceMode2D.Impulse);//총알 파워+방향주기
        else
            rigid.AddForce(Vector2.left * BulletPower, ForceMode2D.Impulse);//총알 파워+방향주기
        curShotDelay = 0;

    }
    #endregion

    #region 움직임
    void Move()
    {
        
        if((rigid.velocity.y == 0 && rigid.velocity.x == 0))
        {
            anim.SetBool("IsWalk", false);
            anim.SetBool("IsJump", false);
        }

        //if (rigid.velocity.y != 0)
        //    anim.SetBool("IsJump", true);
        //이동
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.position += Vector3.left * Speed * Time.deltaTime;
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            anim.SetBool("IsWalk", true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.position += Vector3.right * Speed * Time.deltaTime;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            anim.SetBool("IsWalk", true);
        }
        if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            anim.SetBool("IsWalk", false);

        //점프
        if (rigid.velocity.y <= 0)//레이캐스트를 사용하여 타일에 닿았는지 확인
        {
            if (Physics2D.Raycast(TileCheck.transform.position, Vector3.down, 0.15f, PlatFormCheck_Layer)
                || Physics2D.Raycast(TileCheck.transform.position, Vector3.down, 0.15f, Object_Layer))
            {
                JumpCount = 1;
                anim.SetBool("IsJump", false);
            }

           
        }
            //isJump = Physics2D.Raycast(TileCheck.transform.position, Vector3.down, 0.15f, PlatFormCheck_Layer);
        if (Input.GetKey(KeyCode.Space) && JumpCount >= 1)
        {
            rigid.velocity = Vector2.zero;
            rigid.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            JumpCount--;
            anim.SetBool("IsJump", true);
        }

        Debug.DrawRay(TileCheck.transform.position, Vector2.down * 0.15f, Color.red);//레이케스트 보여주는 코드

    }
    #endregion

    #region 데미지
    void Damaged(Vector2 TargetPos)
    {
        HP--;
        if(HP == -1)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            int dirc = transform.position.x - TargetPos.x > 0 ? 1 : -1;
            
            rigid.AddForce(new Vector2(dirc, 1) * 10, ForceMode2D.Impulse);
            Invoke("Die", 0.6f);
        }
        else
        {
            gameObject.layer = 14;
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            int dirc = transform.position.x - TargetPos.x > 0 ? 1 : -1;
            if (SceneManager.GetActiveScene().name == "Boss" || SceneManager.GetActiveScene().name == "InGame")
                rigid.AddForce(new Vector2(-1, 1) * 20, ForceMode2D.Impulse);
            else
                rigid.AddForce(new Vector2(dirc, 1) * 10, ForceMode2D.Impulse);
            Invoke("OffDamaged", 1.5f);
        }
      
    }
    void OffDamaged()//데미지 종료
    {
        gameObject.layer = 9;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
    void Die()//죽음
    {
        gameObject.SetActive(false);
        GameManager.instance.FadeOut();
        Invoke("SceneLoad", 1.3f);
        HP = 2;
    }

    void SceneLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #endregion


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Monster")
        {
            Damaged(collision.transform.position);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            Damaged(collision.transform.position);

        }
    }
}
