using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objetivo : MonoBehaviour
{
    public GameObject canvas;
    // private Vector3[] positions;
    public GameObject[] referencies;

    // Start is called before the first frame update
    void Start()
    {
        if (canvas != null)
        {
            canvas.SetActive(false);
        }

        int randomNumber = Random.Range(0, 8);
        // Debug.Log(randomNumber);
        transform.position = referencies[randomNumber].transform.position;

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter(Collider other)
    {
        if (canvas != null)
        {
            canvas.SetActive(true);
        }
        

    }

    void OnTriggerExit(Collider other)
    {
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
    }
}
