using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
  // Este script es activado cuando el boton de reinicio en la escena DerroteEscena es pulsado.
  
  // Start is called before the first frame update
  public void Reiniciarnuestrojuegos()
  {
    SceneManager.LoadScene("SampleScene");  
  }

  // Update is called once per frame
  void Update()
  {
      
  }
}
