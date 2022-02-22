using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuerdasRenderer : MonoBehaviour
{

    private GameObject Torre;
    private GameObject ZonaChoque1;
    LineRenderer lineRenderer;
    Vector3 ZonaChoque1Pos;
    private float speedLanzaCuerda = 10f;
    public bool lanzarCuerdaBool = false;
    GameObject posicionFinal = null;
    public bool estaEnganchado = false;



    private void Start()
    {
        Torre = GameObject.Find("Torre");
        ZonaChoque1 = Torre.transform.Find("Zona de Choque 1").gameObject;
        posicionFinal = transform.Find("Posicion Final").gameObject;

        lineRenderer = gameObject.GetComponent<LineRenderer>();
        //GameObject ZonaChoque1 = Torre.GetComponentInChildren<>  ("Zona de Choque 1");
        lineRenderer.widthMultiplier = 0.3f; 
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
        lineRenderer.positionCount = 2;
    }



    private void Update()
    {
        ZonaChoque1Pos = ZonaChoque1.transform.position;

        //if (lanzarCuerdaBool)
        //    CrearCuerda();

        //if (Input.GetKeyDown("f"))
        //{
        //    StartCoroutine(CrearCuerda1());
        //}

        //print(estaEnganchado + " enganchado");
        if (estaEnganchado)
        {
            posicionFinal.transform.position = ZonaChoque1Pos;
            lineRenderer.SetPosition(1, posicionFinal.transform.position);
        }
    }

    //public void CrearCuerda()
    //{
    //    posicionFinal.transform.position = Vector3.Lerp(posicionFinal.transform.position, ZonaChoque1Pos, speedLanzaCuerda * 10 * Time.deltaTime);
    //    lineRenderer.SetPosition(0, transform.position);
    //    lineRenderer.SetPosition(1, posicionFinal.transform.position);
    //}

    public IEnumerator CrearCuerda1() {
        //posicionFinal.transform.position = Vector3.Lerp();
        while (Vector3.Distance(posicionFinal.transform.position, ZonaChoque1Pos) > 0.05f)
        {
            posicionFinal.transform.position = Vector3.MoveTowards(posicionFinal.transform.position, ZonaChoque1Pos, speedLanzaCuerda * Time.deltaTime);
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, posicionFinal.transform.position);
            yield return null;

        }
        estaEnganchado = true;
    }


    public void DestruirCuerda()
    {       
        estaEnganchado = false;
        lanzarCuerdaBool = false;
        posicionFinal.transform.position = transform.position;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, posicionFinal.transform.position);
    }
}