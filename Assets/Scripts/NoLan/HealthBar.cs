using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    //public static HealthBar health { get; private set; }
    public Image mask;
    public float barwidth;
    public float giatri;
    private void Awake()
    {
        barwidth = mask.rectTransform.rect.width;
    }
    void Start()
    {
        //health = this;
        //barwidth = mask.rectTransform.rect.width;
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void setValue(float value)
    {
        //barwidth = 0.97f;
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, value * barwidth);
        Debug.Log("thaydoi" + value+" chieudai: "+ barwidth);
        giatri = value;  
    }
}
