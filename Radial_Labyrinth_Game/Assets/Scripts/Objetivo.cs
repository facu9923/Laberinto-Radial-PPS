using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objetivo : MonoBehaviour
{
    public GameObject[] referencies;

    void Start()
    {
        int randomNumber = Random.Range(0, 6);
        transform.position = referencies[randomNumber].transform.position;

        string a = "Referencias para la esfera:\n[";

        for (int i = 0; i< referencies.Length; i++)
        {
            a += "[" + referencies[i].transform.position.x.ToString() + ", " + referencies[i].transform.position.z.ToString() + "]\n";
        }
        a += "]";

        Debug.Log(a);

    }
}
