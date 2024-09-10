using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class BotonIniciar : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private TMP_InputField nombreField;

    [SerializeField]
    private TMP_InputField apellidoField;

    [SerializeField]
    private TMP_InputField edadField;

    public void ClickIniciar()
    {
        uint edadParsed;

        if (!UInt32.TryParse(edadField.text, out edadParsed)/* || edadParsed > 130*/)
        {
            // La edad es invalida
            return;
        }

        // Aca se deberia chequear que el usuario ingrese algo en los input

        Usuario usuario = new Usuario(
            nombreField.text,
            apellidoField.text,
            edadParsed
        );

        gameManager.IniciarExperimento(usuario);        
    }
    
    void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(ClickIniciar);
    }
}
