using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    public float moveSpeed = 5f;
    public GameObject reference;

    public float runMultiplier = 2f;

    private float ultimaColisionObjetivo = 0;

    private int brazoActual = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = reference.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveX *= runMultiplier;
            moveZ *= runMultiplier;
        }

        transform.Translate(moveX, 0, moveZ);

        // Cursor.lockState = CursorLockMode.Locked;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Objetivo") && Time.time - ultimaColisionObjetivo > 1)
        {
            transform.position = reference.transform.position;
            float randomNumber = UnityEngine.Random.Range(0f, 360f);
            transform.Rotate(new Vector3(0f, randomNumber, 0f));

            ultimaColisionObjetivo = Time.time;

            gameManager.InformarObjetivoEncontrado();

        }
    }

    public void OnNewArm(int armID)
    {
        Debug.Log("Estas en el brazo: " +  armID);
        brazoActual = armID;
    }

    public void OnSpawn()
    {
        Debug.Log("Estas en el spawn");
        brazoActual = 0;
    }

    public int GetArm()
    {
        return brazoActual;
    }
}
