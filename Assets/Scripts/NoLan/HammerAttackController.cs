using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class HammerAttackController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigidbody2d;
    Tilemap tilemapphaduoc;
    public GameObject explosionprefab;
    public LayerMask ExploseMask;
    public Box box;
    public AudioClip tiengbua;
    public AudioClip tiengno;
    Vector2 huong;
    PlayerController player;
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        player = gameObject.GetComponent<PlayerController>();

        //if (SceneManager.GetActiveScene().name == "MapScene")
        //{
        //    tilemapphaduoc = GameObject.Find("blockphaduoc").GetComponent<Tilemap>();
        //}
        tilemapphaduoc = GameObject.Find("blockphaduoc").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void kichhoatbua()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(tiengbua);
        huong = gameObject.GetComponent<PlayerMovementController>().huongnhin;
        gameObject.GetComponent<PlayerMovementController>().vohieuhoamove = true;
        StartCoroutine(Explose(transform.position, huong));
        StartCoroutine(player.bovohieuhoatime(1.7f));
    }
    public IEnumerator Explose(Vector2 vitrino, Vector2 huongno)
    {
        vitrino.y -= 0.44f;
        yield return new WaitForSeconds(0.5f);
        for (int i = 1; i <= 5; ++i)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(tiengno);
            GameObject explosion = Instantiate(explosionprefab, vitrino  + huongno * i, Quaternion.identity);
            Destroy(explosion, 0.5f);
            if (Physics2D.OverlapBox(vitrino + huongno * i, Vector2.one / 10f, 0f, ExploseMask))
            {
                xoabobox(vitrino + huongno * i);
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
}
