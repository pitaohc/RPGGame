using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class VfxAutoController : MonoBehaviour
{
    [SerializeField] private bool autoDestroy = true;
    [SerializeField] private float destroyDelay = 1.0f;
    [Space]
    [SerializeField] private bool applyOffset = true;


    [Header("Random Offset")]
    [SerializeField] private float xMin = -.5f;
    [SerializeField] private float xMax = .5f;
    [SerializeField] private float yMin = -.5f;
    [SerializeField] private float yMax = .5f;



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
        if (!applyOffset) return;

        float randomX = Random.Range(xMin, xMax);
        float randomY = Random.Range(yMin, yMax);
        float randomRotation = Random.Range(0, 360);
        //String log = String.Format("({0:0.0},{1:0.0}), R: {2:0.0}", randomX, randomY, randomRotation);
        //Debug.Log(log);
        transform.position += new Vector3(randomX, randomY, 0);
        transform.Rotate(0, 0, randomRotation);
    }
}
