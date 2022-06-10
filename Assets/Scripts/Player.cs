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
    public bool isStairs;
    [SerializeField] private Slider jumpSlider;
    private float cnt = 0;
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();

    private int isRight = 1;
    private void Update()
    {
        #region 이동
        if (Input.GetMouseButtonDown(0) && isGround&&!isWall)
        {
            this.spd += 5f;
            rb.velocity = new Vector2(spd, rb.velocity.y);
        }

        if (this.spd >= 0)
        {
            this.spd *= 0.99f;//감속
        }
        #endregion

        if (Input.GetMouseButtonUp(0)&&jumpSlider.value>=0.5f)
        {
            if (isGround == false && isWall == false) return;
            Jump(spd+=5 * isRight, 5f);
        }
        #region 점프
        //점프게이지 위치 설정
        jumpSlider.transform.position = Camera.main.WorldToScreenPoint(
            new Vector3(transform.position.x - 1.5f,
            transform.position.y, transform.position.z));

        if (Input.GetMouseButton(0))
        {
            if (isWall)
            {
                rb.AddForce(Vector2.up*0.1f, ForceMode2D.Impulse);
                if (jumpSlider.value == 1)
                {
                    Jump(20f * isRight, 10f);
                }
            }

            cnt += Time.deltaTime;
            jumpSlider.value = cnt / 1f;
        }
        else
        {
            cnt = 0;
            jumpSlider.value = 0;
        }

        if (jumpSlider.value == 1)
        {
            cnt = 0;
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
        switch (collision.collider.tag)
        {
            case "Ground":
                isRight = 1;
                isGround = true;
                break;
            case "Wall":
                isWall = true;
                if (collision.transform.position.x > transform.position.x) isRight = -1;
                else isRight = 1;
                Debug.Log(isRight);
                break;
            case "Stairs":
                isStairs = true;
                break;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Ground":
                isGround = false;

                break;
            case "Wall":
                isWall = false;

                break;
            case "Stairs":
                isStairs = false;

                break;
        }
    }
}
