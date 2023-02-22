using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public Camera cam;
    public float velocidad;
    public GameObject prefabSuelo;

    Vector3 offset;
    //Rigidbody rb;

    float valX;
    float valZ;


    // Start is called before the first frame update
    void Start()
    {
        offset = cam.transform.position;
        //rb = GetComponent<Rigidbody>();

        valX = 0.0f;
        valZ = 0.0f;
        SueloInicial();
    }

    // Update is called once per frame
    void Update()
    {
        // Obtener el input del movimiento
        //float movHorizontal = Input.GetAxis("Horizontal");
        //float movVertical = Input.GetAxis("Vertical");

        // Mover el jugador
        //Vector3 movimiento = new Vector3(movHorizontal, 0.0f, movVertical);
        //rb.AddForce(movimiento * velocidad);

        // Mover la camara
        //cam.transform.position = this.transform.position + offset;
    }

    void SueloInicial()
    {
        for (int n = 0; n < 3; n++)
        {
            valZ += 6.0f;
            GameObject elSuelo = Instantiate(prefabSuelo, new Vector3(valX, 0.0f, valZ), Quaternion.identity) as GameObject;
        }
    }
}
