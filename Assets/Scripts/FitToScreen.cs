using UnityEngine;

public class FitToScreen : MonoBehaviour
{
    private void Start()
    {
        Resize();
    }

    private void Resize()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer found on the GameObject.");
            return;
        }

        float cameraHeight = Camera.main.orthographicSize * 2;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        Vector2 newScale = transform.localScale;
        newScale.x = cameraWidth / spriteSize.x;
        newScale.y = cameraHeight / spriteSize.y;

        transform.localScale = newScale;
    }
}
