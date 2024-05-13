using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class testmotchut : NetworkBehaviour
{
    public int x;
    // Start is called before the first frame update
    void Start()
    {
        x = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(x);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (NetworkManager.LocalClientId == 0)
            {
                ++x;
            }
            else
            {
                plusvalueServerRpc();
            }
        }
        
    }
    [ServerRpc(RequireOwnership = false)]
    private void plusvalueServerRpc()
    {
        ++x;
    }
}
