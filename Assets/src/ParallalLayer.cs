using UnityEngine;

[System.Serializable]
public class ParallalLayer
{
    [SerializeField] private Transform layerTransform;
    [SerializeField] private float parallaxFactor;
    [SerializeField] private float safetyWidth = 0.0f;

    private float imageFullWidth;
    private float imageHalfWidth;

    public void Move(float deltaX)
    {
        layerTransform.position += Vector3.right * (deltaX * parallaxFactor);
    }

    public void LoopBackground(float cameraLeftEdge, float cameraRightEdge)
    {
        float imageLeftEdge = layerTransform.position.x - imageHalfWidth;
        float imageRightEdge = layerTransform.position.x + imageHalfWidth;
        if (imageRightEdge < cameraLeftEdge + safetyWidth)
        {
            layerTransform.position += Vector3.right * imageFullWidth;
        }
        else if (imageLeftEdge > cameraRightEdge - safetyWidth)
        {
            layerTransform.position += Vector3.left * imageFullWidth;
        }
    }

    public void CalculateImageWidth()
    {
        imageFullWidth = layerTransform.GetComponent<SpriteRenderer>().bounds.size.x;
        imageHalfWidth = imageFullWidth / 2f;
        //Debug.Log($"Image Width for {layerTransform.name}: {imageFullWidth}");
    }
}
