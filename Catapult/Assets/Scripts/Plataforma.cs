using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{

    public GameObject LastJugador;
    
    private int NumLastJug = 0;
    private float platMovSpeed = 0.5f;
    private GameObject currentJugador = null;
    private Jugador currentJugJugador = null;
    private List<GameObject> ListJugadores = new List<GameObject>();
    private bool MoverJugador = false;

    private void Update()
    {
        if (currentJugJugador)
        {
            MoverJugador = currentJugJugador.moverEnPlataforma;
        }

        if(MoverJugador)
        {
            MoverJugadorEnPlataforma(currentJugador);
        }
    }

    void MoverJugadorEnPlataforma(GameObject Jugador)
    {
        Debug.Log("Empieza el movimiento de " + Jugador.name);
        Vector3 targetDir = LastJugador.transform.position; //Posicion a la que queremos mover el jugador
        Jugador.transform.name = "Jugador " + transform.name + " " + NumLastJug; //Cambiamos el nombre del jugador
        Debug.Log("Posicion del jugador: " + Jugador.transform.position);
        Debug.Log("Posicion final: " + targetDir);
        Jugador.transform.position = Vector3.MoveTowards(Jugador.transform.position, new Vector3(targetDir.x, Jugador.transform.position.y, 0), platMovSpeed * Time.deltaTime);

        //Comprobamos si el jugador ya ha llegado a la posicion del Last Jugador
        if (Jugador.transform.position == new Vector3(targetDir.x, Jugador.transform.position.y, 0))
        {
            Debug.Log("El jugador se para");
            ListJugadores.Add(Jugador); //Añade el jugador a la lista de jugadores de la plataforma
            MoverLastJugador(); // Mueve de posicion el Last Jugador
        }
    }

    private void MoverLastJugador()
    {
        currentJugJugador.moverEnPlataforma = false; //El current jugador se para de mover
        LastJugador.transform.Translate(new Vector3(currentJugador.transform.localScale.x + 0.1f, 0, 0));
        Debug.Log("Pre " + NumLastJug);
        NumLastJug += 1;
        Debug.Log("Post " + NumLastJug);
        Debug.Log("Punto movido a " + LastJugador.transform.position);
    }

    void OnCollisionEnter2D(Collision2D collision)
    { 
        currentJugador = collision.collider.gameObject;
        Debug.Log("Collision tag: " + collision.collider.tag);
        Debug.Log("Collision name: " + collision.collider.name);
        currentJugador.transform.position = new Vector3(currentJugador.transform.position.x, LastJugador.transform.position.y, 0);
        currentJugJugador = currentJugador.GetComponent<Jugador>();

        currentJugJugador.moverEnPlataforma = true;


        
    }
}
