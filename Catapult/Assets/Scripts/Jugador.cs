using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10f;
    public Vector2 Velocidad;
    Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //speed = 10f;
        Debug.Log("AAA", rb);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //Velocidad.y += (Physics2D.gravity.y/130)*Time.deltaTime;
        //rb.position += Velocidad;
        
    }
}
