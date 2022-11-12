using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    // Start is called before the first frame update
    float coinSpeed = 100;

    void Update()
    {
        transform.Rotate(0, coinSpeed * Time.deltaTime, 0);
    }
}
