using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ExplosionLan : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("notrung");
            //collision.gameObject.GetComponent<PlayerLanController>().thaydoimau(-2);
            if (collision.gameObject.GetComponent<NetworkObject>().IsOwner)
            {
                collision.gameObject.GetComponent<PlayerLanController>().requestChangeHealthServerRpc(-2);
            }

        }
    }
}
