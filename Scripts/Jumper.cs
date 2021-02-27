using System.Collections;
using System;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    public GameObject arrow, _camera;
    public float arrowRotSpeed, jumpForce;
    private Rigidbody2D rb;
    private bool jump = false, isGrounded = false, pointing = false;
    private Vector2 startPoint, bias;

    Vector2 Jump()
    {
        Vector2 v = new Vector2(0, 0);

        if(Input.GetMouseButtonDown(0))
        {
           pointing = true;
           startPoint = (Vector2)Input.mousePosition;
           startPoint -= new Vector2(Screen.width / 2f, Screen.height / 2f);
        }

        if(pointing)
        {
            bias = startPoint - (Vector2)Input.mousePosition;
            arrow.SetActive(true);
            arrow.transform.LookAt(new Vector3(0f, 0f, 0f));
        }

        if(!pointing)
        {
            arrow.SetActive(false);
            startPoint = Vector2.zero;
        }

        if(Input.GetMouseButtonUp(0))
        {
            pointing = false;
            v = bias / Screen.height * jumpForce;
        }

        return v;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        arrow.transform.position = transform.position + Vector3.forward;
        _camera.transform.position = transform.position - Vector3.forward;
        arrow.SetActive(!jump);

        rb.AddForce(Jump());
    }

    void OnCollisionEnter2D()
    {
        isGrounded = true;
        jump = false;
    }

    void OnCollisionExit2D()
    {
        isGrounded = false;
    }
}
