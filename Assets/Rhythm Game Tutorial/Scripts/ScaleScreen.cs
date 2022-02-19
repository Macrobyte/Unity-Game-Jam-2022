using UnityEngine;

public class ScaleScreen : MonoBehaviour
{
    public Camera cam;
    public Collider2D worldCol;
    public float _buffer;

    void Start()
    {
        var (center, size) = Calculate0rthoSize();
        cam.transform.position = center;
        cam.orthographicSize = size;
    }

    public (Vector3 center, float size) Calculate0rthoSize()
    {
        var bounds = new Bounds();
        bounds.Encapsulate(worldCol.bounds);

        bounds.Expand(_buffer);

        var vertical = bounds.size.y;
        var horizontal = bounds.size.x * cam.pixelHeight / cam.pixelWidth;

        var size = Mathf.Max(horizontal, vertical) * 0.5f;
        var center = bounds.center + new Vector3(0, 0, -10);

        return (center, size);
    }
}
