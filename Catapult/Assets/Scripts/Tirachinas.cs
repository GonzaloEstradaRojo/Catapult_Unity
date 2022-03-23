using UnityEngine;

public class Tirachinas : MonoBehaviour
{
    public GameObject tirachinas;
    public GameObject JugadorPrefab;
    public Indicador indicadorScript;
    public GameObject FlechaIndicador;

    private GameObject Cubo;
    private GameObject FirePoint;
    private Animator tirachinasAnimator;

    private float potenciaLanzamiento = 0f;

    void Start()
    {
        Cubo = tirachinas.transform.Find("Cubo").gameObject;
        FirePoint = Cubo.transform.Find("FirePoint").gameObject;

        print(FlechaIndicador.transform.position);
        tirachinasAnimator = tirachinas.GetComponent<Animator>();
    }

    void Update()
    {

        if (Input.GetButtonUp("Fire1"))
        {
            potenciaLanzamiento = indicadorScript.potencia;
            tirachinasAnimator.SetTrigger("LanzarTirachinas");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject Jugador = Instantiate(JugadorPrefab, new Vector3(8, 6, 0), Quaternion.identity);
            //Rotor.gameObject.transform.Rotate(0f, 0f, angle);  // = Quaternion.Euler(new Vector3(Rotor.transform.rotation.x, Rotor.transform.rotation.y,  Rotor.transform.rotation.z + angle));
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject Jugador = Instantiate(JugadorPrefab, new Vector3(-9, 6, 0), Quaternion.identity);
            Jugador.transform.Rotate(new Vector3(0, 180, 0));
            //Rotor.gameObject.transform.Rotate(0f, 0f, angle);  // = Quaternion.Euler(new Vector3(Rotor.transform.rotation.x, Rotor.transform.rotation.y,  Rotor.transform.rotation.z + angle));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            potenciaLanzamiento = indicadorScript.potencia;
            LanzarJugador();
        }

        FirePoint.transform.rotation = FlechaIndicador.transform.rotation;

        if(tirachinasAnimator.GetCurrentAnimatorStateInfo(0).IsName("IDLE"))
        {
            tirachinasAnimator.ResetTrigger("LanzarTirachinas");
        }

    }

    void LanzarJugador()
    {
        Rigidbody2D rbFP = FirePoint.GetComponent<Rigidbody2D>();
        GameObject Jugador = Instantiate(JugadorPrefab, rbFP.transform.position, Quaternion.identity/* rbFP.transform.rotation*/);
        Rigidbody2D rb = Jugador.GetComponent<Rigidbody2D>();
        rb.AddForce(rbFP.transform.up * indicadorScript.Map(potenciaLanzamiento, 0, 100, 10, 20), ForceMode2D.Impulse);
    }
}
