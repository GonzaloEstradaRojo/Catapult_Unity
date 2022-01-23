using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plataforma : MonoBehaviour
{

    public GameObject LastJugador;
    public CuerdasRenderer cuerda;

    private int NumLastJug = 1;
    private float platMovSpeed = 2f;
    private GameObject currentJugador = null;
    private Rigidbody2D rbCurrentJugador = null;
    private Jugador currentJugadorScript = null;
    public List<GameObject> ListJugadores = new List<GameObject>();
    private bool MoverJugador = false;
    private bool EliminacionJugador = false;
    private bool DestruirPrimerJugador = false;



    private void Update()
    {
        if (currentJugadorScript)
            MoverJugador = currentJugadorScript.moverEnPlataforma;

        if(MoverJugador)
            MoverJugadorEnPlataforma(currentJugador);

        if (EliminacionJugador)
            QuitarPrimerJugador();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(ListJugadores.Count > 0)
                EliminacionJugador = true;
        }

        CheckJugadorEnLista();
        
        //if (Input.GetKeyDown(KeyCode.W))
        //    lanzarCuerdaBool = true;

        //if (Input.GetKeyDown(KeyCode.E))
        //    DestruyeCuerda();
        

        //if (lanzarCuerdaBool)
        //{
        //    Debug.Log("Cuerda: " + PorcentajeCuerda);
        //    Debug.Log("Velocidad Cuerda: " + velocidadLanzamientoCuerda);
        //    Debug.Log("Velocidad Total: " + velocidadLanzamientoCuerda * Time.deltaTime);
            
        //    PorcentajeCuerda += velocidadLanzamientoCuerda * Time.deltaTime;

        //    LanzaCuerda(PorcentajeCuerda);

        //}

    }

    void MoverTodosJugadores()
    {
        int lenLista = ListJugadores.Count;
        GameObject primerJugador = ListJugadores[0];
        Vector3 posPrimerJugador = primerJugador.transform.position;
        primerJugador.GetComponent<SpriteRenderer>().enabled = false;
        for (var i = 0; i< lenLista-1; i++)
            ListJugadores[i+1].transform.position = Vector3.MoveTowards(ListJugadores[i+1].transform.position, ListJugadores[i].transform.position, platMovSpeed * Time.deltaTime);

        if(ListJugadores.Count > 1)
        {
            if (primerJugador.transform.position == ListJugadores[1].transform.position)
            {
                DestruirPrimerJugador = true;
                cuerda.lanzarCuerdaBool = true;
            }
        }
        else if(ListJugadores.Count == 1)
            DestruirPrimerJugador = true;
    }

    void CambiaNombresJugEnPlataformas()
    {
        int lenLista = ListJugadores.Count;
        if(lenLista > 1)
        {
            for (var i = lenLista-1; i>=1; i--)
                ListJugadores[i].name = ListJugadores[i-1].name;

        }
    }

    void QuitarPrimerJugador()
    {
        cuerda.DestruirCuerda();
        GameObject primerJugador = ListJugadores[0];
        MoverTodosJugadores();
        if (DestruirPrimerJugador)
        {
            CambiaNombresJugEnPlataformas();
            ListJugadores.RemoveAt(0);
            Destroy(primerJugador);
            EliminacionJugador = false;
            DestruirPrimerJugador = false;
            MoverLastJugador(false);
        }
    }


    void MoverJugadorEnPlataforma(GameObject Jugador)
    {
        Vector3 targetDir = LastJugador.transform.position; //Posicion a la que queremos mover el jugador
        Jugador.transform.name = "Jugador " + transform.name + " " + NumLastJug; //Cambiamos el nombre del jugador
        Jugador.transform.position = Vector3.MoveTowards(Jugador.transform.position, new Vector3(targetDir.x, Jugador.transform.position.y, 0), platMovSpeed * Time.deltaTime);

        //Comprobamos si el jugador ya ha llegado a la posicion del Last Jugador
        if (Jugador.transform.position == new Vector3(targetDir.x, Jugador.transform.position.y, 0))
        {
            if(ListJugadores.Count == 0)
            {
                cuerda.lanzarCuerdaBool = true;
            }
            ListJugadores.Add(Jugador); //Añade el jugador a la lista de jugadores de la plataforma
            MoverLastJugador(true); // Mueve de posicion el Last Jugador
        }
    }

    private void MoverLastJugador(bool Aumentar)
    {
        int dirMovimiento = Aumentar ? 1 : -1;
        currentJugadorScript.moverEnPlataforma = false; //El current jugador se para de mover
        LastJugador.transform.Translate(new Vector3(dirMovimiento*(currentJugador.transform.localScale.x + 0.1f), 0, 0));
        if (Aumentar)
            NumLastJug += 1;
        else
            NumLastJug -= 1;
    }

    void OnCollisionEnter2D(Collision2D collision)
    { 
        currentJugador = collision.collider.gameObject;
        rbCurrentJugador = currentJugador.GetComponent<Rigidbody2D>();
        rbCurrentJugador.velocity = Vector2.zero;
        currentJugador.transform.position = new Vector3(currentJugador.transform.position.x, LastJugador.transform.position.y, 0);
        currentJugadorScript = currentJugador.GetComponent<Jugador>();
        currentJugadorScript.moverEnPlataforma = true;        
    }

    private bool CheckJugadorEnLista()
    {
        return ListJugadores.Count > 0;
    }


    //public void LanzaCuerda(float porcentaje)
    //{
    //    sliderCuerda.value = porcentaje ;

    //    if (sliderCuerda.value >= sliderCuerda.maxValue)
    //        lanzarCuerdaBool = false;
    //}

    //public void DestruyeCuerda()
    //{
    //    PorcentajeCuerda = 0f;
    //    LanzaCuerda(PorcentajeCuerda);
    //}

    #region Funcion para setear el maximo de un slider
    //public void SetMaxPotencia(int maxPotencia)
    //{
    //    slider.maxValue = maxPotencia;
    //    slider.value = maxPotencia;
    //}
    #endregion


}
