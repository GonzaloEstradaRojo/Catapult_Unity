using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Idea: Script para plataformas. Poner un array de jugadores. Acceder al movimiento de los jugadores segun el array y el valor de mover
//enum cada elemento del array. Cuando choca con la plataforma, añade elemento. Mueve mientras mover sea true
// logica futura: al quitar cuerda, pop de jugador
public class Jugador : MonoBehaviour
{

    //public bool Mover = false;
    public float platformMovSpeed = 10f;
    [SerializeField] LayerMask capaPlataforma;
    public GameObject JugadorPrefab;

    private Rigidbody2D rb;
    //private BoxCollider2D coll;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //coll = GetComponent<BoxCollider2D>();
        //coll.enabled = true;
    }

    private void Update()
    {
        //Raycast empezando desde los pies en la esquina izq y derecha
        RaycastHit2D rayCastIzq = Physics2D.Raycast(transform.position - new Vector3(-transform.localScale.x / 2, transform.localScale.y / 2, 0), Vector2.down,0.15f, capaPlataforma);
        RaycastHit2D rayCastDer = Physics2D.Raycast(transform.position - new Vector3(transform.localScale.x / 2, transform.localScale.y / 2, 0), Vector2.down,0.15f, capaPlataforma);
        
        Debug.Log("RayCastIzq " + rayCastIzq);
        Debug.Log("RayCastIzq collider " + rayCastIzq.collider);
        Debug.Log("RayCastDer " + rayCastDer);
        Debug.Log("RayCastDer collider " + rayCastDer.collider);
        Debug.DrawRay( transform.position - new Vector3(-transform.localScale.x / 2, transform.localScale.y / 2, 0), Vector3.down*0.15f, Color.green);
        Debug.DrawRay( transform.position - new Vector3(transform.localScale.x / 2, transform.localScale.y / 2, 0), Vector3.down*0.15f, Color.red);
        
        //Si alguna esquina choca con la plataforma, se crea el nuevo jugador en la plataforma
        if(rayCastIzq == true || rayCastDer == true)
        {
            string plataformaChocada = rayCastIzq ? rayCastIzq.collider.ToString() : rayCastDer.collider.ToString();
            PlataformaColisionada(plataformaChocada);
            Destroy(gameObject);
            Debug.Log("Jugador destruido");
            CrearJugadorEnPlataforma(transform.position);
        }

    }

    void CrearJugadorEnPlataforma(Vector3 position)
    {
        Debug.Log("Jugador creado");
        GameObject jugadorEnPlat = Instantiate(JugadorPrefab, position, Quaternion.identity);

        BoxCollider2D colJugEnPlat = jugadorEnPlat.GetComponent<BoxCollider2D>();
        //Rigidbody2D rbJugEnPlat = jugadorEnPlat.GetComponent<Rigidbody2D>();
        //rbJugEnPlat.constraints = RigidbodyConstraints2D.FreezePositionY;
        colJugEnPlat.enabled = true;

    }

    string PlataformaColisionada(string plataforma)
    {
        //Return "Alta", "Media" o "Baja" dependiendo de en que plataforma haya aterrizado el jugador
        string altura = plataforma.Split(' ')[1];
        Debug.Log("Altura "+ altura);
        return altura;
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


    void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log("collider tag : " + collision.collider.tag);

        if (collision.collider.tag == "Zona eliminacion")
        {
            Destroy(gameObject);
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
