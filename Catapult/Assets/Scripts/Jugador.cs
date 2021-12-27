using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10f;
    public Vector2 Velocidad;

    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        //speed = 10f;
    }

    // Update is called once per frame
    private void Update()
    {
        //Velocidad.y += (Physics2D.gravity.y/130)*Time.deltaTime;
        //rb.position += Velocidad;
        
    }

    public void OnBecameInvisible()
    {
        Debug.Log("Destruido ");
        Destroy(gameObject);
    }
}
