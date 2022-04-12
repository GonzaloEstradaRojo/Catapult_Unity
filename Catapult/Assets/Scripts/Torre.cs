using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torre : MonoBehaviour
{

    private GameObject platAltaDer;
    private GameObject platMediaDer;
    private GameObject platBajaDer;
    
    private GameObject platAltaIzq;
    private GameObject platMediaIzq;
    private GameObject platBajaIzq;


    private bool estaGirando = false;
    private GameObject rotorTorre;


    private int totalJugadoresDer;
    private int totalJugadoresIzq;
    private int totalJugadores;

    private float anguloTorre = 180f;
    private float tiempoDeGiro = 1.0f;

    private bool hayGanchos = false;

    private void Start()
    {
        //StartCoroutine("InclinarTorre");
        platAltaDer = GameObject.Find("Plataforma Alta Der");
        platMediaDer = GameObject.Find("Plataforma Media Der");
        platBajaDer = GameObject.Find("Plataforma Baja Der");
        
        platAltaIzq = GameObject.Find("Plataforma Alta Izq");
        platMediaIzq = GameObject.Find("Plataforma Media Izq");
        platBajaIzq = GameObject.Find("Plataforma Baja Izq");

        rotorTorre = GameObject.Find("Rotor Torre");
    }

    private void Update()
    {
        //Angulo del rotor de la torre
        anguloTorre = rotorTorre.transform.eulerAngles.z;


        hayGanchos = (platAltaDer.transform.GetComponent<Plataforma>().cuerda.estaEnganchado ||
                      platMediaDer.transform.GetComponent<Plataforma>().cuerda.estaEnganchado ||
                      platBajaDer.transform.GetComponent<Plataforma>().cuerda.estaEnganchado||
                      platAltaIzq.transform.GetComponent<Plataforma>().cuerda.estaEnganchado ||
                      platMediaIzq.transform.GetComponent<Plataforma>().cuerda.estaEnganchado ||
                      platBajaIzq.transform.GetComponent<Plataforma>().cuerda.estaEnganchado);

        List<GameObject> listJugPlatAltaDer = platAltaDer.GetComponent<Plataforma>().ListJugadores;
        List<GameObject> listJugPlatMediaDer = platMediaDer.GetComponent<Plataforma>().ListJugadores;
        List<GameObject> listJugPlatBajaDer = platBajaDer.GetComponent<Plataforma>().ListJugadores;

        totalJugadoresDer = listJugPlatAltaDer.Count + listJugPlatMediaDer.Count + listJugPlatBajaDer.Count;
        
        List<GameObject> listJugPlatAltaIzq = platAltaIzq.GetComponent<Plataforma>().ListJugadores;
        List<GameObject> listJugPlatMediaIzq = platMediaIzq.GetComponent<Plataforma>().ListJugadores;
        List<GameObject> listJugPlatBajaIzq = platBajaIzq.GetComponent<Plataforma>().ListJugadores;

        totalJugadoresIzq = listJugPlatAltaIzq.Count + listJugPlatMediaIzq.Count + listJugPlatBajaIzq.Count;

        totalJugadores = totalJugadoresDer + totalJugadoresIzq; 

        if (totalJugadores != 0 && hayGanchos)
        {
            tiempoDeGiro = 1f + 1f / (1f + Math.Max(totalJugadoresDer,totalJugadoresIzq) - Math.Min(totalJugadoresDer, totalJugadoresIzq));
            StartCoroutine("InclinarTorre");
        }
    }

    private IEnumerator InclinarTorre()
    {
        if (estaGirando)
        {
            yield return null;
        }
        else
        {
            if (anguloTorre > 245 || anguloTorre < 115)
            {
                StopCoroutine("InclinarTorre");
            }

            int direccion = (totalJugadoresDer == totalJugadoresIzq) ? 0 : ((totalJugadoresDer > totalJugadoresIzq) ? -1 : 1);
            estaGirando = true;
            rotorTorre.transform.Rotate(new Vector3(0, 0, direccion * 5f));
            yield return new WaitForSeconds(tiempoDeGiro);
            estaGirando = false;
        }
    }    
}
