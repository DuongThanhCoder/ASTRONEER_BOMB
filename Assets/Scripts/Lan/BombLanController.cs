using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class BombLanController : NetworkBehaviour
{
    // Start is called before the first frame update
    public int soluongbom = 1;
    public float thoigianno = 3;
    public int soluongconlai = 0;
    public int bankinhno = 1;
    public GameObject bombprefab;
    public GameObject explosionprefab;
    public LayerMask ExploseMask;
    Vector2 saisovuno = new Vector2(0, 0.6f);
    public Tilemap tilemapphaduoc;
    public BoxLan box;
    public bool trangthai;
    public float breaktime;
    void Start()
    {
        soluongconlai = soluongbom;
        if (SceneManager.GetActiveScene().name == "MapLanScene")
        {
            tilemapphaduoc = GameObject.Find("blockphaduoc").GetComponent<Tilemap>();
        }
        trangthai = true;
        breaktime = 0;
        //tilemapphaduoc = GameObject.Find("Grid").transform.Find("blockphaduoc").GetComponent<Tilemap>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        if (trangthai == false)
        {
            breaktime -= Time.deltaTime;
            if (breaktime <= 0)
            {
                trangthai = true;
                breaktime = 0;
            }
        }
        if (gameObject.GetComponent<PlayerLanController>().mauhientai == 0)
        {
            return;
        }
        if (soluongconlai > 0 && gameObject.GetComponent<PlayerLanController>().vukhi == 0 && Input.GetKeyDown(KeyCode.F)&&trangthai)
        {
            if (IsHost)
            {
                StartCoroutine(Datbom());
            }
            if (IsClient && !IsHost)
            {
                goidatbomServerRpc();
            }
            //requestDatBomServerRpc();
        }

    }
    private IEnumerator Datbom()
    {
        Vector2 vitridatbom = transform.position;
        vitridatbom.y -= 0.44f;
            GameObject bomb = Instantiate(bombprefab, vitridatbom, Quaternion.identity);
            bomb.GetComponent<NetworkObject>().Spawn();
            soluongconlai--;

            yield return new WaitForSeconds(thoigianno);

            vitridatbom = bomb.transform.position;
            vitridatbom.y += 0.44f;
            callNoClientRpc(vitridatbom, bankinhno);
            GameObject explosion = Instantiate(explosionprefab, vitridatbom, Quaternion.identity);
            //explosion.GetComponent<NetworkObject>().Spawn();
            Debug.Log("Checkpoint1");
            //Explose(vitridatbom + Vector2.up, Vector2.up, bankinhno);
            //Explose(vitridatbom + Vector2.down, Vector2.down, bankinhno);
            //Explose(vitridatbom + Vector2.left, Vector2.left, bankinhno);
            //Explose(vitridatbom + Vector2.right, Vector2.right, bankinhno);
            Destroy(explosion, 0.6f);
            Debug.Log("CheckpointNo");
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
            if (Physics2D.OverlapBox(new Vector2(vitrino.x,vitrino.y-0.6f), Vector2.one / 10f, 0f, ExploseMask))
            {
                xoabobox(vitrino-saisovuno);
                //goixoaboxClientRpc(vitrino - saisovuno);
                return;
            }
            GameObject explosion = Instantiate(explosionprefab, vitrino, Quaternion.identity);
            //explosion.GetComponent<NetworkObject>().Spawn();
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
            //if (IsHost)
            //{
                BoxLan boxobj = Instantiate(box, vitrihop + saisobox, Quaternion.identity);
                //boxobj.GetComponent<NetworkObject>().Spawn();
            //}
            tilemapphaduoc.SetTile(cell, null);
        }
        else
        {
            Debug.Log("Bat xit o " + vitri);
        }
    }
    public void themsoluongbom()
    {
        soluongbom++;
        soluongconlai++;
    }
    public void setbreaktime()
    {
        breaktime = 0.6f;
        trangthai = false;
    }
    [ServerRpc]
    public void goidatbomServerRpc()
    {
        if (soluongconlai > 0)
        {
            StartCoroutine(Datbom());
        }
    }
    [ClientRpc]
    public void goixoaboxClientRpc(Vector2 vitri)
    {
        xoabobox(vitri);
    }

    //[ServerRpc(RequireOwnership = false)]
    //public void requestNoServerRpc()
    //{
    //    StartCoroutine(Datbom());
    //    callNoClientRpc();
    //}
    [ClientRpc]
    public void callNoClientRpc(Vector2 vitridatbom,int bankinhno)
    {
        GameObject explosion = Instantiate(explosionprefab, vitridatbom, Quaternion.identity);
        Explose(vitridatbom + Vector2.up, Vector2.up, bankinhno);
        Explose(vitridatbom + Vector2.down, Vector2.down, bankinhno);
        Explose(vitridatbom + Vector2.left, Vector2.left, bankinhno);
        Explose(vitridatbom + Vector2.right, Vector2.right, bankinhno);
        Destroy(explosion, 0.6f);
    }
    //[ServerRpc(RequireOwnership = false)]
    //public void requestDatBomServerRpc()
    //{
    //    StartCoroutine(Datbom());
    //    callDatBomClientRpc();
    //}
    //[ClientRpc]
    //public void callDatBomClientRpc()
    //{
    //    StartCoroutine(Datbom());
    //}

}
