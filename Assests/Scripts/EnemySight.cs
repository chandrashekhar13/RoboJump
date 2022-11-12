using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    [SerializeField]
    private Enemy enemy;

    void OnTriggertEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            enemy.Target = other.gameObject;
        }
    }

    void OnTriggertExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            enemy.Target = null;
        }
    }
}
