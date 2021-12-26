using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Catapulta : MonoBehaviour
{

    public GameObject catapulta;
    public GameObject JugadorPrefab;

    private GameObject Estructura;
    private GameObject Rotor;
    private GameObject Brazo;
    private GameObject FirePoint;

    Animator rotorAnimator;

    public float velLanzar = 100f;

    // Start is called before the first frame update
    void Start()
    {
        Estructura = catapulta.transform.Find("Estructura Principal").gameObject;
        Rotor = Estructura.transform.Find("Rotor").gameObject;
        Brazo = Rotor.transform.Find("Brazo").gameObject;
        FirePoint = Brazo.transform.Find("FirePoint").gameObject;
        rotorAnimator = Rotor.GetComponent<Animator>();
        Debug.Log("a ");
        
    }



    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Rotor rotation " + Rotor.transform.rotation.eulerAngles.x + "  " + Rotor.transform.rotation.eulerAngles.y + "  " + Rotor.transform.rotation.eulerAngles.z);
        //Debug.Log("rotation " + Rotor.transform.localRotation.eulerAngles.x + "  " + Rotor.transform.localRotation.eulerAngles.y + "  " + Rotor.transform.localRotation.eulerAngles.z);

        //float angle = velLanzar * Time.deltaTime;
        //Debug.Log("Angle " + angle);

        float angulo = 0f;
        float potencia = 0f;

        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Space pressed ");
            rotorAnimator.SetTrigger("LanzarCatapulta");
            //Rotor.gameObject.transform.Rotate(0f, 0f, angle);  // = Quaternion.Euler(new Vector3(Rotor.transform.rotation.x, Rotor.transform.rotation.y,  Rotor.transform.rotation.z + angle));
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A pressed ");
        }
        if (Input.GetKeyDown(KeyCode.S)) 
        {
            Debug.Log("S pressed ");
            LanzarJugador(angulo, potencia);
            //GameObject jugador = Instantiate(JugadorPrefab, FirePoint.transform.position, FirePoint.transform.rotation);
            //Rigidbody2D rbjugador =  jugador.GetComponent<Rigidbody2D>();
            //float asd = jugador.GetComponent<Jugador>().speed;
            //rbjugador.velocity = FirePoint.transform.forward * asd;
            //jugador.GetComponent<Jugador>().Velocidad += new Vector2(Mathf.Cos(45) * jugador.GetComponent<Jugador>().speed, Mathf.Sin(45) * jugador.GetComponent<Jugador>().speed);
        }

    }





    void LanzarJugador(float angulo, float potencia)
    {
        Rigidbody2D rbFP = FirePoint.GetComponent<Rigidbody2D>();
        GameObject Jugador = Instantiate(JugadorPrefab, rbFP.transform.position, rbFP.transform.rotation);
        Rigidbody2D rb = Jugador.GetComponent<Rigidbody2D>();
        rb.AddForce(rbFP.transform.right * 15f, ForceMode2D.Impulse);
    }
}
