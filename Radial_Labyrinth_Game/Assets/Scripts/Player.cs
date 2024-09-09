using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject reference;

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

        transform.Translate(moveX, 0, moveZ);

        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Objetivo"))
        {
            Vector3 newPosition = new Vector3(9.24f,7.23f,40.98f);
            transform.position = newPosition;
            float randomNumber = Random.Range(0f, 360f);
            transform.localRotation = Quaternion.Euler(0f, randomNumber, 0f);
        }
    }
}
