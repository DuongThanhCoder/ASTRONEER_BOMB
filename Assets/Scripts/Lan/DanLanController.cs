using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class DanLanController : NetworkBehaviour
{
    public Tilemap tilemapphaduoc;
    public BoxLan box;
    public LayerMask ExploseMask;
    public Vector2 huongbay;
    Rigidbody2D rigidbody;
    // Start is called before the first frame update
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        tilemapphaduoc = GameObject.Find("blockphaduoc").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 vitrivacham = gameObject.transform.position;
        PlayerLanController player = collision.gameObject.GetComponent<PlayerLanController>();
        if (player != null)
        {
            player.thaydoimau(-1);
            if(IsHost){
                trumauClientRpc(collision.gameObject.name);
            }
            Destroy(gameObject);
            Debug.Log("Ban Trung Nguoi");
            return;
        }
        else
        {
            if (Physics2D.OverlapBox(vitrivacham + huongbay/2f, Vector2.one / 10f, 0f, ExploseMask))
            {
                Debug.Log("Ban Trung box");
                // xoabobox(vitrivacham + huongbay / 2);
                goixoaboxClientRpc(vitrivacham + huongbay / 2);
                Destroy(gameObject);
                return;
            }
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
    public void themluc(Vector2 huong, float luc)
    {
        huongbay = huong;
        rigidbody.AddForce(huong * luc);

    }

    [ClientRpc]
    public void goixoaboxClientRpc(Vector2 vitri)
    {
        xoabobox(vitri);
    }
    [ClientRpc]
    public void trumauClientRpc(string name)
    {
        GameObject.Find(name).GetComponent<PlayerLanController>().thaydoimau(-1);
    }
}
