using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Indicador : MonoBehaviour
{
    public GameObject Indicator;
    public GameObject FirePoint;
    public Slider slider;
    public float potencia = 0f;
    public float angulo = 0f;

    private GameObject RotorFlecha;
    private Rigidbody2D rbFP;
    private Vector3 mousePosInit;

    void Start()
    {
        RotorFlecha = Indicator.transform.Find("Rotor Flecha").gameObject;
        rbFP = FirePoint.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        SeguirPunteroConFlecha();
        GirarFirePoint();
        calculaPotencia();
    }

    private void OnMouseDown()
    {
        mousePosInit = Input.mousePosition;
    }

    void GirarFirePoint() {        
        //Gira el Fire Point para que apunte en la misma direccion que la flecha del indicador

        rbFP.transform.rotation = RotorFlecha.transform.rotation;
        rbFP.transform.Rotate(new Vector3(0, 0, 45));
    }


    public float Map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }


    void SeguirPunteroConFlecha()
    {
        float angle = CalculaAngulo();
        RotorFlecha.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); 
    }

    float CalculaAngulo()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        mousePos.z = 0;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg - 45;
        if (angle < -20)
            angle = -20;
        else if (angle > 48)
            angle = 48;
        angulo = angle;
        return angle;
    }



    public void SetPotencia(float potencia)
    {
        slider.value = potencia;
    }

    #region Funcion para setear el maximo de un slider
    //public void SetMaxPotencia(int maxPotencia)
    //{
    //    slider.maxValue = maxPotencia;
    //    slider.value = maxPotencia;
    //}
    #endregion

    void calculaPotencia()
    {
        Vector2 positionRaton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distancia = Vector3.Distance(transform.position, positionRaton);

        if(positionRaton.y > transform.position.y){
            if (distancia < 10)
            {
                potencia = Mathf.Round(Map(distancia, 0, 10, 0, 100));
                SetPotencia(potencia);
            }
            else
            {
                potencia = 100;
                SetPotencia(potencia);
            }               
        }
    }

}
