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
    public GameObject buttonPrefab; // Prefab del botón siguiente
    public GameObject customButtonPrefab; // Prefab del botón custom
    public Transform buttonContainer; // Contenedor del botón en la interfaz
    public GameObject objectToDeactivate; // GameObject que se desactivará al final del diálogo
    public CameraControler animationController; // Referencia al script de control de animación
    public GameObject dialoguePanel; // Panel del diálogo que se activará

    private int index;
    private bool isTyping = false;
    private bool isLineComplete = false;
    private GameObject nextButton;
    private GameObject closeButton;

    void Start()
    {
        textComponent.text = string.Empty;
        dialoguePanel.SetActive(false); // Asegurarse de que el panel de diálogo esté desactivado al inicio
    }

    void Update()
    {
        // Eliminamos la necesidad de revisar una tecla en el método Update
    }

    public void PauseAnimationAndStartDialogue()
    {
        // Pausar la animación
        animationController.PausaInicial();

        // Activar el panel de diálogo
        dialoguePanel.SetActive(true);

        // Iniciar el diálogo
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
            InstantiateNextButton(); // Instanciar el botón de siguiente si no es el último diálogo
        }
        else
        {
            InstantiateCloseButton(); // Instanciar el botón de cerrar si es el último diálogo
        }
    }

    void InstantiateNextButton()
    {
        if (nextButton == null)
        {
            nextButton = Instantiate(buttonPrefab, buttonContainer);
            nextButton.GetComponentInChildren<TextMeshProUGUI>().text = "Next"; // Configurar el texto del botón
            nextButton.GetComponent<Button>().onClick.AddListener(NextLine); // Agregar listener al botón
        }
    }

    void InstantiateCloseButton()
    {
        if (closeButton == null)
        {
            closeButton = Instantiate(customButtonPrefab, buttonContainer);
            closeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Close"; // Configurar el texto del botón
            closeButton.GetComponent<Button>().onClick.AddListener(CloseDialogue); // Agregar listener al botón para cerrar
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            Destroy(nextButton); // Destruir el botón actual
            StartCoroutine(TypeLine());
        }
    }

    void CloseDialogue()
    {
        // Desactivar el GameObject especificado
        objectToDeactivate.SetActive(false);
        // Reanudar la animación
        animationController.QuitarPausaAnim();
        // Destruir el botón de cerrar
        Destroy(closeButton);
        // Limpiar el texto del componente TextMeshProUGUI
        textComponent.text = string.Empty;
        // Desactivar el panel de diálogo
        dialoguePanel.SetActive(false);
    }
}
