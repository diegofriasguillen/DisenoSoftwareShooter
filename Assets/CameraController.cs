using UnityEngine;
using UnityEngine.Playables;

public class ControlAnimacion : MonoBehaviour
{
    private PlayableDirector director;
    private bool isPaused = false;


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
        director.playableGraph.GetRootPlayable(0).SetSpeed(0); // Pausa la animaci�n
        isPaused = true;
    }

    public void ReanudarAnimacion()
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
        }
    }
}


