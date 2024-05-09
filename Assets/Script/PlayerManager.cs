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
    public float bullet_damage;

    private Animator animator;//
    private bool isPaused = false;//

    //camera
    public Camera camaraPrincipal; // Referencia a la c�mara principal
    public Transform mira; // Transform de la mira

    // Variables para el disparo
    public float distanciaDisparo = 10f; // Distancia m�xima del disparo
    public LineRenderer lineaDisparo; // Referencia al LineRenderer para la l�nea de disparo

    private void Start()
    {
        instance = this;
        animator = GetComponent<Animator>();//
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

        if (isPaused)
        {
            // Aqu� colocar�as la l�gica para verificar la condici�n de reanudaci�n
            // Por ejemplo, si la condici�n se cumple, llamas a ReanudarAnimacion()
        }
    }

    public void PausarAnimacion()
    {
        animator.speed = 0f; // Pausa la animaci�n estableciendo su velocidad a cero
        isPaused = true;
    }

    public void ReanudarAnimacion()
    {
        animator.speed = 1f; // Reanuda la animaci�n estableciendo su velocidad a 1
        isPaused = false;
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
        // Obtener la posici�n del clic del mouse en la pantalla
        Vector3 posicionMousePantalla = Input.mousePosition;

        // Convertir la posici�n del clic del mouse a coordenadas del mundo
        Vector3 posicionMouseMundo = camaraPrincipal.ScreenToWorldPoint(new Vector3(posicionMousePantalla.x, posicionMousePantalla.y, camaraPrincipal.transform.position.y));

        // Crear un Raycast desde la posici�n del clic del mouse
        RaycastHit hit;
        if (Physics.Raycast(posicionMouseMundo, camaraPrincipal.transform.forward, out hit, distanciaDisparo, LayerMask.GetMask("Enemy")))
        {
            // Si el Raycast colisiona con algo que tenga el tag "Damage", mostrar la l�nea de disparo
            lineaDisparo.SetPosition(0, posicionMouseMundo);
            lineaDisparo.SetPosition(1, hit.point);
            lineaDisparo.enabled = true;
            Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
            enemy.GetDamage();
            Debug.Log("Me electrocutaste");
        }
        else
        {
            // Si el Raycast no colisiona con nada, mostrar solo la primera parte de la l�nea
            Debug.Log("Vacio");
            lineaDisparo.SetPosition(0, posicionMouseMundo);
            lineaDisparo.SetPosition(1, posicionMouseMundo + camaraPrincipal.transform.forward * distanciaDisparo);
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
