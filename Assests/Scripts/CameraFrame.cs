using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFrame : MonoBehaviour
{
    [SerializeField]
    private float Max_x;

    [SerializeField]
    private float Max_y;

    [SerializeField]
    private float Min_x;

    [SerializeField]
    private float Min_y;

    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(target.position.x, Min_x, Max_x), Mathf.Clamp(target.position.y, Min_y, Max_y), transform.position.z);
    }
}
