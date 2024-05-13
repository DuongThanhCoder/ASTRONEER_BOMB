using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class HammerAttackLanController : NetworkBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigidbody2d;
    Tilemap tilemapphaduoc;
    public GameObject explosionprefab;
    public LayerMask ExploseMask;
    public BoxLan box;
    Vector2 huong;
    PlayerLanController player;
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        player = gameObject.GetComponent<PlayerLanController>();

        if (SceneManager.GetActiveScene().name == "MapLanScene")
        {
            tilemapphaduoc = GameObject.Find("blockphaduoc").GetComponent<Tilemap>();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void kichhoatbua()
    {
        huong = gameObject.GetComponent<PlayerLanController>().huongnhin;
        player.vohieuhoamove = true;
        StartCoroutine(Explose(transform.position, huong));
        StartCoroutine(player.bovohieuhoatime(1.7f));
    }
    public IEnumerator Explose(Vector2 vitrino, Vector2 huongno)
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 1; i <= 5; ++i)
        {
            GameObject explosion = Instantiate(explosionprefab, vitrino  + huongno * i, Quaternion.identity);
            Destroy(explosion, 0.5f);
            if (Physics2D.OverlapBox(vitrino - new Vector2(0,0.6f) + huongno * i, Vector2.one / 10f, 0f, ExploseMask))
            {
                 xoabobox(vitrino - new Vector2(0, 0.6f) + huongno * i);
                //xoabobox(vitrino - saisovuno);
                //goixoaboxClientRpc(vitrino - new Vector2(0, 0.6f) + huongno * i);
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
    [ClientRpc]
    public void goixoaboxClientRpc(Vector2 vitri)
    {
        xoabobox(vitri);
    }
    [ServerRpc(RequireOwnership = false)]
    public void requestHammerAttackServerRpc()
    {
        kichhoatbua();
        callkichhoatbuaClientRpc();
    }
    [ClientRpc]
    public void callkichhoatbuaClientRpc()
    {
        kichhoatbua();
    }
}
