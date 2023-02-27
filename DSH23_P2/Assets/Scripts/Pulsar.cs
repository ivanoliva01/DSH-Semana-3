using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pulsar : MonoBehaviour
{
    // [SerializeField] hace que el objeto sea privado pero aparezca como públic en el editor de Unity

    [SerializeField] Button btn;
    [SerializeField] Image img;
    [SerializeField] Sprite[] spNumeros;
    [SerializeField] Text texto;

    [SerializeField] AudioSource source {get {return GetComponent<AudioSource>();}}
    [SerializeField] AudioClip clip;

    bool contar;
    int numero;

    // Start is called before the first frame update
    void Start()
    {
        // Si no pudiesemos poner el objeto público, se puede buscar cualquier elemento de tipo Button (pero si hay varios...)
        //btn = GameObject.FindAnyObjectByType<Button>();
        // Otra manera es mediante etiquetas, donde etiqueta sería la etiqueta que le asignemos
        //btn = GameObject.FindWithTag("etiqueta").GetComponent<Button>();
        // En este caso, se ha dejado boton como público

        // Añadimos un listener, y cuando pulsemos el boton se llama a la función Pulsado
        btn.onClick.AddListener(Pulsado);
        gameObject.AddComponent<AudioSource>();
        contar = false;
        numero = 3;
    }

    void Pulsado()
    {
        // Ejecutar el sonido clip:
        source.PlayOneShot(clip);

        img.gameObject.SetActive(true);

        // Desactivamos el boton para evitar pulsarlo varias veces
        btn.gameObject.SetActive(false);
        contar = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (contar)
        {
            switch (numero)
            {
                case 0: 
                    SceneManager.LoadScene("SegundaEscena", LoadSceneMode.Single); 
                    break;
                case 1:
                    img.sprite = spNumeros[0];
                    texto.text = "1";
                    break;
                case 2:
                    img.sprite = spNumeros[1];
                    texto.text = "2";
                    break;
                case 3:
                    img.sprite = spNumeros[2];
                    texto.text = "3";
                    break;
            }
            // Empezamos una corutina para que espere x tiempo y entonces haga el resto de la función
            StartCoroutine(Esperar());
            contar = false;
            numero--;
        }
    }

    IEnumerator Esperar()
    {
        yield return new WaitForSeconds(1.0f);

        contar = true;

    }
}
