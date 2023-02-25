using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamante : MonoBehaviour
{

    public float velocidad = 45.0f;
    public int puntuacion = 1;

    public GameObject particle;

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
