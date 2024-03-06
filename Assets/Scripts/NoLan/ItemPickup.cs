using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
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
                player.GetComponent<PlayerController>().vukhi = 1;
                player.GetComponent<PlayerController>().capnhapvukhi();
                break;
            case ItemType.Speed:
                player.GetComponent<PlayerMovementController>().speed +=0.25f;
                break;
            case ItemType.MoreBomb:
                player.GetComponent<BombController>().themsoluongbom();
                break;
            case ItemType.Health:
                player.GetComponent<PlayerController>().health.thaydoimau(1);
                break;
            case ItemType.MoreRadius:
                player.GetComponent<BombController>().bankinhno++;
                break;
            case ItemType.Shield:
                player.GetComponent<PlayerController>().batkhien();
                break;
            case ItemType.Gun:
                player.GetComponent<PlayerController>().vukhi = 3;
                player.GetComponent<PlayerController>().gunAttack.soluongdan = 5;
                player.GetComponent<PlayerController>().capnhapvukhi();
                break;
            case ItemType.Hammer:
                player.GetComponent<PlayerController>().vukhi = 2;
                player.GetComponent<PlayerController>().capnhapvukhi();
                break;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnItemPickup(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
