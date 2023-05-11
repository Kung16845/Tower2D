using UnityEngine;
using UnityEngine.UI;

public class ObjectToggle : MonoBehaviour
{
    public Button toggleButton;
    public GameObject targetObject;
    public Sprite activeSprite;
    public Sprite inactiveSprite;
    private Image buttonImage;

    void Start()
    {
        // Get a reference to the button's Image component
        buttonImage = toggleButton.GetComponent<Image>();

        // Add the ToggleObject method as a listener to the button's OnClick event
        toggleButton.onClick.AddListener(ToggleObject);
    }

    // Toggle the target GameObject's active state and change the button's sprite accordingly
    public void ToggleObject()
    {
        targetObject.SetActive(!targetObject.activeSelf);

        // Change the button's sprite based on the new active state of the target object
        if (targetObject.activeSelf)
        {
            buttonImage.sprite = activeSprite;
        }
        else
        {
            buttonImage.sprite = inactiveSprite;
        }
    }
}
