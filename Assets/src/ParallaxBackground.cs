using System;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private ParallalLayer[] parallaxLayers;

    private Camera mainCamera;
    private float lastCameraPositionX;

    private void Awake()
    {
        mainCamera = Camera.main;
        lastCameraPositionX = mainCamera.transform.position.x;
    }

    // Move is called once per frame
    void FixedUpdate()
    {
        float cameraPositionX = mainCamera.transform.position.x;
        float deltaX = cameraPositionX - lastCameraPositionX;
        lastCameraPositionX = cameraPositionX;

        foreach (var layer in parallaxLayers)
        {
            layer.Move(deltaX);
        }
    }
}
