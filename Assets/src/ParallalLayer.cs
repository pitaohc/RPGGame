using UnityEngine;

[System.Serializable]
public class ParallalLayer
{
    [SerializeField] private Transform layerTransform;
    [SerializeField] private float parallaxFactor;

    public void Move(float deltaX)
    {
        layerTransform.position += Vector3.right * (deltaX * parallaxFactor);
    }
}
