using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class CargadorEscena : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;

    void Start()
    {
        int cantidad_brazos = URLParameters.GetSearchParameters().GetInt("brazos", 6);
        string dificultad = URLParameters.GetSearchParameters().GetValueOrDefault("dificultad", "normal");
        string ambiente = URLParameters.GetSearchParameters().GetValueOrDefault("ambiente", "naturaleza");
        string gameID = URLParameters.GetSearchParameters().GetValueOrDefault("id", "999999999999");

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

        if (ambiente != "naturaleza" && ambiente != "clinica")
        {
            text.text = "Ambiente invalido";
            return;
        }

        if (gameID.Length != 12 || !Regex.IsMatch(gameID, "^[A-Za-z0-9]+$"))
        {
            text.text = "ID de juego invalido";
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

        if (ambiente == "clinica")
            sceneBuildIndex += 6;

        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }
}
