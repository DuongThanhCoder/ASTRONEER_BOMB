using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Spawner : NetworkBehaviour
{
    public GameObject spawnOject;
    public int timespawn = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawn());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator spawn()
    {
        yield return new WaitForSeconds(timespawn);
        if (NetworkManager.IsHost)
        {
            GameObject a = Instantiate(spawnOject);
            a.GetComponent<NetworkObject>().Spawn();
            Debug.Log("Spawn constructer Game Lan");
        }
    }
}
