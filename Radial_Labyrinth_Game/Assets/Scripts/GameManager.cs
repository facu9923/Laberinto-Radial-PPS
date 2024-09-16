using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ui;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject loginCanvas;

    [SerializeField]
    private GameObject finalCanvas;

    [SerializeField]
    private GameObject objectFinded;

    private Usuario usuario;

    private int[] crossAmount = {0,0,0,0,0,0};

    private uint cantidad_objetivos_encontrados;

    public void IniciarExperimento(Usuario usuario)
    {
        this.usuario = usuario;
        this.cantidad_objetivos_encontrados = 0;

        ui.SetActive(false);
        player.SetActive(true);
    }

    public void InformarObjetivoEncontrado()
    {
        this.cantidad_objetivos_encontrados++;

        if (this.cantidad_objetivos_encontrados == 3)
        {
            // Mostrar la interfaz de prueba finalizada

            loginCanvas.SetActive(false);
            finalCanvas.SetActive(true);
            objectFinded.SetActive(false);
            ui.SetActive(true);
        }else{
            StartCoroutine(goodJob(2f));
        }
    }

    IEnumerator goodJob(float tiempo)
    {
        loginCanvas.SetActive(false);
        finalCanvas.SetActive(false);
        objectFinded.SetActive(true);
        ui.SetActive(true);
        yield return new WaitForSeconds(tiempo);
        ui.SetActive(false);

    }

    public void addCrossAmount(int number){
        this.crossAmount[number]++;
        Debug.Log(this.crossAmount[number]);
    }



}
