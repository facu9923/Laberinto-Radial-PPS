using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class CargadorEscena : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;


    // Start is called before the first frame update
    void Start()
    {

        int cantidad_brazos = URLParameters.GetSearchParameters().GetInt("brazos", 8);
        string dificultad = URLParameters.GetSearchParameters().GetValueOrDefault("dificultad", "normal");

        if (cantidad_brazos != 6 && cantidad_brazos != 8 && cantidad_brazos != 12)
        {
            text.text = "Cantidad de brazos invalida";
            return;
        }

        if (dificultad != "normal" && dificultad != "facil")
        {
            text.text = "Dificultad invalida";
            return;
        }

        int sceneBuildIndex = 4;

        if (cantidad_brazos == 6 && dificultad == "facil")
            sceneBuildIndex = 1;
        if (cantidad_brazos == 6 && dificultad == "normal")
            sceneBuildIndex = 2;
        if (cantidad_brazos == 8 && dificultad == "facil")
            sceneBuildIndex = 3;
        if (cantidad_brazos == 8 && dificultad == "normal")
            sceneBuildIndex = 4;
        if (cantidad_brazos == 12 && dificultad == "facil")
            sceneBuildIndex = 5;
        if (cantidad_brazos == 12 && dificultad == "normal")
            sceneBuildIndex = 6;

        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
