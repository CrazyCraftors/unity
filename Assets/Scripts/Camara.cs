using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public Transform Cube;
    public Vector3 offset;

    void Update()
    {
        if(Cube != null){ 
            transform.position = Cube.position+offset;
        }
    }
}
