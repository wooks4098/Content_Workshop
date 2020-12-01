using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Monster : MonoBehaviour
{
    public int HP;
    public float Speed;
    bool Die = false;
    public int NextMove;
    public LayerMask PlatFormCheck_Layer;//플랫폼 체크 레이어
    public LayerMask Object_Layer;//오브젝트 레이어
    public GameObject TileCheck; //플랫폼 체크 위치

    Animator anim;//애니메이션
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        MoveCheck();

        //NextMove = -1;
    }
    private void FixedUpdate()
    {
        //이동체크
        if (Die)
            return;
       if(NextMove == -1)
            gameObject.transform.position += Vector3.left * Speed * Time.deltaTime;
       else if(NextMove == 1)
            gameObject.transform.position += Vector3.right * Speed * Time.deltaTime;
        Vector2 vec = new Vector2(TileCheck.transform.position.x + NextMove, TileCheck.transform.position.y);
        RaycastHit2D rayHit_PlatForm = Physics2D.Raycast(vec, Vector3.down, 0.3f, PlatFormCheck_Layer);//타일 체크
        Debug.DrawRay(vec, Vector2.down * 0.3f, Color.red);//레이케스트 보여주는 코드
        if (rayHit_PlatForm.collider == null)//바닥이 있는지 체크
        {
            if(NextMove == -1)
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            else if(NextMove == 1)
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            NextMove *= -1;
            CancelInvoke();
            Invoke("MoveCheck", 3);
        }
        //Ani();
    }
    void Ani()
    {
        if (rigid.velocity.x == 0)
            anim.SetBool("IsWalk", false);
        if(rigid.velocity.x != 0)
            anim.SetBool("IsWalk", true);
    }
    void MoveCheck()//몬스터 방향체크
    {
        NextMove = Random.Range(-1, 2);
        if(NextMove == -1)
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        else if(NextMove == 1)
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        if(NextMove == 0)
            anim.SetBool("IsWalk", false);
        else
            anim.SetBool("IsWalk", true);
        Invoke("MoveCheck", 3);
    }
    void Damage()
    {

        SoundManager.instance.SoundPlay("Boss_attack");
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        if (HP <= 0)
        {
            anim.Play("Monster_DIe");
            anim.speed = 0f;
            Die = true;
            rigid.AddForce(Vector2.up*8f, ForceMode2D.Impulse);
            gameObject.layer = 16;

        }
        Invoke("Damagae_Off", 0.3f);
        
    }
    void Damagae_Off()
    {
        
        if(HP <= 0)
        {
            rigid.gravityScale = 5;
            gameObject.SetActive(false);
        }
        else
            spriteRenderer.color = new Color(1, 1, 1, 1f);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Object")
        {
            if (NextMove == -1)
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            else if (NextMove == 1)
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            NextMove *= -1;
            CancelInvoke();
            Invoke("MoveCheck", 3);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            HP--;
            Damage();
        }
    }

}
