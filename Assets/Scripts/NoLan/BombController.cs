using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
    // Start is called before the first frame update
    public int soluongbom = 1;
    public float thoigianno = 3;
    public int soluongconlai = 0;
    public int bankinhno = 1;
    public GameObject bombprefab;
    public GameObject explosionprefab;
    public LayerMask ExploseMask;
    //Vector2 saisovuno = new Vector2(0, 0.6f);
    public Tilemap tilemapphaduoc;
    public Box box;
    public bool trangthai;
    public float breaktime;
    public AudioClip tiengbom;
    public enum role
    {
        p1, p2, p3, p4, bot,
    }
    public role roleplayer;
    void Start()
    {
        soluongconlai = soluongbom;
        //if (SceneManager.GetActiveScene().name == "MapScene")
        //{
        //    tilemapphaduoc = GameObject.Find("blockphaduoc").GetComponent<Tilemap>();
        //}
        tilemapphaduoc = GameObject.Find("blockphaduoc").GetComponent<Tilemap>();
        trangthai = true;
        breaktime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (MapUI.pause == true) { return; }
        if (trangthai == false)
        {
            breaktime -= Time.deltaTime;
            if (breaktime <= 0)
            {
                trangthai = true;
                breaktime = 0;
            }
        }
        if (gameObject.GetComponent<HealthController>().mauhientai == 0)
        {
            return;
        }
        if (soluongconlai > 0 && gameObject.GetComponent<PlayerController>().vukhi == 0 && trangthai == true)
        {
            switch (roleplayer)
            {
                case role.p1:
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        StartCoroutine(Datbom());
                    }
                    break;
                case role.p2:
                    if (Input.GetKeyDown(KeyCode.RightControl))
                    {
                        StartCoroutine(Datbom());
                    }
                    break;
                case role.p3:
                    if (Input.GetKeyDown(KeyCode.K))
                    {
                        StartCoroutine(Datbom());
                    }
                    break;
                case role.p4:
                    if (Input.GetKeyDown(KeyCode.RightShift))
                    {
                        StartCoroutine(Datbom());
                    }
                    break;
            }
            
        }

    }
    private IEnumerator Datbom()
    {
        Vector2 vitridatbom = transform.position;
        vitridatbom.y -= 0.44f;
        GameObject bomb = Instantiate(bombprefab,vitridatbom,Quaternion.identity);
        soluongconlai--;

        yield return new WaitForSeconds(thoigianno);

        vitridatbom = bomb.transform.position;
        //vitridatbom.y += 0.44f;
        GameObject explosion = Instantiate(explosionprefab,vitridatbom,Quaternion.identity);
        gameObject.GetComponent<AudioSource>().PlayOneShot(tiengbom);
        Explose(vitridatbom + Vector2.up, Vector2.up, bankinhno);
        Explose(vitridatbom + Vector2.down, Vector2.down, bankinhno);
        Explose(vitridatbom + Vector2.left, Vector2.left, bankinhno);
        Explose(vitridatbom + Vector2.right, Vector2.right, bankinhno);

        Destroy(explosion, 0.6f);
        
        Destroy(bomb);
        soluongconlai++;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.isTrigger = false;
        
    }
    public void Explose(Vector2 vitrino, Vector2 huongno, int length)
    {
        for(int i = length; i > 0; --i)
        {
            if (Physics2D.OverlapBox(vitrino, Vector2.one / 10f, 0f, ExploseMask))
            {
                xoabobox(vitrino);
                return;
            }
            GameObject explosion = Instantiate(explosionprefab, vitrino, Quaternion.identity);
            Destroy(explosion, 0.6f);
            vitrino += huongno;
        }
    }
    public void xoabobox(Vector2 vitri)
    {
        Vector2 saisobox = new Vector2(0.5f, 0.69f);
        Vector3Int cell = tilemapphaduoc.WorldToCell(vitri);
        TileBase tile = tilemapphaduoc.GetTile(cell);
        Vector2 vitrihop = tilemapphaduoc.CellToLocal(cell);

        if(tile != null)
        {
            
            Instantiate(box,vitrihop+saisobox,Quaternion.identity);
            tilemapphaduoc.SetTile(cell, null);
            
        }
    }
    public void themsoluongbom()
    {
        soluongbom++;
        soluongconlai++;
    }
    public void setbreaktime()
    {
        breaktime = 0.5f;
        trangthai = false;
    }
}
