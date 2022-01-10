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

    private Rigidbody2D rb;
    private bool moverEnPlataforma = false;
    private GameObject plataformaChocada;
    private GameObject currentLastJugador;
    private List<GameObject> currentListPlat;
    private List<GameObject> ListJugPlatAlta = new List<GameObject>();
    private List<GameObject> ListJugPlatMedia = new List<GameObject>();
    private List<GameObject> ListJugPlatBaja = new List<GameObject>();

    private void Start()
    {
        platformMovSpeed = 0.5f;
        rb = GetComponent<Rigidbody2D>();
        Debug.DrawRay( transform.position - new Vector3(transform.localScale.x / 2, transform.localScale.y / 2, 0), Vector3.down*0.15f, Color.red);
        Debug.DrawRay( transform.position - new Vector3(-transform.localScale.x / 2, transform.localScale.y / 2, 0), Vector3.down*0.15f, Color.green);
    }

    private void Update()
    {
        //RaycastHit2D rayCastDer = Physics2D.Raycast(transform.position - new Vector3(transform.localScale.x / 2, transform.localScale.y / 2, 0), Vector2.down,0.15f, capaPlataforma);
        //RaycastHit2D rayCastIzq = Physics2D.Raycast(transform.position - new Vector3(-transform.localScale.x / 2, transform.localScale.y / 2, 0), Vector2.down,0.15f, capaPlataforma);
        ////Raycast empezando desde los pies en la esquina izq y derecha
        //Debug.DrawRay( transform.position - new Vector3(transform.localScale.x / 2, transform.localScale.y / 2, 0), Vector3.down*0.15f, Color.red);
        //Debug.DrawRay( transform.position - new Vector3(-transform.localScale.x / 2, transform.localScale.y / 2, 0), Vector3.down*0.15f, Color.green);
        //GameObject plataformaChocada = null;
        ////GameObject newJugador = null;
        ////Si alguna esquina choca con la plataforma, se crea el nuevo jugador en la plataforma
        //if (rayCastIzq || rayCastDer)
        //{
        //    //Debug.Log("RayCastDer collider " + rayCastDer.collider);
        //    //Debug.Log("RayCastIzq collider " + rayCastIzq.collider);
        //    plataformaChocada = rayCastIzq ? rayCastIzq.collider.gameObject : rayCastDer.collider.gameObject;

        //    if(plataformaChocada.name == "Plataforma Alta")
        //    {
        //        currentLastJugador = LastJugadorAlta;

        //    }else if (plataformaChocada.name == "Plataforma Media")
        //    {
        //        currentLastJugador = LastJugadorMedia;

        //    }else if (plataformaChocada.name == "Plataforma Baja")
        //    {
        //        currentLastJugador = LastJugadorBaja;
        //    }

        //    //Destroy(gameObject);
        //    //newJugador = CrearJugadorEnPlataforma(transform.position);
        //    moverEnPlataforma = true;

        //}

        //Debug.Log(moverEnPlataforma + "Move en plataforma");

        if (moverEnPlataforma)
        {
            MoverJugadorEnPlataforma(currentLastJugador.transform.position);
        }
    }

    //GameObject CrearJugadorEnPlataforma(Vector3 creationPos)
    //{
    //    Debug.Log("Jugador creado");
    //    GameObject jugadorEnPlat = Instantiate(JugadorPrefab, creationPos, Quaternion.identity);
    //    BoxCollider2D colJugEnPlat = jugadorEnPlat.GetComponent<BoxCollider2D>();
    //    colJugEnPlat.enabled = true;
    //    //Rigidbody2D rbJugEnPlat = jugadorEnPlat.GetComponent<Rigidbody2D>();
    //    //rbJugEnPlat.constraints = RigidbodyConstraints2D.FreezePositionY;
        
    //    return jugadorEnPlat;
    //}

    void MoverJugadorEnPlataforma(Vector3 targetDir)
    {
        //Mueve al jugador creado al chocar la plataforma hacia el Last Jugador de la plataforma chocada
        Vector3 dir = new Vector3(targetDir.x - transform.position.x,0,0).normalized * platformMovSpeed;  
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetDir.x, transform.position.y,transform.position.z), platformMovSpeed*Time.deltaTime);

        Debug.Log("Current Position: " + transform.position);
        Debug.Log("Target Position: " + new Vector3(targetDir.x, transform.position.y, transform.position.z));
        if(transform.position == new Vector3(targetDir.x, transform.position.y, transform.position.z))
        {
            Debug.Log("Lo hacemos false");
            moverEnPlataforma = false;
            MoverLastJugador(currentLastJugador);
        }
    }

    private void MoverLastJugador(GameObject LastJugador)
    {
        LastJugador.transform.Translate(new Vector3(transform.localScale.x, 0, 0));
        Debug.Log("Punto movido a " + LastJugador.transform.position);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Zona eliminacion")
        {
            Destroy(gameObject);
        }

        if(collision.collider.tag == "Borde")
        {
            transform.position = collision.collider.gameObject.transform.position;
        }

        if(collision.collider.tag == "Plataforma")
        {
            //Add Jugador a lista de jugadores en plataforma
            
            plataformaChocada = collision.collider.gameObject;

            if(plataformaChocada.name == "Plataforma Alta")
            {
                currentListPlat = ListJugPlatAlta;
                currentLastJugador = plataformaChocada.transform.Find("Last Jugador Alta").gameObject;
            }
            else if(plataformaChocada.name == "Plataforma Media")
            {
                currentListPlat = ListJugPlatMedia;
                currentLastJugador = plataformaChocada.transform.Find("Last Jugador Media").gameObject;
            }
            else if(plataformaChocada.name == "Plataforma Baja")
            {
                currentListPlat = ListJugPlatBaja;
                currentLastJugador = plataformaChocada.transform.Find("Last Jugador Baja").gameObject;
            }

            currentListPlat.Add(gameObject);
            
            moverEnPlataforma = true;
        }
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
