using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Value : MonoBehaviour
{
    public int val = 3;
    public GameObject CubeNumberPrefab;

    void Start()
    {
        Instantiate(CubeNumberPrefab, transform.position, Quaternion.identity, transform);
    }
    
}
