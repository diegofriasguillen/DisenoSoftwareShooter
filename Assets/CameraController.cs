using UnityEngine;
using UnityEngine.Playables;
using System.Collections;

public class ControlAnimacion : MonoBehaviour
{
    private PlayableDirector director;
    private bool isPaused = false;
    public GameObject medicine; // Referencia al objeto que quieres instanciar
   

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

    public void PausarAnimacion()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(0); // Pausa la animación
        isPaused = true;
        InstanciarObjeto();
    }

    public void ReanudarAnimacion()
    {
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
        
        yield return new WaitForSeconds(2); // Espera 2 segundos

        while (ContarEnemigos() > 0)
        {
            yield return new WaitForSeconds(0.5f); // Vuelve a comprobar cada 0.5 segundos
        }

        ReanudarAnimacion(); // Reanuda la animación cuando no hay más enemigos
    }

    // Método para contar los enemigos en la escena
    private int ContarEnemigos()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemy");
        return enemigos.Length;
    }

    // Método para manejar la colisión con un trigger
    

    // Método para instanciar el objeto
    private void InstanciarObjeto()
    {
        medicine.SetActive(true);
    }
}


