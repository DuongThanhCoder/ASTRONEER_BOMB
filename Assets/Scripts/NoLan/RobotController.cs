using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class RobotController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5;
    float ngang, doc;
    Rigidbody2D rigidbody2d;
    Animator animator;
    public Tilemap tilemapphaduoc;
    public GameObject explosionprefab;
    public LayerMask ExploseMask;
    public Box box;
    public AudioClip tiengno;
    int bankinhno = 2;
    bool dichuyen;
    public enum role
    {
        p1, p2, p3, p4, bot,
    }
    public role roleplayer;
    public void Awake()
    {
        dichuyen = true;
    }
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        tilemapphaduoc = GameObject.Find("blockphaduoc").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MapUI.pause == true) { return; }
        Debug.Log("robot di chuyen: " + dichuyen);
        if (dichuyen == false)
        {
            ngang = 0;
            doc = 0;
            return;
        }
        switch (roleplayer)
        {
            case role.p1:
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
                if (Input.GetKeyDown(KeyCode.F))
                {
                    string ten1 = "BluePlayer(Clone)";
                    kichhoatno(ten1);
                }
                break;
            case role.p2:
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
                if (Input.GetKeyDown(KeyCode.RightControl))
                {
                    string ten2 = "GreenPlayer(Clone)";
                    kichhoatno(ten2);
                }
                break;
            case role.p3:
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
                if (Input.GetKeyDown(KeyCode.K))
                {
                    string ten3 = "RedPlayer(Clone)";
                    kichhoatno(ten3);
                }
                break;
            case role.p4:
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
                if (Input.GetKeyDown(KeyCode.RightShift))
                {
                    string ten4 = "YellowPlayer(Clone)";
                    kichhoatno(ten4);
                }
                break;
        }
        if(ngang!=0||doc!=0)
        {
            animator.SetBool("Move", true);
        }
        else
        {
            animator.SetBool("Move", false);
        }
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
    public void kichhoatno(string ten)
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(tiengno);
        Vector2 vitrino = rigidbody2d.position;
        GameObject explosion = Instantiate(explosionprefab, vitrino, Quaternion.identity);
        Explose(vitrino + Vector2.up, Vector2.up, bankinhno);
        Explose(vitrino + Vector2.down, Vector2.down, bankinhno);
        Explose(vitrino + Vector2.left, Vector2.left, bankinhno);
        Explose(vitrino + Vector2.right, Vector2.right, bankinhno);
        Destroy(explosion, 0.5f);
        Destroy(gameObject, 1f);
        dichuyen = false;
        PlayerController player = GameObject.Find(ten).GetComponent<PlayerController>();
        if(player != null)
        {
            player.vukhi = 0;
            player.capnhapvukhi();
            player.GetComponent<PlayerMovementController>().vohieuhoamove = false;
            player.gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
        }
    }
    public void Explose(Vector2 vitrino, Vector2 huongno, int length)
    {
        for (int i = length; i > 0; --i)
        {
            if (Physics2D.OverlapBox(vitrino, Vector2.one / 10f, 0f, ExploseMask))
            {
                xoabobox(vitrino);
                return;
            }
            GameObject explosion = Instantiate(explosionprefab, vitrino, Quaternion.identity);
            Destroy(explosion, 0.5f);
            vitrino += huongno;
        }
    }
    public void xoabobox(Vector2 vitri)
    {
        Vector2 saisobox = new Vector2(0.5f, 0.69f);
        Vector3Int cell = tilemapphaduoc.WorldToCell(vitri);
        TileBase tile = tilemapphaduoc.GetTile(cell);
        Vector2 vitrihop = tilemapphaduoc.CellToLocal(cell);
        if (tile != null)
        {
            Instantiate(box, vitrihop + saisobox, Quaternion.identity);
            tilemapphaduoc.SetTile(cell, null);
        }
    }
}
