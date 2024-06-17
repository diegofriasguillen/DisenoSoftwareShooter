using UnityEngine;
using UnityEngine.Playables;
using System.Collections;

public class CameraControler : MonoBehaviour
{
    private PlayableDirector director;
    private bool isPaused = false;
    int counter;
    public GameObject medicine; // Referencia al objeto que quieres instanciar
    public GameObject medicine2; // Referencia al objeto que quieres instanciar
    public GameObject medicine3; // Referencia al objeto que quieres instanciar
    public GameObject medicine4; // Referencia al objeto que quieres instanciar
    public GameObject medicine5; // Referencia al objeto que quieres instanciar
    public GameObject winPanel; // Referencia al objeto que quieres instanciar
    public AdvancedDialogue dialogueScript; // Referencia al script de diálogo
    public AudioSource caminata;
    public AudioSource boss;


    private void Start()
    {
        director = GetComponent<PlayableDirector>();
    }

    private void Update()
    {
        if (isPaused && Input.GetKeyDown(KeyCode.R))
        {
            ReanudarAnimacion();
        }
    }

    public void PausaInicial()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
        isPaused = true;
    }
    public void PausaBoss()
    {
        // Llamar al método para iniciar el diálogo
        dialogueScript.PauseAnimationAndStartDialogue();
        boss.Play();

    }

    public void PausarAnimacion()
    {
        caminata.Pause();
        director.playableGraph.GetRootPlayable(0).SetSpeed(0); // Pausa la animación
        isPaused = true;
        switch (counter)
        {
            case 0:
                InstanciarObjeto();
                break;
            case 1:
                InstanciarObjeto2();
                break;
            case 2:
                InstanciarObjeto3();
                break;
            case 3:
                InstanciarObjeto4();
                break;
            case 4:
                InstanciarObjeto5();
                break;
            default:
                InstanciarObjeto5();
                break;
        }

        
    }

    public void ReanudarAnimacion()
    {
        caminata.Play();
        director.playableGraph.GetRootPlayable(0).SetSpeed(1); // Reanuda la animación
        isPaused = false;
        counter++;
    }

    public void QuitarPausaAnim()
    {
        caminata.Play();
        director.playableGraph.GetRootPlayable(0).SetSpeed(1); // Reanuda la animación
        isPaused = false;
    }

    // Método llamado por un evento en la animación
    public void EnPuntoDePausa()
    {
        if (!isPaused)
        {
            PausarAnimacion();
            StartCoroutine(EsperarYReanudar());
        }
    }

    private IEnumerator EsperarYReanudar()
    {
        yield return new WaitForSeconds(5); // Espera 2 segundos

        while (ContarEnemigos() > 0)
        {
            yield return new WaitForSeconds(0.5f); // Vuelve a comprobar cada 0.5 segundos
        }

        ReanudarAnimacion(); // Reanuda la animación cuando no hay más enemigos
    }

    private IEnumerator WinCondition()
    {
        yield return new WaitForSeconds(5); // Espera 2 segundos

        while (ContarEnemigos() > 0)
        {
            yield return new WaitForSeconds(0.5f); // Vuelve a comprobar cada 0.5 segundos
        }

        InstanciarWinPanel(); // Reanuda la animación cuando no hay más enemigos
        Time.timeScale = 0;
    }

    public void CompleteLevel1()
    {
        // Guardar que el nivel 1 ha sido completado
        PlayerPrefs.SetInt("Level1Completed", 1);
        PlayerPrefs.Save();
        // Puedes agregar cualquier otra lógica aquí, como cargar una nueva escena
    }

    // Método para contar los enemigos en la escena
    private int ContarEnemigos()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemy");
        return enemigos.Length;
    }

    // Método para instanciar el objeto
    private void InstanciarObjeto()
    {
        medicine.SetActive(true);
    }

    private void InstanciarObjeto2()
    {
        medicine2.SetActive(true);
    }

    private void InstanciarObjeto3()
    {
        medicine3.SetActive(true);
    }

    private void InstanciarObjeto4()
    {
        medicine4.SetActive(true);
    }

    private void InstanciarObjeto5()
    {
        medicine5.SetActive(true);
    }

    private void InstanciarWinPanel()
    {
        winPanel.SetActive(true);
    }
}
