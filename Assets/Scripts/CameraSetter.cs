using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetter : MonoBehaviour
{

    Canvas canvas;

    void Awake()
    {
        canvas = GetComponent<Canvas>();
    }


    public void SetCanvasCamera(Camera camera)
    {
        canvas.worldCamera = camera;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
