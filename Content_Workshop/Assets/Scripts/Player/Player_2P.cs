using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_2P : MonoBehaviour
{
    public float Speed;//이동속도
    public float JumpPower;//점프파워
    public int JumpCount;//점프카운트
    public LayerMask PlatFormCheck_Layer;//플랫폼 체크 레이어
    public LayerMask Object_Layer;//오브젝트 레이어
    public GameObject TileCheck; //플랫폼 체크 위치
    float Jump_timecheck = 0;//점프 쿨타임용


    Rigidbody2D rigid;//물리
    Animator anim;//애니메이션


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    private void FixedUpdate()
    {
        Move();
        //Physics2D.IgnoreLayerCollision(10,9);//1P레이어 무시
        //Physics2D.IgnoreLayerCollision(10,11);//꿈의 균열레이어 무시
        //Physics2D.IgnoreLayerCollision(10,8);//타일 레이어 무시
    }

    void Move()
    {
        if (rigid.velocity.y == 0 && rigid.velocity.x == 0)
        {
            anim.SetBool("IsWalk", false);
            anim.SetBool("IsJump", false);
            anim.SetBool("Is2Jump", false);
        }
        Jump_timecheck += Time.deltaTime;
        //이동
        if (Input.GetKey(KeyCode.J))
        {
            gameObject.transform.position += Vector3.left * Speed * Time.deltaTime;
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            anim.SetBool("IsWalk", true);
        }
        if (Input.GetKey(KeyCode.L))
        {
            gameObject.transform.position += Vector3.right * Speed * Time.deltaTime;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            anim.SetBool("IsWalk", true);
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
        if (Input.GetKey(KeyCode.RightShift) && JumpCount >= 1 && Jump_timecheck >= 0.3f)
        {
            if(JumpCount == 2)
                anim.SetBool("IsJump", true);
            else
                anim.SetBool("Is2Jump", true);
            SoundManager.instance.SoundPlay("Jump");
            rigid.velocity = Vector2.zero;
            rigid.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            JumpCount--;
            Jump_timecheck = 0;

        }
        Debug.DrawRay(TileCheck.transform.position, Vector2.down * 0.15f, Color.red);//레이케스트 보여주는 코드





    }

}
