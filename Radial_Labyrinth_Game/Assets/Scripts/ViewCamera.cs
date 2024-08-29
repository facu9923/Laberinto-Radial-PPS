using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCamera : MonoBehaviour
{
    public float sensibility = 100f;
    float RotacionX = 0f;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float MauseX = Input.GetAxis("Mouse X") * sensibility * Time.deltaTime;
        float MauseY = Input.GetAxis("Mouse Y") * sensibility * Time.deltaTime;

        RotacionX -= MauseY;
        RotacionX = Mathf.Clamp(RotacionX, -90f, 55f);
        transform.localRotation = Quaternion.Euler(RotacionX, 0f, 0f);
        player.Rotate(Vector3.up*MauseX);
        
    }
}
