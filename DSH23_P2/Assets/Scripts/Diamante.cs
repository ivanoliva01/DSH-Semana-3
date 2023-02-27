using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamante : MonoBehaviour
{
    // [SerializeField] hace que el objeto sea privado pero aparezca como públic en el editor de Unity
    // [Tooltip("Explicacion de la variable")] hace que cuando pongas el cursor en la variable, se muestre "Explicacion de la variable"

    [SerializeField] [Tooltip("Velocidad de rotacion.")]
    float velocidad = 45.0f;

    //[SerializeField] [Tooltip("Puntuacion obtenida al tocar el premio.")]
    //int puntuacion = 1;

    [SerializeField] [Tooltip("Prefab de la particular a usar al tocar el premio.")]
    GameObject particle;



    // Start is called before the first frame update
    void Start()
    {
        //ParticleSystem particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, Time.deltaTime * velocidad);
    }

    // Generar particulas
    void OnDestroy()
    {
        // Generar las particulas con una rotación aleatoria
        Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        Instantiate(particle.gameObject, transform.position, randomRotation);
        
        // Destruir las particulas despues de 10 segundos
        Destroy(particle, 10.0f);
    }
}
