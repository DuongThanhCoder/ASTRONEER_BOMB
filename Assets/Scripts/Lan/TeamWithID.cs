using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamWithID : MonoBehaviour
{
    // Start is called before the first frame update
    public static TeamWithID Instance;
    public List<int> teamWithId;
    public int x;
    void Awake()
    {
        teamWithId = new List<int> { -1,-1,-1,-1};
        x = -1;
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
