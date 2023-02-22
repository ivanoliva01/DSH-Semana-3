using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pulsar : MonoBehaviour
{

    public Button btn;
    public Image img;
    public Sprite[] spNumeros;

    public Text texto;

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

        contar = false;
        numero = 3;
    }

    void Pulsado()
    {
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
                    Debug.Log("Terminado - Salto a otra escena.");
                    break;
                case 1:
                    img.sprite = spNumeros[0];
                    texto.text = "1";

                    // Estaba probando como mover el elemento, pero pasamos a otra cosa
                    //texto.rectTransform.anchoredPosition = texto.rectTransform.anchoredPosition + new Vector2(30.0f, texto.rectTransform.anchoredPosition.y);
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
        yield return new WaitForSeconds(1);

        contar = true;

    }
}
