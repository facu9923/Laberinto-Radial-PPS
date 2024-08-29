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
        Debug.Log(randomNumber);
        transform.position = referencies[randomNumber].transform.position;
        // positions[0] = new Vector3(36.651f, 7.42f, 10.03f);
        
        // if (randomNumber < 0.125){
        //     Vector3 newPosition = new Vector3(36.651f, 7.42f, 10.03f);
        //     transform.position = newPosition;
        // }else if (randomNumber < 0.250)
        // {
        //     Vector3 newPosition = new Vector3(49.09f, 7.42f, 37.63f);
        //     transform.position = newPosition;
        // }
        // else if (randomNumber < 0.375)
        // {
        //     Vector3 newPosition = new Vector3(6f, 7.04f, -2f);
        //     transform.position = newPosition;
        // }
        // else if (randomNumber < 0.5)
        // {
        //     Vector3 newPosition = new Vector3(39f, 7.42f, 64.3f);
        //     transform.position = newPosition;
        // }

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("COLISION ahr");
        if (canvas != null)
        {
            canvas.SetActive(true);
        }
        

    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("SALIENDO ahr");
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
    }
}
