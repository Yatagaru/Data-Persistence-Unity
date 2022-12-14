using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindSingleton : MonoBehaviour
{
    private SceneController instance;
    void Start()
    {
        instance = FindObjectOfType<SceneController>();
        Debug.Log(instance);
        instance.AssignButtonFunction();
    }

    void Update()
    {
        
    }
}
