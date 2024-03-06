using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class RobotLanController : NetworkBehaviour
{
    // Start is called before the first frame update
    public float speed = 5;
    float ngang, doc;
    Rigidbody2D rigidbody2d;
    Animator animator;
    Vector2 saisovuno = new Vector2(0, 0.6f);
    public Tilemap tilemapphaduoc;
    public GameObject explosionprefab;
    public LayerMask ExploseMask;
    public BoxLan box;
    PlayerLanController player;
    int bankinhno = 2;
    bool dichuyen;
    public void Awake()
    {
        dichuyen = true;
        if (gameObject.name[5] == 'B')
        {
            player = GameObject.Find("BlueLanPlayer(Clone)").GetComponent<PlayerLanController>();
        }
        if (gameObject.name[5] == 'G')
        {
            player = GameObject.Find("GreenLanPlayer(Clone)").GetComponent<PlayerLanController>();
        }
        if (gameObject.name[5] == 'R')
        {
            player = GameObject.Find("RedLanPlayer(Clone)").GetComponent<PlayerLanController>();
        }
        if (gameObject.name[5] == 'Y')
        {
            player = GameObject.Find("YellowLanPlayer(Clone)").GetComponent<PlayerLanController>();
        }
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
        //if (MapUI.pause == true) { return; }
        Debug.Log("robot di chuyen: " + dichuyen);
        if (dichuyen == false)
        {
            ngang = 0;
            doc = 0;
            return;
        }
        if (IsOwner)
        {
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
                if (IsHost)
                {
                    kichhoatno();
                    goikichhoatnoClientRpc();
                }
                if(IsClient&&!IsHost)
                {
                    kichhoatno();
                    goikichhoatnoServerRpc();
                }
                
            }
            if (ngang != 0 || doc != 0)
            {
                animator.SetBool("Move", true);
            }
            else
            {
                animator.SetBool("Move", false);
            }
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
    public void kichhoatno()
    {
        Vector2 vitrino = rigidbody2d.position + new Vector2(0,0.6f);
        GameObject explosion = Instantiate(explosionprefab, vitrino, Quaternion.identity);
        Explose(vitrino + Vector2.up, Vector2.up, bankinhno);
        Explose(vitrino + Vector2.down, Vector2.down, bankinhno);
        Explose(vitrino + Vector2.left, Vector2.left, bankinhno);
        Explose(vitrino + Vector2.right, Vector2.right, bankinhno);
        Destroy(explosion, 0.5f);
        Destroy(gameObject, 1f);
        dichuyen = false;
        if(player != null)
        {
            player.vukhi = 0;
            player.capnhapvukhi();
            player.vohieuhoamove = false;
            player.gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
        }
        else
        {
            Debug.Log("player null");
        }
    }
    public void Explose(Vector2 vitrino, Vector2 huongno, int length)
    {
        for (int i = length; i > 0; --i)
        {
            if (Physics2D.OverlapBox(new Vector2(vitrino.x, vitrino.y - 0.6f), Vector2.one / 10f, 0f, ExploseMask))
            {
                xoabobox(vitrino - saisovuno);
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
        else
        {
            //Debug.Log("Bat xit o " + vitri);
        }
    }
    [ServerRpc]
    public void goikichhoatnoServerRpc()
    {
        kichhoatno();
    }
    [ClientRpc]
    public void goikichhoatnoClientRpc()
    {
        kichhoatno();
    }
}
