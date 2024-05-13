using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BoxLan : NetworkBehaviour
{
    public float thoigianvo = 1f;
    // Start is called before the first frame update
    [Range(0f, 1f)]
    public float tilerotdo = 0.6f;
    public GameObject[] items;
    Vector3 saisoitem = new Vector3(0, 0.2f, 0);
    void Start()
    {
        Destroy(gameObject,thoigianvo);
    }
    public void OnDestroy()
    {
        if(NetworkManager.IsHost)
        {
            Debug.Log("Box in Host");
            if (items != null && Random.value < tilerotdo)
            {
                int ran = Random.Range(0, items.Length);
                GameObject item = Instantiate(items[ran], transform.position - saisoitem, Quaternion.identity);
                item.GetComponent<NetworkObject>().Spawn();
            }
        }
        
    }
}
