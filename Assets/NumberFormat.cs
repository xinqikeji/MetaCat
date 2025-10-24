using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberFormat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int n = 15;
        Debug.Log(n.ToString("D3"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
