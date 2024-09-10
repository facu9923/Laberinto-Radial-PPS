using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Usuario usuario;
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
            ui.SetActive(true);
        }
    }



}
