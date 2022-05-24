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
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.spd = 0.08f;
        }
        transform.Translate(this.spd/2, 0, 0);
        this.spd *= 0.98f;//감속
        #region 점프
        jumpSlider.transform.position = Camera.main.WorldToScreenPoint(//점프게이지 위치 설정
            new Vector3(transform.position.x - 1.5f, 
            transform.position.y, transform.position.z));
        if (Input.GetMouseButton(0)&&isGround)
        {
            cnt += 0.01f;
            jumpSlider.value = cnt / 2;
        }
        else 
        {
            cnt = 0;
            jumpSlider.value = 0;
        }

        if (jumpSlider.value == 1 && isGround)
        {
            jumpSlider.value = 0;
            Jump(30f, 20f);
        }
        #endregion
    }
    private void Jump(float x, float y)
    {
        rb.AddForce(new Vector2(x, y));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag=="Ground")
        {
            isGround = true;
        }
        if (collision.collider.tag == "Wall")
        {
            isWall = true;
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
