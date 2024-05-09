using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Enemy enemy;


    public static PlayerManager instance;
    public int vida;
    public int damage;
    public int municion = 30;
    public int cargador=35;
    public GameObject bullet;
    public Transform mouse_position;
    public float bullet_damage;

    private Animator animator;//
    private bool isPaused = false;//

    //camera
    public Camera camaraPrincipal; // Referencia a la cámara principal
    public Transform mira; // Transform de la mira

    // Variables para el disparo
    public float distanciaDisparo = 10f; // Distancia máxima del disparo
    public LineRenderer lineaDisparo; // Referencia al LineRenderer para la línea de disparo

    private void Start()
    {
        instance = this;
        animator = GetComponent<Animator>();//
    }

    private void Update()
    {
        // Mover la mira
        MoverMira();

        // Disparar con el botón izquierdo del mouse
        if (Input.GetButtonDown("Fire1"))
        {
            Disparar();
        }

        if (isPaused)
        {
            // Aquí colocarías la lógica para verificar la condición de reanudación
            // Por ejemplo, si la condición se cumple, llamas a ReanudarAnimacion()
        }
    }

    public void PausarAnimacion()
    {
        animator.speed = 0f; // Pausa la animación estableciendo su velocidad a cero
        isPaused = true;
    }

    public void ReanudarAnimacion()
    {
        animator.speed = 1f; // Reanuda la animación estableciendo su velocidad a 1
        isPaused = false;
    }

    void MoverMira()
    {
        // Obtener la posición del mouse en la pantalla
        Vector3 posicionMousePantalla = Input.mousePosition;

        // Convertir la posición del mouse a coordenadas del mundo
        Vector3 posicionMouseMundo = camaraPrincipal.ScreenToWorldPoint(posicionMousePantalla);

        // Ajustar la posición horizontal de la mira a la del mouse
        mira.position = new Vector3(posicionMouseMundo.x, mira.position.y, mira.position.z);
    }

    // Función para disparar
    void Disparar()
    {
        // Obtener la posición del clic del mouse en la pantalla
        Vector3 posicionMousePantalla = Input.mousePosition;

        // Convertir la posición del clic del mouse a coordenadas del mundo
        Vector3 posicionMouseMundo = camaraPrincipal.ScreenToWorldPoint(new Vector3(posicionMousePantalla.x, posicionMousePantalla.y, camaraPrincipal.transform.position.y));

        // Crear un Raycast desde la posición del clic del mouse
        RaycastHit hit;
        if (Physics.Raycast(posicionMouseMundo, camaraPrincipal.transform.forward, out hit, distanciaDisparo, LayerMask.GetMask("Enemy")))
        {
            // Si el Raycast colisiona con algo que tenga el tag "Damage", mostrar la línea de disparo
            enemy.GetDamage();
            lineaDisparo.SetPosition(0, posicionMouseMundo);
            lineaDisparo.SetPosition(1, hit.point);
            lineaDisparo.enabled = true;

            // Puedes agregar aquí lo que quieres que suceda al impactar, como dañar enemigos, etc.
        }
        else
        {
            // Si el Raycast no colisiona con nada, mostrar solo la primera parte de la línea
            lineaDisparo.SetPosition(0, posicionMouseMundo);
            lineaDisparo.SetPosition(1, posicionMouseMundo + camaraPrincipal.transform.forward * distanciaDisparo);
            lineaDisparo.enabled = true;
        }

        // Desactivar la línea de disparo después de un cierto tiempo
        Invoke("DesactivarLineaDisparo", 0.1f);

        Debug.Log("disparando");
    }




    // Función para desactivar la línea de disparo
    void DesactivarLineaDisparo()
    {
        lineaDisparo.enabled = false;
    }


}
