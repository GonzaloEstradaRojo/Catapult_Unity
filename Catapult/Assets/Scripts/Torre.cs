using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torre : MonoBehaviour
{

    private GameObject platAltaDer;
    private GameObject platMediaDer;
    private GameObject platBajaDer;


    private float velocidadRotacion = 0f;
    private int totalJugadoresDer; 
    private int totalJugadoresIzq;
    private int totalJugadores;

    private bool girarTorre = false;
    private bool hayGanchos = false;

    private void Start()
    {
        platAltaDer = GameObject.Find("Plataforma Alta");
        platMediaDer = GameObject.Find("Plataforma Media");
        platBajaDer = GameObject.Find("Plataforma Baja");


    }
    private void Update()
    {

        hayGanchos = (platAltaDer.transform.GetComponent<Plataforma>().cuerda.estaEnganchado ||
                      platMediaDer.transform.GetComponent<Plataforma>().cuerda.estaEnganchado ||
                      platBajaDer.transform.GetComponent<Plataforma>().cuerda.estaEnganchado);
        Debug.Log("hAY GANCHO 2 " + hayGanchos);
        List<GameObject> listJugPlatAlta = platAltaDer.GetComponent<Plataforma>().ListJugadores;
        List<GameObject> listJugPlatMedia = platMediaDer.GetComponent<Plataforma>().ListJugadores;
        List<GameObject> listJugPlatBaja = platBajaDer.GetComponent<Plataforma>().ListJugadores;

        totalJugadoresDer = listJugPlatAlta.Count + listJugPlatMedia.Count + listJugPlatBaja.Count;
        Debug.Log("Total jugadores en plataformas " + totalJugadoresDer);

        //totalJugadores = totalJugadoresDer - totalJugadoresIzq;
        //if(totalJugadores != 0)
        if (totalJugadoresDer != 0)
        {
            girarTorre = true;
            //Si es > 0 hay mas en la derecha, tiene que girar a la derecha
            //Si es < 0 hay mas en la izquierda, tiene que girar a la izquierda
            InclinarTorre();
        }
        else
        {
            girarTorre = false;
        }

        if(girarTorre == true && hayGanchos)
        {
            Debug.Log("Girar porque " + hayGanchos);
            InclinarTorre();
            //Invoke("InclinarTorre", 50f);
        }
    }

    private void InclinarTorre()
    {
        Debug.Log("Torre inclinandose");
        //angle > 0 gira a la izquierda
        //angle < 0 gira a la derecha
        float angle = -1 * totalJugadoresDer * Time.deltaTime;
        //float angle = -10 * totalJugadores;
        transform.Rotate(new Vector3(0, 0, angle));
        //transform.rotation = Quaternion.Euler(0,0,angle);
    }
}
