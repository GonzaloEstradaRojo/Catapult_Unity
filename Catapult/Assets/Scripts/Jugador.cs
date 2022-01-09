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

    public float platformMovSpeed = 1f;
    public GameObject JugadorPrefab;
    [SerializeField] LayerMask capaPlataforma;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Raycast empezando desde los pies en la esquina izq y derecha
        RaycastHit2D rayCastDer = Physics2D.Raycast(transform.position - new Vector3(transform.localScale.x / 2, transform.localScale.y / 2, 0), Vector2.down,0.15f, capaPlataforma);
        RaycastHit2D rayCastIzq = Physics2D.Raycast(transform.position - new Vector3(-transform.localScale.x / 2, transform.localScale.y / 2, 0), Vector2.down,0.15f, capaPlataforma);
        
        Debug.DrawRay( transform.position - new Vector3(transform.localScale.x / 2, transform.localScale.y / 2, 0), Vector3.down*0.15f, Color.red);
        Debug.DrawRay( transform.position - new Vector3(-transform.localScale.x / 2, transform.localScale.y / 2, 0), Vector3.down*0.15f, Color.green);
        
        //Si alguna esquina choca con la plataforma, se crea el nuevo jugador en la plataforma
        if(rayCastIzq || rayCastDer)
        {
            Debug.Log("RayCastDer collider " + rayCastDer.collider);
            Debug.Log("RayCastIzq collider " + rayCastIzq.collider);
            GameObject plataformaChocada = rayCastIzq ? rayCastIzq.collider.gameObject : rayCastDer.collider.gameObject;
            GameObject lastJug = plataformaChocada.transform.Find("Last Jugador " + PlataformaColisionada(plataformaChocada.name)).gameObject;

            Destroy(gameObject);
            Debug.Log("Jugador destruido");
            GameObject newJugador = CrearJugadorEnPlataforma(transform.position);
            MoverJugadorEnPlataforma(newJugador, lastJug.transform.position);
        }
    }

    GameObject CrearJugadorEnPlataforma(Vector3 creationPos)
    {
        Debug.Log("Jugador creado");
        GameObject jugadorEnPlat = Instantiate(JugadorPrefab, creationPos, Quaternion.identity);
        BoxCollider2D colJugEnPlat = jugadorEnPlat.GetComponent<BoxCollider2D>();
        colJugEnPlat.enabled = true;
        //Rigidbody2D rbJugEnPlat = jugadorEnPlat.GetComponent<Rigidbody2D>();
        //rbJugEnPlat.constraints = RigidbodyConstraints2D.FreezePositionY;
        
        return jugadorEnPlat;
    }

    void MoverJugadorEnPlataforma(GameObject jugador, Vector3 targetDir)
    {
        //Mueve al jugador creado al chocar la plataforma hacia el Last Jugador de la plataforma chocada
        Vector3 dir = new Vector3(targetDir.x - transform.position.x,0,0).normalized * platformMovSpeed;
        Debug.Log("Vector dirección de movimiento: " + dir);
        jugador.transform.Translate(dir * Time.deltaTime);
    }

    string PlataformaColisionada(string plataforma)
    {
        //Devuelve "Alta", "Media" o "Baja" dependiendo de en que plataforma haya aterrizado el jugador
        string altura = plataforma.Split(' ')[1];
        Debug.Log("Plataforma " + altura);
        return altura;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Zona eliminacion")
        {
            Destroy(gameObject);
        }
    }



    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (isInPlataforma && (collision.tag == "Borde" || collision.tag == "Jugador"))
    //    {
    //        Mover = false;
    //    }
    //}

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.collider.tag == "Plataforma")
    //    {
    //        if(seguirMoviendo && isInPlataforma)
    //        {
    //            Debug.Log("Sigue moviendote");
    //            MovimientoPlataforma();
    //        }

    //    }
    //}



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
