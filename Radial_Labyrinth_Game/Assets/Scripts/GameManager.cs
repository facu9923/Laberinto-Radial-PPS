using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.Networking;

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
    private GameObject objectFinded1;

    [SerializeField]
    private GameObject objectFinded2;

    private Usuario usuario;

    private int[] crossAmount;

    private uint cantidad_objetivos_encontrados;

    [SerializeField]
    private uint cantidad_brazos;

    [SerializeField]
    private int maximo;

    public void Start()
    {
        crossAmount = new int[cantidad_brazos];
    }

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

        if (this.cantidad_objetivos_encontrados >= maximo)
        {
            // Mostrar la interfaz de prueba finalizada

            loginCanvas.SetActive(false);
            finalCanvas.SetActive(true);
            objectFinded1.SetActive(false);
            objectFinded2.SetActive(false);
            ui.SetActive(true);

            StartCoroutine(EnviarDatos());
        }
        else if (this.cantidad_objetivos_encontrados == 1)
        {
            StartCoroutine(goodJob(2f, 1));
        }
        else if (this.cantidad_objetivos_encontrados == 2)
        {
            StartCoroutine(goodJob(2f, 2));
        }
    }

    IEnumerator EnviarDatos()
    {
        int cantidad_errores = 0;
        for (int i = 0; i < cantidad_brazos; i++)
            cantidad_errores += crossAmount[i];
        cantidad_errores -= maximo;

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("nombre", usuario.nombre));
        formData.Add(new MultipartFormDataSection("apellido", usuario.apellido));
        formData.Add(new MultipartFormDataSection("edad", usuario.edad.ToString()));
        formData.Add(new MultipartFormDataSection("cantidad_errores", cantidad_errores.ToString()));
        formData.Add(new MultipartFormDataSection("maximo", maximo.ToString()));

        UnityWebRequest www = UnityWebRequest.Post("https://api.laberinto-radial.tech/", formData);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }




    IEnumerator goodJob(float tiempo, int i)
    {
        if (i == 1) {
            loginCanvas.SetActive(false);
            finalCanvas.SetActive(false);
            objectFinded1.SetActive(true);
            objectFinded2.SetActive(false);
            ui.SetActive(true);
            yield return new WaitForSeconds(tiempo);
            ui.SetActive(false);
        }  else {
            loginCanvas.SetActive(false);
            finalCanvas.SetActive(false);
            objectFinded1.SetActive(false);
            objectFinded2.SetActive(true);
            ui.SetActive(true);
            yield return new WaitForSeconds(tiempo);
            ui.SetActive(false);
        }

    }

    public void addCrossAmount(int number){
        this.crossAmount[number]++;
    }
}
