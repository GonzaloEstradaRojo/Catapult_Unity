using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Indicador : MonoBehaviour
{
    public GameObject Indicator;
    public GameObject FirePoint;
    public Slider slider;

    private GameObject RotorFlecha;
    //private Rigidbody2D rbRotorFlecha;
    private Rigidbody2D rbFP;
    private float potencia;
    public Text text;



    // Start is called before the first frame update
    void Start()
    {
        RotorFlecha = Indicator.transform.Find("Rotor Flecha").gameObject;
        rbFP = FirePoint.GetComponent<Rigidbody2D>();
        //rbRotorFlecha = RotorFlecha.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SeguirPuntero();
        GirarFP();

        calculaAnguloPotencia();
    }

    void GirarFP() {
        rbFP.transform.rotation = RotorFlecha.transform.rotation;
        rbFP.transform.Rotate(new Vector3(0, 0, 45));
        //rbFP.transform.rotation = Quaternion.Euler(new Vector3(RotorFlecha.transform.localRotation.x, RotorFlecha.transform.localRotation.y, RotorFlecha.transform.localRotation.z));
    }


    public float Map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }


    void SeguirPuntero()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        mousePos.z = 0;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg - 45;
        if(angle >= -20 && angle <= 75)
        {
            if (Input.GetKeyDown(KeyCode.D))
                Debug.Log("Angulo " + angle);
            RotorFlecha.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        } 
        //else if (angle > 45)
        //{
        //    if (Input.GetKeyDown(KeyCode.D))
        //        Debug.Log("Angulo " + angle);
        //    RotorFlecha.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45));
        //}
        //else if (angle < -45)
        //{
        //    if (Input.GetKeyDown(KeyCode.D))
        //        Debug.Log("Angulo " + angle);
        //    RotorFlecha.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -45));
        //}
    }

    public void SetPotencia(float potencia)
    {
        Debug.Log("Fija potencia a " + potencia);
        slider.value = potencia;
    }

    #region Funcion para setear el maximo de un slider
    //public void SetMaxPotencia(int maxPotencia)
    //{
    //    slider.maxValue = maxPotencia;
    //    slider.value = maxPotencia;
    //}
    #endregion

    void calculaAnguloPotencia()
    {
        Debug.Log("Calculos: ");
        //Debug.Log("Transform position " + transform.position);
        Vector3 positionRaton = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        //Debug.Log("Camera " + Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z)));
        //Debug.Log("Diff " + (positionRaton - transform.position));
        float distancia = Vector3.Distance(transform.position, positionRaton);
        //Debug.Log("Position Raton " + positionRaton);
        //Debug.Log("Position transform " + transform.position);

        //Debug.Log("Distancia " + distancia);
        //Debug.Log("Distancia X " + (positionRaton.x - transform.position.x));
        //Debug.Log("Distancia Y " + (positionRaton.y - transform.position.y));

        float porcentajePotencia;
        if(positionRaton.y > transform.position.y){

            if (/*positionRaton.x > transform.position.x*/true)
            {
                if (distancia < 10)
                {
                    porcentajePotencia = Map(distancia, 0, 10, 0, 100);
                    text.text = porcentajePotencia.ToString();
                    SetPotencia(porcentajePotencia);
                }
                else
                {
                    porcentajePotencia = 100;
                    text.text = porcentajePotencia.ToString();
                    SetPotencia(porcentajePotencia);
                }
            }   
        }
    }
}
