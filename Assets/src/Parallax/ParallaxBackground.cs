using System;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private ParallalLayer[] parallaxLayers;

    private Camera mainCamera;
    private float lastCameraPositionX;
    private float cameraHalfWidth;
    private float cameraFullWidth;

    private void Awake()
    {
        mainCamera = Camera.main;
        lastCameraPositionX = mainCamera.transform.position.x;

        cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;
        cameraFullWidth = cameraHalfWidth * 2f;

        CalculateImageWidth();
    }

    // Move is called once per frame
    private void FixedUpdate()
    {
        //Debug.Log($"FC: {Time.frameCount}, dT: {Time.deltaTime}");
        float cameraPositionX = mainCamera.transform.position.x;
        float deltaX = cameraPositionX - lastCameraPositionX;
        //Debug.Log($"{Time.frameCount} Delta X: {deltaX}， cur: {cameraPositionX}, last: {lastCameraPositionX}");
        lastCameraPositionX = cameraPositionX;
        float cameraLeftEdge = cameraPositionX - cameraHalfWidth;
        float cameraRightEdge = cameraPositionX + cameraHalfWidth;
        foreach (var layer in parallaxLayers)
        {
            layer.Move(deltaX);
            layer.LoopBackground(cameraLeftEdge, cameraRightEdge);
        }
    }

    private void CalculateImageWidth()
    {
        foreach (var layer in parallaxLayers)
        {
            layer.InitializeLayers();
        }
    }
}
