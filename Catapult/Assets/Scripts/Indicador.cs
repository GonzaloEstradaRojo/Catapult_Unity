using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicador : MonoBehaviour
{
    public GameObject Indi;
    private GameObject RotorFlecha;
    private Rigidbody2D rbRotorFlecha;

    // Start is called before the first frame update
    void Start()
    {
        RotorFlecha = Indi.transform.Find("Rotor Flecha").gameObject;
        //rbRotorFlecha = RotorFlecha.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        SeguirPuntero();
    }

    void SeguirPuntero()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        mousePos.z = 0;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg - 45;
        if(angle >-45 && angle < 45)
        {
            if (Input.GetKeyDown(KeyCode.D))
                Debug.Log("Angulo " + angle);
            RotorFlecha.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}
