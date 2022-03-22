using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torre : MonoBehaviour
{

    private GameObject platAltaDer;
    private GameObject platMediaDer;
    private GameObject platBajaDer;
    private GameObject rotorTorre;


    private float velocidadRotacion = 10f;
    private int totalJugadoresDer;
    private int totalJugadoresIzq;
    private int totalJugadores;
    private float anguloRotacionTorre = 5f;
    private float anguloTorre = 180f;
    private float tiempoDeGiro = 1.0f;

    private bool girarTorre = false;
    private bool hayGanchos = false;

    private void Start()
    {
        platAltaDer = GameObject.Find("Plataforma Alta");
        platMediaDer = GameObject.Find("Plataforma Media");
        platBajaDer = GameObject.Find("Plataforma Baja");
        rotorTorre = GameObject.Find("Rotor Torre");
    }

    private void Update()
    {

        anguloTorre = rotorTorre.transform.rotation.z;
        print($"anguloTorre {anguloTorre}");

        hayGanchos = (platAltaDer.transform.GetComponent<Plataforma>().cuerda.estaEnganchado ||
                      platMediaDer.transform.GetComponent<Plataforma>().cuerda.estaEnganchado ||
                      platBajaDer.transform.GetComponent<Plataforma>().cuerda.estaEnganchado);

        List<GameObject> listJugPlatAlta = platAltaDer.GetComponent<Plataforma>().ListJugadores;
        List<GameObject> listJugPlatMedia = platMediaDer.GetComponent<Plataforma>().ListJugadores;
        List<GameObject> listJugPlatBaja = platBajaDer.GetComponent<Plataforma>().ListJugadores;

        totalJugadoresDer = listJugPlatAlta.Count + listJugPlatMedia.Count + listJugPlatBaja.Count;

        //totalJugadores = totalJugadoresDer - totalJugadoresIzq;
        //if(totalJugadores != 0)
        //if (totalJugadoresDer != 0)
        //{
        //    girarTorre = true;
        //    //Si es > 0 hay mas en la derecha, tiene que girar a la derecha
        //    //Si es < 0 hay mas en la izquierda, tiene que girar a la izquierda
        //    InclinarTorre();
        //}
        //else
        //{
        //    girarTorre = false;
        //}

        if (totalJugadoresDer != 0 && hayGanchos)
        {
            tiempoDeGiro = 1 + 1 / totalJugadoresDer;
            print($"totalJugDer {totalJugadoresDer}   tiempo de giro  {tiempoDeGiro}");
            //InvokeRepeating("GiraTorre", 0f, tiempoDeGiro);
            StartCoroutine(InclinarTorre());
            //Invoke("InclinarTorre", 50f);
        }
    }

    private void GiraTorre()
    {
        
        rotorTorre.transform.Rotate(new Vector3(0, 0, 0.1f));
    }

    //private IEnumerator ComprobarInclinacionTorre()
    //{
    //    yield return new WaitForSeconds(0f);
    //    StartCoroutine(InclinarTorre());

    //}

    private IEnumerator InclinarTorre()
    {

        for (float f = 0; f < 10; f++)
        {
            rotorTorre.transform.Rotate(new Vector3(0, 0, 0.01f));
            yield return new WaitForSeconds(2f);
        }

        //yield return new WaitForSeconds(2f);
        //rotorTorre.transform.Rotate(new Vector3(0, 0, 0.1f));
    }
        //rotorTorre.transform.rotation = Quaternion.Euler(0, 0, angle);


        //Debug.Log("Torre inclinandose "+ rotorTorre.transform.localRotation.eulerAngles.z);
        //angle > 0 gira a la izquierda
        //angle < 0 gira a la derecha
        //yield return new WaitForSeconds(3f);
        //float inclinacion = rotorTorre.transform.rotation.eulerAngles.z;
        ////print("1 " + ((inclinacion >= 140 && inclinacion <= 220)));


        ////while ((inclinacion >= -0.5 && inclinacion<=70) || (inclinacion<=360 && inclinacion >= 310))
        //if ((inclinacion >= 140 && inclinacion <= 220))
        //{
        //    //float angle = -10 * totalJugadores;
        //    //angle = -1 * totalJugadoresDer*0.5f;
        //    print("angulo" + angle);
        //    yield return new WaitForSeconds(5f);
        //    //rotorTorre.transform.Rotate(new Vector3(0, 0, angle));

        //    angle += -1 * totalJugadoresDer * Time.deltaTime;
        //    rotorTorre.transform.rotation = Quaternion.Euler(0, 0, angle);

        //    //print(angle + " angle");
        //    //yield return null;
        //}
        //yield return new WaitForSeconds(10f);
    
}
