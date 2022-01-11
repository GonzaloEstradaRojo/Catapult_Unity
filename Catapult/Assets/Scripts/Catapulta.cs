using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Catapulta : MonoBehaviour
{

    public GameObject catapulta;
    public GameObject JugadorPrefab;
    public Indicador indicador;
    public bool canShoot;

    private GameObject Estructura;
    private GameObject Rotor;
    private GameObject Brazo;
    private GameObject FirePoint;
    private Animator catapultaAnimator;


    private float potenciaLanzamiento = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Estructura = catapulta.transform.Find("Estructura Principal").gameObject;
        Rotor = Estructura.transform.Find("Rotor").gameObject;
        Brazo = Rotor.transform.Find("Brazo").gameObject;
        FirePoint = Brazo.transform.Find("FirePoint").gameObject;
        catapultaAnimator = catapulta.GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            //Debug.Log("Mouse clicked");
        }
        if (Input.GetButtonUp("Fire1"))
        {
            //Debug.Log("Mouse release");
            potenciaLanzamiento = indicador.potencia;
            catapultaAnimator.SetTrigger("LanzarCatapulta");
        }

        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Space pressed ");
            catapultaAnimator.SetTrigger("LanzarCatapulta");
            //Rotor.gameObject.transform.Rotate(0f, 0f, angle);  // = Quaternion.Euler(new Vector3(Rotor.transform.rotation.x, Rotor.transform.rotation.y,  Rotor.transform.rotation.z + angle));
        }
        if (Input.GetKeyDown(KeyCode.S)) 
        {
            Debug.Log("S pressed ");
            potenciaLanzamiento = indicador.potencia;
            LanzarJugador();
        }
    }

    void LanzarJugador()
    {
        Rigidbody2D rbFP = FirePoint.GetComponent<Rigidbody2D>();
        GameObject Jugador = Instantiate(JugadorPrefab, rbFP.transform.position, Quaternion.identity/* rbFP.transform.rotation*/);
        Rigidbody2D rb = Jugador.GetComponent<Rigidbody2D>();
        rb.AddForce(rbFP.transform.right * indicador.Map(potenciaLanzamiento, 0,100,10 ,20), ForceMode2D.Impulse);
    }
}
