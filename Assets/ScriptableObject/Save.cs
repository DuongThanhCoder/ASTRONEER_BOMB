using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Save")]
public class Save : ScriptableObject
{
    // Start is called before the first frame update
    public List<int> teamWithId = new List<int> { -1,-1,-1,-1};
    public List<string> teamWithName = new List<string> { "", "", "","" };
}
