using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Box : MonoBehaviour
{
    public float thoigianvo = 1f;
    // Start is called before the first frame update
    [Range(0f, 1f)]
    public float tilerotdo = 0.6f;
    public GameObject[] items;
    Vector3 saisoitem = new Vector3(0, 0.2f,0);
    void Start()
    {
        Destroy(gameObject,thoigianvo);
    }
    private void OnDestroy()
    {
        if (items != null && Random.value < tilerotdo)
        {
            int ran = Random.Range(0, items.Length);
            
            Instantiate(items[ran],transform.position - saisoitem, Quaternion.identity);
        }
    }
}
