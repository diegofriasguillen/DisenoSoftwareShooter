using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AdvancedDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public GameObject buttonPrefab; // Prefab del bot�n siguiente
    public GameObject customButtonPrefab; // Prefab del bot�n custom
    public Transform buttonContainer; // Contenedor del bot�n en la interfaz
    public GameObject objectToDeactivate; // GameObject que se desactivar� al final del di�logo
    public CameraControler animationController; // Referencia al script de control de animaci�n
    public GameObject dialoguePanel; // Panel del di�logo que se activar�

    private int index;
    private bool isTyping = false;
    private bool isLineComplete = false;
    private GameObject nextButton;
    private GameObject closeButton;

    void Start()
    {
        textComponent.text = string.Empty;
        dialoguePanel.SetActive(false); // Asegurarse de que el panel de di�logo est� desactivado al inicio
    }

    void Update()
    {
        // Eliminamos la necesidad de revisar una tecla en el m�todo Update
    }

    public void PauseAnimationAndStartDialogue()
    {
        // Pausar la animaci�n
        animationController.PausaInicial();

        // Activar el panel de di�logo
        dialoguePanel.SetActive(true);

        // Iniciar el di�logo
        if (lines.Length > 0)
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        isLineComplete = false;
        textComponent.text = string.Empty;

        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
        isLineComplete = true;
        if (index < lines.Length - 1)
        {
            InstantiateNextButton(); // Instanciar el bot�n de siguiente si no es el �ltimo di�logo
        }
        else
        {
            InstantiateCloseButton(); // Instanciar el bot�n de cerrar si es el �ltimo di�logo
        }
    }

    void InstantiateNextButton()
    {
        if (nextButton == null)
        {
            nextButton = Instantiate(buttonPrefab, buttonContainer);
            nextButton.GetComponentInChildren<TextMeshProUGUI>().text = "Next"; // Configurar el texto del bot�n
            nextButton.GetComponent<Button>().onClick.AddListener(NextLine); // Agregar listener al bot�n
        }
    }

    void InstantiateCloseButton()
    {
        if (closeButton == null)
        {
            closeButton = Instantiate(customButtonPrefab, buttonContainer);
            closeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Close"; // Configurar el texto del bot�n
            closeButton.GetComponent<Button>().onClick.AddListener(CloseDialogue); // Agregar listener al bot�n para cerrar
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            Destroy(nextButton); // Destruir el bot�n actual
            StartCoroutine(TypeLine());
        }
    }

    void CloseDialogue()
    {
        // Desactivar el GameObject especificado
        objectToDeactivate.SetActive(false);
        // Reanudar la animaci�n
        animationController.QuitarPausaAnim();
        // Destruir el bot�n de cerrar
        Destroy(closeButton);
        // Limpiar el texto del componente TextMeshProUGUI
        textComponent.text = string.Empty;
        // Desactivar el panel de di�logo
        dialoguePanel.SetActive(false);
    }
}
