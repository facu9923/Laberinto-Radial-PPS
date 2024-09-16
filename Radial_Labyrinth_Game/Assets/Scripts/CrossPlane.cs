using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossPlane : MonoBehaviour
{
    private float ultimaColisionObjetivo = 0;
    public GameManager gameManager;
    public int plane;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player") && Time.time - ultimaColisionObjetivo > 10){
            this.gameManager.addCrossAmount(this.plane);
            ultimaColisionObjetivo = Time.time;
        }
    }
}
