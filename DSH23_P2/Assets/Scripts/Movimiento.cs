using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Movimiento : MonoBehaviour
{
    // [SerializeField] hace que el objeto sea privado pero aparezca como públic en el editor de Unity
    // [Tooltip("Explicacion de la variable")] hace que cuando pongas el cursor en la variable, se muestre "Explicacion de la variable"

    [SerializeField] Camera cam;

    [SerializeField] [Tooltip("Prefab que se va a usar como suelo.")]
    GameObject prefabSuelo;

    [SerializeField] [Tooltip("Velocidad del jugador.")]
    int velocidad;

    Vector3 offset;
    float valX;
    float valZ;

    int premios;
    [SerializeField] Text textoPuntuacion;

    [SerializeField] [Tooltip("Prefab que se va a usar como premio.")]
    GameObject prefabPremio;

    Scene escena;
    private Rigidbody rb;

    private Vector3 direccionActual;
    private AudioSource AudioSource;
    [SerializeField] private AudioClip SonidoDeCambioDireccion;
    [SerializeField] private AudioClip SonidoChoque;

    //GameObject Reiniciar; //boton de reinicio

    // Start is called before the first frame update
    void Start()
    {
        // Obtenemos la posicion de la camara al inicio del juego
        offset = cam.transform.position;
        premios = 0;
        // Obtener la escena activa
        escena = SceneManager.GetActiveScene();
        valX = 0.0f;
        valZ = 0.0f;
        rb = GetComponent<Rigidbody>();
        direccionActual = Vector3.forward;
        AudioSource = GetComponent<AudioSource>();

        // Llamamos a SueloInicial:
        SueloInicial();

        //Reiniciar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = this.transform.position + offset; //para mover la camara con la bola

        // Obtener input del jugador para cambiar la dirección de la pelota
        if(Input.GetKeyUp(KeyCode.Space)){
            AudioSource.PlayOneShot(SonidoDeCambioDireccion);
            if(direccionActual == Vector3.forward)
                direccionActual = Vector3.right;
            else
                direccionActual = Vector3.forward;
        }

        // Mover la pelota:
        float tiempo = velocidad * Time.deltaTime;
        rb.transform.Translate(direccionActual*tiempo);

        // Si el jugador se cae, cargar la escena de derrota
        if (transform.position.y <= -10.0f)
        {
            SceneManager.LoadScene("DerrotaEscena", LoadSceneMode.Single);
            //Reiniciar.gameObject.SetActive(true);
        }
    }

    void SueloInicial()
    {
        // Generar tres plataformas delante del jugador:
        for (int n = 0; n < 3; n++)
        {
            valZ += 6.0f;
            GameObject elSuelo = Instantiate(prefabSuelo, new Vector3(valX, 0.0f, valZ), Quaternion.identity) as GameObject;
        }
    }

    void OnCollisionExit(Collision other) {
        // Al salir de un suelo, llamar a la corutina CrearSuelo
        if(other.transform.tag == "Suelo"){
            StartCoroutine(CrearSuelo(other));
        }
    }

    IEnumerator CrearSuelo(Collision col){
        yield return new WaitForSeconds(0.5f); //espera de 0.5 s
        // Hacer que el suelo caiga:
        col.rigidbody.isKinematic = false;
        col.rigidbody.useGravity = true;

        float ran = Random.Range(0f,1f); //creo un suelo nuevo de forma aleatoria, o hacia delante o hacia la derecha
        if(ran <= 0.5f)//creo un cubo hacia delante
            valX += 6.0f;
        else
            valZ += 6.0f;
        
        //creo un cubo nuevo
        GameObject elSuelo = Instantiate(prefabSuelo, new Vector3(valX, 0.0f, valZ), Quaternion.identity) as GameObject;

        // Generar premios: (se podría poner dentro de los if anteriores, pero por claridad he preferido ponerlo separado)
        // Generar el premio en un sitio aleatorio del suelo
        float ranSpawnZ = Random.Range(-2.0f, 2.0f);
        float ranSpawnX = Random.Range(-2.0f, 2.0f);

        // Premio delante
        if (ran <= 0.25f)
        {
            GameObject elPremio = Instantiate(prefabPremio, new Vector3(valX, 1.0f, valZ + ranSpawnZ), Quaternion.identity) as GameObject;
        }
        // Premio a la derecha
        if (ran >= 0.75f)
        {
            GameObject elPremio = Instantiate(prefabPremio, new Vector3(valX + ranSpawnX, 1.0f, valZ), Quaternion.identity) as GameObject;
        }

        // Se destruye el suelo después de crear el nuevo suelo para evitar que la espera no cree el nuevo suelo antes de que el jugador llegue
        yield return new WaitForSeconds(1.0f); //espera de 1 s
        Destroy(col.gameObject); // se destruye el suelo
    }

    void OnTriggerEnter(Collider other)
    {
        // Si el objeto con el que nos chocamos es un premio, destruirlo y aumentar premios
        if (other.gameObject.CompareTag("Premio"))
        {
            // Ejecutar el sonido SonidoChoque:
            AudioSource.PlayOneShot(SonidoChoque);
            Debug.Log("Has conseguido un premio!");
            premios++;
            textoPuntuacion.text = "Puntuación: " + premios;
            Destroy(other.gameObject);

            // Pasar al siguiente nivel o ganar, dependiendo del nivel actual y los premios conseguidos
            if (premios == 3 && escena.name == "SegundaEscena")
            {
                SceneManager.LoadScene("TerceraEscena", LoadSceneMode.Single);    
            }
            if (premios == 5 && escena.name == "TerceraEscena")
            {
                SceneManager.LoadScene("VictoriaEscena", LoadSceneMode.Single);  
            }
        }
        
    }
}
