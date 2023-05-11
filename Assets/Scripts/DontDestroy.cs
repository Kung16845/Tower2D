using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{   

    public string objectID;
    private void Awake()
    {
        objectID = name + transform.position.ToString() + transform.eulerAngles.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Object.FindObjectsOfType<DontDestroy>().Length;i++)
        {
            if (Object.FindObjectsOfType<DontDestroy>()[i] != this)
            {
                    if(Object.FindObjectsOfType<DontDestroy>()[i].name == gameObject.name)
                {
                    Destroy(gameObject);    
                }
            }
        }

        if (transform.parent == null) // Check if this GameObject is a root GameObject
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("DontDestroyOnLoad only works for root GameObjects or components on root GameObjects.");
        }
    }

}
