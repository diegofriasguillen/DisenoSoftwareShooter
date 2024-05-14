using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public TextMeshProUGUI Tvida;
    public int vida;
    public int damage;
    public int municion = 30;
    public int cargador=35;
    public float bullet_damage = 10f;

    private Animator animator;//
    private bool isPaused = false;//

    //camera
    public Camera camaraPrincipal; // Referencia a la c�mara principal
    public Transform mira; // Transform de la mira

    // Variables para el disparo
    public float distanciaDisparo = 20f; // Distancia m�xima del disparo
    public LineRenderer lineaDisparo; // Referencia al LineRenderer para la l�nea de disparo

    private void Start()
    {
        vida = 100;
        instance = this;
        animator = GetComponent<Animator>();//
      
        
            Time.timeScale = 1f;
        
    }

    private void Update()
    {
        // Mover la mira
        MoverMira();
        Tvida.text = "Vida: " + vida.ToString();

        if (Input.GetButtonDown("Fire1"))
        {
            Disparar();
        }

        if (isPaused)
        {

        }

        if (vida <=0 )
        {
            SceneManager.LoadScene("MainMenu");
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
        Vector3 posicionMousePantalla = Input.mousePosition;

        Vector3 posicionMouseMundo = camaraPrincipal.ScreenToWorldPoint(new Vector3(posicionMousePantalla.x, posicionMousePantalla.y, camaraPrincipal.transform.position.y));

        RaycastHit hit;
        if (Physics.Raycast(posicionMouseMundo, camaraPrincipal.transform.forward, out hit, distanciaDisparo, LayerMask.GetMask("Enemy")))
        {
            lineaDisparo.SetPosition(0, posicionMouseMundo);
            lineaDisparo.SetPosition(1, hit.point);
            lineaDisparo.enabled = true;
            Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
            enemy.GetDamage(bullet_damage); 
        }
        else
        {
            Debug.Log("Vacio");
            lineaDisparo.SetPosition(0, posicionMouseMundo);
            lineaDisparo.SetPosition(1, posicionMouseMundo + camaraPrincipal.transform.forward * distanciaDisparo);
            lineaDisparo.enabled = true;
        }

        Invoke("DesactivarLineaDisparo", 0.1f);

        Debug.Log("disparando");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            vida -= other.GetComponent<Bullet>().Damage;
        }
    }

    public void AddHealth(int heal)
    {
        vida += heal;
        if (vida > 100)
        {
            vida = 100;
        }
    }

    void DesactivarLineaDisparo()
    {
        lineaDisparo.enabled = false;
    }
}
