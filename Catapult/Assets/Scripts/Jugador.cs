using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Idea: Script para plataformas. Poner un array de jugadores. Acceder al movimiento de los jugadores segun el array y el valor de mover
//enum cada elemento del array. Cuando choca con la plataforma, añade elemento. Mueve mientras mover sea true
// logica futura: al quitar cuerda, pop de jugador


//Cae jugador en plataforma, se mueve al Last Jugador. Cuando llegue, se añade el jugador a una lista de jugadores en plataforma
//Cuando llega, se activa la animacion de crear gancho (Solo si es el primero) y despues de tirar
//Se mueve el Last Jugador una posicion a la derecha

public class Jugador : MonoBehaviour
{

    public float platformMovSpeed;
    public GameObject JugadorPrefab;
    [SerializeField] LayerMask capaPlataforma;
    public bool moverEnPlataforma = false;
    public string variables;
    private Rigidbody2D rb;

    //private GameObject plataformaChocada;
    //private GameObject currentLastJugador;
    //private List<GameObject> currentListPlat;
    //private List<GameObject> ListJugPlatAlta = new List<GameObject>();
    //private List<GameObject> ListJugPlatMedia = new List<GameObject>();
    //private List<GameObject> ListJugPlatBaja = new List<GameObject>();
    //private int PosLastJugAlta = 0;
    //private int PosLastJugMedia = 0;
    //private int PosLastJugBaja = 0;

    private void Start()
    {
        platformMovSpeed = 0.5f;
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Zona eliminacion")
            Destroy(gameObject);        

        if(collision.collider.tag == "Borde")
            transform.position = collision.collider.gameObject.transform.position;
        
    }


    //void MovimientoEnPlataforma()
    //{
    //    Vector3 m_Input = new Vector3(-1, 0, 0);
    //    //rb.MovePosition(transform.position + m_Input * Time.deltaTime);
    //    transform.Translate(m_Input * Time.deltaTime);

    //    //rb.AddForce(new Vector2(- platformMovSpeed, 0));
    //    //if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
    //    //    rb.velocity = new Vector2(platformMovSpeed, 0);
    //    //else
    //    //    rb.velocity = new Vector2(0, 0);

    //    //Debug.Log("Empieza el movimiento "+ collision.collider.name);
    //    //Debug.Log("Empieza el movimiento 2"+ collision.collider.transform.right + m_Input * Time.deltaTime);

    //    ////Apply the movement vector to the current position, which is
    //    ////multiplied by deltaTime and speed for a smooth MovePosition

    //    //rb.AddForce(collision.collider.transform.right + m_Input * Time.deltaTime, ForceMode2D.Impulse);
    //}


}
