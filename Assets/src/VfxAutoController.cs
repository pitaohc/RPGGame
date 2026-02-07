using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class VfxAutoController : MonoBehaviour
{
    [SerializeField] private bool autoDestroy = true;
    [SerializeField] private float destroyDelay = 1.0f;
    [Space]
    [SerializeField] private bool randomPosition = true;
    [SerializeField] private bool randomRotation = true;


    [Header("Random Position")]
    [SerializeField] private float xMin = -.5f;
    [SerializeField] private float xMax = .5f;
    [SerializeField] private float yMin = -.5f;
    [SerializeField] private float yMax = .5f;
    [Header("Random Rotation")]
    [SerializeField] private float minRotation = 0;
    [SerializeField] private float maxRotation = 360;



    private void Start()
    {
        ApplyOffset();
        if (autoDestroy)
        {
            Destroy(gameObject, destroyDelay);
        }
    }

    private void ApplyOffset()
    {
        RandomPosition();
        RandomRotation();
    }

    private void RandomRotation()
    {
        if (!this.randomRotation) return;
        float randomAngle = Random.Range(minRotation, maxRotation);
        transform.Rotate(0, 0, randomAngle);
    }

    private void RandomPosition()
    {
        if (!randomPosition) return;
        float randomX = Random.Range(xMin, xMax);
        float randomY = Random.Range(yMin, yMax);
        transform.position += new Vector3(randomX, randomY, 0);
    }
}
