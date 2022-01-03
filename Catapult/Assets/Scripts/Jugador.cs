using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Idea: Script para plataformas. Poner un array de jugadores. Acceder al movimiento de los jugadores segun el array y el valor de mover
//enum cada elemento del array. Cuando choca con la plataforma, añade elemento. Mueve mientras mover sea true
// logica futura: al quitar cuerda, pop de jugador
public class Jugador : MonoBehaviour
{

    private bool isInPlataforma = false;
    public bool Mover = false;
    private Rigidbody2D rb;
    public float platformMovSpeed = 10f;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Debug.Log("Mover : " + Mover);
        if (Mover)
        {
            MovimientoEnPlataforma();
        }
        else
        {
            Debug.Log("No se mueve");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isInPlataforma && (collision.tag == "Borde" || collision.tag == "Jugador"))
        {
            Mover = false;
        }
    }

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

        Debug.Log("collider tag : "+ collision.collider.tag);

        if(collision.collider.tag == "Zona eliminacion")
        {
            Destroy(gameObject);
        }else if(collision.collider.tag == "Plataforma")
        {
            isInPlataforma = true;
            Mover = true;
        }
        else if (collision.collider.tag == "Suelo")
        {
            Mover = false;
        }
        //if (isInPlataforma && (collision.collider.tag is "Borde" or "Player"))
        //{
        //    Mover = false;
        //}
    }



    void MovimientoEnPlataforma()
    {
        Vector3 m_Input = new Vector3(-1, 0, 0);
        //rb.MovePosition(transform.position + m_Input * Time.deltaTime);
        transform.Translate(m_Input * Time.deltaTime);


        //rb.AddForce(new Vector2(- platformMovSpeed, 0));
        //if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        //    rb.velocity = new Vector2(platformMovSpeed, 0);
        //else
        //    rb.velocity = new Vector2(0, 0);

        //Debug.Log("Empieza el movimiento "+ collision.collider.name);
        //Debug.Log("Empieza el movimiento 2"+ collision.collider.transform.right + m_Input * Time.deltaTime);

        ////Apply the movement vector to the current position, which is
        ////multiplied by deltaTime and speed for a smooth MovePosition

        //rb.AddForce(collision.collider.transform.right + m_Input * Time.deltaTime, ForceMode2D.Impulse);
    }





}
