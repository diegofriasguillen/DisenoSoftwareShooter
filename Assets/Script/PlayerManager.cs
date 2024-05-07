using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public int vida;
    public int damage;
    public int municion = 30;
    public int cargador=35;
    public GameObject bullet;
    public Transform mouse_position;
    public float bullet_damage;

    //camera
    public Camera camaraPrincipal; // Referencia a la c�mara principal
    public Transform mira; // Transform de la mira

    // Variables para el disparo
    public float distanciaDisparo = 10f; // Distancia m�xima del disparo
    public LineRenderer lineaDisparo; // Referencia al LineRenderer para la l�nea de disparo

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        // Mover la mira
        MoverMira();

        // Disparar con el bot�n izquierdo del mouse
        if (Input.GetButtonDown("Fire1"))
        {
            Disparar();
        }
    }

    void MoverMira()
    {
        // Obtener la posici�n del mouse en la pantalla
        Vector3 posicionMousePantalla = Input.mousePosition;

        // Convertir la posici�n del mouse a coordenadas del mundo
        Vector3 posicionMouseMundo = camaraPrincipal.ScreenToWorldPoint(posicionMousePantalla);

        // Ajustar la posici�n horizontal de la mira a la del mouse
        mira.position = new Vector3(posicionMouseMundo.x, mira.position.y, mira.position.z);
    }

    // Funci�n para disparar
    void Disparar()
    {
        // Crear un Raycast desde la mira
        RaycastHit hit;
        if (Physics.Raycast(mira.position, mira.forward, out hit, distanciaDisparo))
        {
            // Si el Raycast colisiona con algo, mostrar la l�nea de disparo
            lineaDisparo.SetPosition(0, mira.position);
            lineaDisparo.SetPosition(1, hit.point);
            lineaDisparo.enabled = true;

            // Puedes agregar aqu� lo que quieres que suceda al impactar, como da�ar enemigos, etc.
        }
        else
        {
            // Si el Raycast no colisiona con nada, mostrar solo la primera parte de la l�nea
            lineaDisparo.SetPosition(0, mira.position);
            lineaDisparo.SetPosition(1, mira.position + mira.forward * distanciaDisparo);
            lineaDisparo.enabled = true;
        }

        // Desactivar la l�nea de disparo despu�s de un cierto tiempo
        Invoke("DesactivarLineaDisparo", 0.1f);

        Debug.Log("disparando");
    }

    // Funci�n para desactivar la l�nea de disparo
    void DesactivarLineaDisparo()
    {
        lineaDisparo.enabled = false;
    }


}
