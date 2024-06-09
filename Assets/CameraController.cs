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
    public AdvancedDialogue dialogueScript; // Referencia al script de di�logo

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
        // Llamar al m�todo para iniciar el di�logo
        dialogueScript.PauseAnimationAndStartDialogue();
    }

    public void PausarAnimacion()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(0); // Pausa la animaci�n
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
        director.playableGraph.GetRootPlayable(0).SetSpeed(1); // Reanuda la animaci�n
        isPaused = false;
        counter++;
    }

    public void QuitarPausaAnim()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(1); // Reanuda la animaci�n
        isPaused = false;
    }

    // M�todo llamado por un evento en la animaci�n
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

        ReanudarAnimacion(); // Reanuda la animaci�n cuando no hay m�s enemigos
    }

    private IEnumerator WinCondition()
    {
        yield return new WaitForSeconds(5); // Espera 2 segundos

        while (ContarEnemigos() > 0)
        {
            yield return new WaitForSeconds(0.5f); // Vuelve a comprobar cada 0.5 segundos
        }

        InstanciarWinPanel(); // Reanuda la animaci�n cuando no hay m�s enemigos
        Time.timeScale = 0;
    }

    // M�todo para contar los enemigos en la escena
    private int ContarEnemigos()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemy");
        return enemigos.Length;
    }

    // M�todo para instanciar el objeto
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
