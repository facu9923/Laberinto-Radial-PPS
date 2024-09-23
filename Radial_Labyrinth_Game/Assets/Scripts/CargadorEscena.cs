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

        if (cantidad_brazos != 6 && cantidad_brazos != 8)
        {
            text.text = "Cantidad de brazos invalida";
            return;
        }

        text.text = "Cargando";

        string path;

        if (cantidad_brazos == 8)
            path = "Scenes/NaturalTexture/EightArms/NaturalEightNormal";
        else
            path = "Scenes/NaturalTexture/SixArms/NaturalSixArmNormal";

        SceneManager.LoadScene(path, LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
