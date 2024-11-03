using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.Networking;
using System.Threading;
using System.Text;
using UnityEngine.UI;



enum Estado
{
    Inicial,
    Jugando,
    Finalizado
};

class GeneradorAleatorio
{
    public static string GenerarCadenaAleatoria(int longitud = 12)
    {
        const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        StringBuilder resultado = new StringBuilder(longitud);
        System.Random random = new System.Random();

        for (int i = 0; i < longitud; i++)
        {
            int index = random.Next(caracteres.Length);
            resultado.Append(caracteres[index]);
        }

        return resultado.ToString();
    }
}


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

    Estado estado;

    [SerializeField]
    private int maximo;

    private string gameID;
    int cantidad_brazos;
    string dificultad;
    string ambiente;

    [SerializeField]
    int intervalInMilliseconds = 4000;


#if UNITY_EDITOR
    string endpoint = "http://localhost:80";
#else
    string endpoint = "https://api.laberinto-radial.tech";
#endif

    private float tiempoUltimoEnvio = 0f;

    float tiempo_inicio;

    public void Start()
    {
        gameID = URLParameters.GetSearchParameters().GetValueOrDefault("id", GeneradorAleatorio.GenerarCadenaAleatoria());
        cantidad_brazos = URLParameters.GetSearchParameters().GetInt("brazos", 8);
        dificultad = URLParameters.GetSearchParameters().GetValueOrDefault("dificultad", "normal");
        ambiente = URLParameters.GetSearchParameters().GetValueOrDefault("ambiente", "naturaleza");

        estado = Estado.Inicial;


        crossAmount = new int[cantidad_brazos];
    }

    void Update()
    {
        // Solo ejecuta el envío cuando el juego está en estado Jugando y ha pasado el intervalo
        if (estado == Estado.Jugando && Time.time - tiempoUltimoEnvio >= intervalInMilliseconds / 1000)
        {
            tiempoUltimoEnvio = Time.time;  // Actualiza el tiempo del último envío

            // Configura el formulario de datos y llama a la corutina de envío
            List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
            formData.Add(new MultipartFormDataSection("id", gameID));
            formData.Add(new MultipartFormDataSection("x", player.transform.position.x.ToString()));
            formData.Add(new MultipartFormDataSection("y", player.transform.position.z.ToString()));
            formData.Add(new MultipartFormDataSection("escena", Util.getSceneID(cantidad_brazos, dificultad, ambiente).ToString()));
            formData.Add(new MultipartFormDataSection("brazo", player.GetComponent<Player>().GetArm().ToString()));

            StartCoroutine(EnviarDatosRecurrentes(formData));
        }
    }

    IEnumerator EnviarDatosRecurrentes(List<IMultipartFormSection> formData)
    {
        UnityWebRequest www = UnityWebRequest.Post(endpoint + "/datos-recurrentes", formData);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Datos recurrentes enviados con éxito: " + www.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Error en el envío de datos recurrentes: " + www.error);
        }
    }

    public void IniciarExperimento(Usuario usuario)
    {
        this.usuario = usuario;
        this.cantidad_objetivos_encontrados = 0;

        for (int i = 0; i < cantidad_brazos; i++)
            crossAmount[i] = 0;

        estado = Estado.Jugando;

        ui.SetActive(false);
        player.SetActive(true);

        tiempo_inicio = Time.time;

        StartCoroutine(EnviarDatosIniciales());
    }

    public void InformarObjetivoEncontrado()
    {
        this.cantidad_objetivos_encontrados++;

        if (this.cantidad_objetivos_encontrados >= maximo)
        {
            // Mostrar la interfaz de prueba finalizada

            estado = Estado.Finalizado;

            loginCanvas.SetActive(false);
            finalCanvas.SetActive(true);
            objectFinded1.SetActive(false);
            objectFinded2.SetActive(false);
            ui.SetActive(true);

            StartCoroutine(EnviarDatosFinales());
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

    IEnumerator EnviarDatosFinales()
    {
        float segundos_transcurridos = Time.time - tiempo_inicio;

        int cantidad_errores = 0;
        for (int i = 0; i < cantidad_brazos; i++)
            cantidad_errores += crossAmount[i];
        cantidad_errores -= maximo;

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("id", gameID));
        formData.Add(new MultipartFormDataSection("cantidad_errores", cantidad_errores.ToString()));
        formData.Add(new MultipartFormDataSection("duracion_segundos", segundos_transcurridos.ToString()));

        UnityWebRequest www = UnityWebRequest.Post(endpoint + "/datos-finales", formData);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Se enviaron los datos finales!");
        }
    }

    IEnumerator EnviarDatosIniciales()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();

        formData.Add(new MultipartFormDataSection("id", gameID));
        formData.Add(new MultipartFormDataSection("cantidad_brazos", cantidad_brazos.ToString()));
        formData.Add(new MultipartFormDataSection("ambiente", ambiente));
        formData.Add(new MultipartFormDataSection("dificultad", dificultad));
        formData.Add(new MultipartFormDataSection("nombre", usuario.nombre));
        formData.Add(new MultipartFormDataSection("apellido", usuario.apellido));
        formData.Add(new MultipartFormDataSection("edad", usuario.edad.ToString()));

        UnityWebRequest www = UnityWebRequest.Post(endpoint + "/datos-iniciales", formData);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Se enviaron los datos iniciales!");
        }
    }

    /*
    IEnumerator EnviarDatosFinales()
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
    */

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
