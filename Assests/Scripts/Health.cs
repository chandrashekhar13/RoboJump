using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    float healthSpeed = 300;

    void Update()
    {
        transform.Rotate(0,healthSpeed * Time.deltaTime,0);
    }
}
