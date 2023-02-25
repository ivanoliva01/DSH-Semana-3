using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public Camera cam;
    public GameObject prefabSuelo;
    public int velocidad;

    Vector3 offset;
    //Rigidbody rb;

    float valX;
    float valZ;


    private Rigidbody rb;
    private Vector3 direccionActual;
    private AudioSource SonidoDeCambioDireccion;

    // Start is called before the first frame update
    void Start()
    {
        offset = cam.transform.position;
        //rb = GetComponent<Rigidbody>();

        valX = 0.0f;
        valZ = 0.0f;
        rb = GetComponent<Rigidbody>();
        direccionActual = Vector3.forward;
        SonidoDeCambioDireccion = GetComponent<AudioSource>();
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
        cam.transform.position = this.transform.position + offset; //para mover la camara con la bol
        if(Input.GetKeyUp(KeyCode.Space)){
            SonidoDeCambioDireccion.Play();
            if(direccionActual == Vector3.forward)
                direccionActual = Vector3.right;
            else
                direccionActual = Vector3.forward;
        }

        float tiempo = velocidad * Time.deltaTime;
        rb.transform.Translate(direccionActual*tiempo);
        
    }

    void SueloInicial()
    {
        for (int n = 0; n < 3; n++)
        {
            valZ += 6.0f;
            GameObject elSuelo = Instantiate(prefabSuelo, new Vector3(valX, 0.0f, valZ), Quaternion.identity) as GameObject;
        }
    }

    void OnCollisionExit(Collision other) {
        if(other.transform.tag == "Suelo"){
            StartCoroutine(CrearSuelo(other));
        }
    }

    IEnumerator CrearSuelo(Collision col){
        yield return new WaitForSeconds(0.5f); //espera de 5 s
        col.rigidbody.isKinematic = false;
        col.rigidbody.useGravity = true;
        yield return new WaitForSeconds(1f); //espera de 5 s
        Destroy(col.gameObject); // se destruye el suelo
        float ran = Random.Range(0f,1f); //creo un suelo nuevo de forma aleatoria, o hacia delante o hacia la derecha
        if(ran < 0.5f)//creo un cubo hacia delante
            valX += 6.0f;
        else
            valZ += 6.0f;
        
        //creo un cubo nuevo
        GameObject elSuelo = Instantiate(prefabSuelo, new Vector3(valX, 0.0f, valZ), Quaternion.identity) as GameObject;
    }
}
