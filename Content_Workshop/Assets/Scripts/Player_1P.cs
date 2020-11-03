using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_1P : MonoBehaviour
{

    public float Speed;//이동속도
    public float JumpPower;//점프파워
    public int JumpCount;//점프카운트
    public LayerMask PlatFormCheck_Layer;//플랫폼 체크 레이어
    public GameObject TileCheck; //플랫폼 체크 위치

    public GameObject Gun_Position;//버블건 위치
    public GameObject Bullet_Prefab;//총알 프리펩
    public float maxShotDelay;//총알 딜레이값
    public float curShotDelay;//
    public Bullet_Manager bulletManager;//총알 매니저
    public float BulletPower;//총알 파워

    Rigidbody2D rigid;//물리

   


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
        Physics2D.IgnoreLayerCollision(9, 10);//2P레이어 무시
        Physics2D.IgnoreLayerCollision(9, 12);//2P타일 레이어 무시
        Shoot();
        Reload();
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;

    }

    void Shoot()
    {
        if (!Input.GetKey(KeyCode.U))
            return;

        if (curShotDelay < maxShotDelay)
            return;

        GameObject bullet = bulletManager.MakeBullet();//총알 생성
        bullet.transform.position = Gun_Position.transform.position;//총알 위치 지정

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        if (gameObject.transform.rotation.y == 0)
            rigid.AddForce(Vector2.right * BulletPower, ForceMode2D.Impulse);//총알 파워+방향주기
        else
            rigid.AddForce(Vector2.left * BulletPower, ForceMode2D.Impulse);//총알 파워+방향주기
        curShotDelay = 0;

    }

    void Move()
    {
        //이동
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.position += Vector3.left * Speed * Time.deltaTime;
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.position += Vector3.right * Speed * Time.deltaTime;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        //점프
        if (rigid.velocity.y <= 0)//레이캐스트를 사용하여 타일에 닿았는지 확인
        {
            if(Physics2D.Raycast(TileCheck.transform.position, Vector3.down, 0.15f, PlatFormCheck_Layer))
                JumpCount = 1;
        }
            //isJump = Physics2D.Raycast(TileCheck.transform.position, Vector3.down, 0.15f, PlatFormCheck_Layer);
        if (Input.GetKey(KeyCode.Space) && JumpCount >= 1)
        {
            rigid.velocity = Vector2.zero;
            rigid.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            JumpCount--;
        }
        Debug.DrawRay(TileCheck.transform.position, Vector2.down * 0.15f, Color.red);//레이케스트 보여주는 코드

    }



}
