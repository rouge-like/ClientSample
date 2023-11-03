using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.Protocol;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject _parentGo;
    public float Speed;

    private void Start()
    {
        Speed = -40;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, Speed * Time.deltaTime, 0));
        transform.position = _parentGo.transform.position; 
    }
}
