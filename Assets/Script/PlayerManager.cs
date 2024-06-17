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
    public int cargador = 35;
    public float bullet_damage = 10f;
    public GameObject gameOverPanel;
    public GameObject vidaText;
    public GameObject pauseButton;
    public AudioSource caminata;
    public AudioSource disparo;

    private Animator animator;
    public bool isPaused = false;

    //camera
    public Camera camaraPrincipal; // Referencia a la cámara principal
    public Transform mira; // Transform de la mira

    // Variables para el disparo
    public float distanciaDisparo = 20f; // Distancia máxima del disparo
    public LineRenderer lineaDisparo; // Referencia al LineRenderer para la línea de disparo

    private void Start()
    {
        vida = 100;
        instance = this;
        animator = GetComponent<Animator>();

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

        if (vida <= 0)
        {
            gameOverPanel.SetActive(true);
            vidaText.SetActive(false);
            pauseButton.SetActive(false);
            Time.timeScale = 0;
        }
    }

    public void PausarAnimacion()
    {
        animator.speed = 0f; // Pausa la animación estableciendo su velocidad a cero
        isPaused = true;
        //caminata.Pause();

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
        Vector3 posicionMouseMundo = camaraPrincipal.ScreenToWorldPoint(new Vector3(posicionMousePantalla.x, posicionMousePantalla.y, camaraPrincipal.nearClipPlane));

        // Ajustar la posición de la mira a la del mouse
        mira.position = new Vector3(posicionMouseMundo.x, posicionMouseMundo.y, mira.position.z);
    }

    // Función para disparar
    void Disparar()
    {
        // Obtener la posición del mouse en la pantalla
        Vector3 posicionMousePantalla = Input.mousePosition;

        // Convertir la posición del mouse a coordenadas del mundo
        Vector3 posicionMouseMundo = camaraPrincipal.ScreenToWorldPoint(new Vector3(posicionMousePantalla.x, posicionMousePantalla.y, camaraPrincipal.nearClipPlane));

        // Calcular la dirección del disparo
        Vector3 direccionDisparo = (posicionMouseMundo - camaraPrincipal.transform.position).normalized;

        RaycastHit hit;
        if (Physics.Raycast(camaraPrincipal.transform.position, direccionDisparo, out hit, distanciaDisparo, LayerMask.GetMask("Enemy")))
        {
            lineaDisparo.SetPosition(0, camaraPrincipal.transform.position);
            lineaDisparo.SetPosition(1, hit.point);
            lineaDisparo.enabled = true;
            Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.GetDamage(bullet_damage);
            }
            disparo.Play();

        }
        else
        {
            Debug.Log("Vacio");
            lineaDisparo.SetPosition(0, camaraPrincipal.transform.position);
            lineaDisparo.SetPosition(1, camaraPrincipal.transform.position + direccionDisparo * distanciaDisparo);
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
