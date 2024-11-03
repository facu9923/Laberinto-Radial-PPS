using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossPlane : MonoBehaviour
{
    private float ultimaColisionObjetivo = 0;
    public GameManager gameManager;
    public int plane;

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player") && Time.time - ultimaColisionObjetivo > 5) {
            ultimaColisionObjetivo = Time.time;
            this.gameManager.addCrossAmount(this.plane);
        }
    }
}
