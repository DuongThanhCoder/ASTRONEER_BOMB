using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ItemPickupLan : NetworkBehaviour
{
    // Start is called before the first frame update
    public enum ItemType
    {
        Robot,Speed,MoreBomb,Health,MoreRadius,Shield,Gun,Hammer,
    }
    public ItemType Type;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnItemPickup(GameObject player)
    {
        switch (Type)
        {
            case ItemType.Robot:
                player.GetComponent<PlayerLanController>().vukhi = 1;
                player.GetComponent<PlayerLanController>().capnhapvukhi();
                break;
            case ItemType.Speed:
                player.GetComponent<PlayerLanController>().speed+=0.25f;
                break;
            case ItemType.MoreBomb:
                player.GetComponent<BombLanController>().themsoluongbom();
                break;
            case ItemType.Health:
                player.GetComponent<PlayerLanController>().thaydoimau(1);
                break;
            case ItemType.MoreRadius:
                player.GetComponent<BombLanController>().bankinhno++;
                break;
            case ItemType.Shield:
                player.GetComponent<PlayerLanController>().batkhien();
                break;
            case ItemType.Gun:
                player.GetComponent<PlayerLanController>().vukhi = 3;
                player.GetComponent<PlayerLanController>().soluongdan = 5;
                player.GetComponent<PlayerLanController>().capnhapvukhi();
                break;
            case ItemType.Hammer:
                player.GetComponent<PlayerLanController>().vukhi = 2;
                player.GetComponent<PlayerLanController>().capnhapvukhi();
                break;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(IsHost){
                goinhatitemClientRpc(collision.gameObject.name);
            }
            OnItemPickup(collision.gameObject);
            Destroy(gameObject);
        }
    }
    [ClientRpc]
    public void goinhatitemClientRpc(string name){
        if(!IsHost){
            OnItemPickup(GameObject.Find(name).gameObject);
        } 
    }
}
