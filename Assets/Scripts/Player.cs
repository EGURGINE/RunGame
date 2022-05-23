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
    void Update()
    {
        jumpSlider.transform.position = Camera.main.WorldToScreenPoint(
            new Vector3(transform.position.x - 1.5f, transform.position.y, transform.position.z));
        if (Input.GetMouseButton(0))
        {
            cnt += 0.01f;
            jumpSlider.value = cnt / 1;
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
                //벽 타기
            }
            else if (isGround)
            {
                //점프
            }
            else return;
        }
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
