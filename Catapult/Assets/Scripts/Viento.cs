using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Viento : MonoBehaviour
{

    private AreaEffector2D areaEffector;
    private int maxForceViento = 100;
    private int velVientoPrevia = 0;
    private int velViento = 0;

    public Slider slider;

    void Start()
    {
        areaEffector = GetComponent<AreaEffector2D>();
        slider.maxValue = maxForceViento;
        slider.minValue = -maxForceViento;
        StartCoroutine("SetRandomViento");
    }

    // Update is called once per frame
    void Update()
    {
        //areaEffector.forceMagnitude = Random.Range(-maxForceViento, maxForceViento);
            SetVientoSlider(velViento);
    }

    private IEnumerator SetRandomViento()
    {

        for (; ; )
        {
            velVientoPrevia = velViento;
            velViento = Random.Range(-maxForceViento, maxForceViento);
            int tiempoEspera = Random.Range(3, 6);
            print("tiempoEspera " + tiempoEspera);
            print("velViento " + velViento);

            yield return new WaitForSeconds(tiempoEspera);
        }

    }

    public void SetVientoSlider(float velViento)
    {
        print("abs " + Mathf.Abs(velViento - velVientoPrevia));
        for (int i = 0; i < Mathf.Abs(velViento - velVientoPrevia); i++)
        {
            if (velViento <= velVientoPrevia)
            {
                print("slider alante");
                slider.value = velViento - i;
            }
            else
            {
                print("slider atras");
                slider.value = velViento + i;
            }

        }
        areaEffector.forceMagnitude = velViento;

    }
}
