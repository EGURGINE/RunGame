using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float spd;
    public bool isThoch;
    public bool isGround;
    public bool isWall;
    [SerializeField] private Slider jumpSlider;
    private float cnt = 0;
    private Rigidbody2D rb;

    private int isRight = 1;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        #region 이동
        if (Input.GetMouseButtonDown(0) && isGround&&!isWall)
        {
            this.spd = 0.08f;
        }
        transform.Translate(this.spd, 0, 0);
        this.spd *= 0.99f;//감속
        #endregion


        #region 점프
        //점프게이지 위치 설정
        jumpSlider.transform.position = Camera.main.WorldToScreenPoint(
            new Vector3(transform.position.x - 1.5f,
            transform.position.y, transform.position.z));

        if (Input.GetMouseButton(0))
        {
            if (isWall)
            {
                rb.AddForce(Vector2.up*4*Time.deltaTime, ForceMode2D.Impulse);
            }

            cnt += Time.deltaTime;
            jumpSlider.value = cnt / 0.5f;
        }
        else
        {
            cnt = 0;
            jumpSlider.value = 0;
        }

        if (jumpSlider.value == 1)
        {
            jumpSlider.value = 0;

            if (isWall)
            {
                Debug.Log("jump");
                Jump(5f * isRight, 3f);
            }
            else if (isGround) Jump(5f, 3f);

        }
        #endregion
    }
    private void Jump(float x, float y)
    {
        rb.AddForce(new Vector2(x, y),ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGround = true;
        }
        if (collision.collider.tag == "Wall")
        {
            isWall = true;
            if (collision.transform.position.x>transform.position.x) isRight = -1;
            else isRight = 1;
            Debug.Log(isRight);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGround = false;
        }
        if (collision.collider.tag == "Wall")
        {
            isWall = false;
        }
    }
}
