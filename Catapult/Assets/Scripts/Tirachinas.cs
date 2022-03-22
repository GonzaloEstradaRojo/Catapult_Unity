using UnityEngine;

public class Tirachinas : MonoBehaviour
{
    public GameObject tirachinas;
    public GameObject JugadorPrefab;
    public Indicador indicadorScript;
    public GameObject FlechaIndicador;


    private bool lanzarTirachinas = false;
    private bool recargarTirachinas = true;

    private GameObject Cubo;
    private GameObject FirePoint;
    private Animator tirachinasAnimator;

    private bool cargarTirachinas = false;
    private float potenciaLanzamiento = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cubo = tirachinas.transform.Find("Cubo").gameObject;
        FirePoint = Cubo.transform.Find("FirePoint").gameObject;

        print(FlechaIndicador.transform.position);
        tirachinasAnimator = tirachinas.GetComponent<Animator>();
    }

    void Update()
    {
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    //Debug.Log("Mouse clicked");
        //}
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //lanzarTirachinas = true;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            //Debug.Log("Mouse release");
            potenciaLanzamiento = indicadorScript.potencia;
            tirachinasAnimator.ResetTrigger("RecargarTirachinas");
            tirachinasAnimator.SetTrigger("LanzarTirachinas");
            recargarTirachinas = true;
        }

        if (Input.GetKeyDown("space"))
        {
            GameObject Jugador = Instantiate(JugadorPrefab, new Vector3(8, 6, 0), Quaternion.identity);
            //catapultaAnimator.SetTrigger("LanzarTirachinas");
            //Rotor.gameObject.transform.Rotate(0f, 0f, angle);  // = Quaternion.Euler(new Vector3(Rotor.transform.rotation.x, Rotor.transform.rotation.y,  Rotor.transform.rotation.z + angle));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            potenciaLanzamiento = indicadorScript.potencia;
            LanzarJugador();
        }

        FirePoint.transform.rotation = FlechaIndicador.transform.rotation;
        if (recargarTirachinas)
        {
            tirachinasAnimator.SetTrigger("RecargarTirachinas");
            tirachinasAnimator.ResetTrigger("LanzarTirachinas");
            recargarTirachinas = false;
        }
        //if (lanzarTirachinas)
        //{
        //    tirachinasAnimator.SetTrigger("LanzarTirachinas");
        //    tirachinasAnimator.ResetTrigger("LanzarTirachinas");
        //    lanzarTirachinas = false;
        //    recargarTirachinas = true;
        //}
    }

    void LanzarJugador()
    {
        Rigidbody2D rbFP = FirePoint.GetComponent<Rigidbody2D>();
        GameObject Jugador = Instantiate(JugadorPrefab, rbFP.transform.position, Quaternion.identity/* rbFP.transform.rotation*/);
        Rigidbody2D rb = Jugador.GetComponent<Rigidbody2D>();
        rb.AddForce(rbFP.transform.up * indicadorScript.Map(potenciaLanzamiento, 0, 100, 10, 20), ForceMode2D.Impulse);
    }
}
