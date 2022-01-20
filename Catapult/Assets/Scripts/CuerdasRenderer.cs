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
    private bool lanzarCuerdaBool = false;
    GameObject posicionFinal = null;



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
        Debug.Log("Posicion inicial " + transform.position);
        lineRenderer.positionCount = 2;
    }

    private void Update()
    {
        ZonaChoque1Pos = ZonaChoque1.transform.position;
        Debug.Log("Pos Zona Choque " + ZonaChoque1Pos);
        if (Input.GetKeyDown(KeyCode.W))
            lanzarCuerdaBool = true;

        if (lanzarCuerdaBool)
        {

            //lineRenderer.SetPosition(0, transform.position);
            //lineRenderer.SetPosition(1, ZonaChoque1Pos);

            posicionFinal.transform.position = Vector3.MoveTowards(transform.position, ZonaChoque1Pos, speedLanzaCuerda * 10 * Time.deltaTime);
            //lineRenderer.SetPosition(0, transform.position);
            //lineRenderer.SetPosition(1, posicionFinal.transform.position);

            //CrearCuerda();
        }

        if(posicionFinal.transform.position == ZonaChoque1Pos)
        {
            lanzarCuerdaBool = false;
        }
    }

    private void CrearCuerda()
    {

    }
}
