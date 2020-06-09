using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Value : MonoBehaviour
{
    public int val = 3;
    public GameObject CubeNumberPrefab;

    void Start()
    {
        Vector3 offset = new Vector3(0, 0, 1);
        Instantiate(CubeNumberPrefab, transform.position + offset , Quaternion.identity, transform);
    }
    
}
