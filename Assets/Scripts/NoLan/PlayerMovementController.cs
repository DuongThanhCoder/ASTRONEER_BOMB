using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineFreeLook;

public class PlayerMovementController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 4;
    public float ngang, doc;
    public bool vohieuhoamove;
    Rigidbody2D rigidbody2d;
    HealthController health;
    PlayerController player;
    public Vector2 huongnhin = new Vector2(0, -1);
    private Vector2 vecmove;
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        vohieuhoamove = false;
        health = gameObject.GetComponent<HealthController>();
        player = gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MapUI.pause == true) { return; }
        if (health.mauhientai == 0)
        {
            speed = 0;
            return;
        }
        if (vohieuhoamove == false)
        {
            switch (player.roleplayer)
            {
                case PlayerController.role.p1:
                    if (Input.GetKey(KeyCode.A))
                    {
                        ngang = -0.7f;
                    }
                    else
                    {
                        if (Input.GetKey(KeyCode.D))
                        {
                            ngang = 0.7f;
                        }
                        else
                        {
                            ngang = 0;
                        }
                    }
                    if (Input.GetKey(KeyCode.S))
                    {
                        doc = -0.7f;
                    }
                    else
                    {
                        if (Input.GetKey(KeyCode.W))
                        {
                            doc = 0.7f;
                        }
                        else
                        {
                            doc = 0;
                        }
                    }
                    break;
                case PlayerController.role.p2:
                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        ngang = -0.7f;
                    }
                    else
                    {
                        if (Input.GetKey(KeyCode.RightArrow))
                        {
                            ngang = 0.7f;
                        }
                        else
                        {
                            ngang = 0;
                        }
                    }
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        doc = -0.7f;
                    }
                    else
                    {
                        if (Input.GetKey(KeyCode.UpArrow))
                        {
                            doc = 0.7f;
                        }
                        else
                        {
                            doc = 0;
                        }
                    }
                    break;
                case PlayerController.role.p3:
                    if (Input.GetKey(KeyCode.G))
                    {
                        ngang = -0.7f;
                    }
                    else
                    {
                        if (Input.GetKey(KeyCode.J))
                        {
                            ngang = 0.7f;
                        }
                        else
                        {
                            ngang = 0;
                        }
                    }
                    if (Input.GetKey(KeyCode.H))
                    {
                        doc = -0.7f;
                    }
                    else
                    {
                        if (Input.GetKey(KeyCode.Y))
                        {
                            doc = 0.7f;
                        }
                        else
                        {
                            doc = 0;
                        }
                    }
                    break;
                case PlayerController.role.p4:
                    if (Input.GetKey(KeyCode.L))
                    {
                        ngang = -0.7f;
                    }
                    else
                    {
                        if (Input.GetKey(KeyCode.Quote))
                        {
                            ngang = 0.7f;
                        }
                        else
                        {
                            ngang = 0;
                        }
                    }
                    if (Input.GetKey(KeyCode.Semicolon))
                    {
                        doc = -0.7f;
                    }
                    else
                    {
                        if (Input.GetKey(KeyCode.P))
                        {
                            doc = 0.7f;
                        }
                        else
                        {
                            doc = 0;
                        }
                    }
                    break;
            }
        }
        else
        {
            ngang = 0;
            doc = 0;
        }

        Debug.Log("Ngang = " + ngang + " doc = " + doc);
        vecmove = new Vector2(ngang, doc);
        if (ngang != 0 || doc != 0)
        {
            huongnhin = vecmove;
            huongnhin.Normalize();
            player.setHuongNhin(huongnhin);
        }
        player.setSpeed(vecmove);
    }
    private void FixedUpdate()
    {
        if (ngang != 0 && doc != 0)
        {
            ngang = ngang * 0.707106f;
            doc = doc * 0.707106f;
        }
        Vector2 vitri = rigidbody2d.position;
        vitri.x += speed * ngang * Time.deltaTime;
        vitri.y += speed * doc * Time.deltaTime;
        rigidbody2d.MovePosition(vitri);
    }
}
