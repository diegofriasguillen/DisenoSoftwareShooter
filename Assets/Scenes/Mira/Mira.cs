using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Texture2D cursorTexture; // Textura que se usará como cursor personalizado
    public GameObject imageToFollowCursor; // Imagen que seguirá al cursor

    void Start()
    {
        // Establecer la textura personalizada como cursor
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {
        // Obtener la posición del mouse en el mundo
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // Asegurarse de que el objeto siga en el plano Z correcto

        // Mover la imagen para que siga al cursor
        if (imageToFollowCursor != null)
        {
            imageToFollowCursor.transform.position = mousePos;
        }
    }
}
