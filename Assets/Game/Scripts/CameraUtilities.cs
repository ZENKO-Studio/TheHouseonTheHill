using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUtilities : MonoBehaviour
{

    [Header("Raycast Settings")]
    [SerializeField] private static float raycastDistance = 100f;
    [SerializeField] private static Vector2 screenArea = new Vector2(0.4f, 0.4f); // Increased area to 40% for better capture

    public static bool IsObjectInViewAndWithinArea(Camera camera, GameObject obj, float requiredVisiblePercentage = 0.4f)
    {
        // Get the object's renderer component
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer == null)
        {
            return false;
        }

        // Get the object's bounds
        Bounds bounds = renderer.bounds;

        // Get the camera's view frustum planes
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);

        // Test if the object's bounds intersect with the view frustum planes
        if (!GeometryUtility.TestPlanesAABB(planes, bounds))
        {
            return false;
        }

        // Check if the object's bounding box intersects with the specified screen area
        Vector3[] screenPoints = new Vector3[8];
        screenPoints[0] = camera.WorldToScreenPoint(bounds.min);
        screenPoints[1] = camera.WorldToScreenPoint(new Vector3(bounds.min.x, bounds.min.y, bounds.max.z));
        screenPoints[2] = camera.WorldToScreenPoint(new Vector3(bounds.min.x, bounds.max.y, bounds.min.z));
        screenPoints[3] = camera.WorldToScreenPoint(new Vector3(bounds.min.x, bounds.max.y, bounds.max.z));
        screenPoints[4] = camera.WorldToScreenPoint(bounds.max);
        screenPoints[5] = camera.WorldToScreenPoint(new Vector3(bounds.max.x, bounds.min.y, bounds.min.z));
        screenPoints[6] = camera.WorldToScreenPoint(new Vector3(bounds.max.x, bounds.min.y, bounds.max.z));
        screenPoints[7] = camera.WorldToScreenPoint(new Vector3(bounds.max.x, bounds.max.y, bounds.min.z));

        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Vector2 screenBounds = screenArea * new Vector2(Screen.width, Screen.height);
        Rect screenRect = new Rect(screenCenter.x - screenBounds.x / 2, screenCenter.y - screenBounds.y / 2, screenBounds.x, screenBounds.y);

        int pointsInArea = 0;
        foreach (var point in screenPoints)
        {
            if (screenRect.Contains(point))
            {
                pointsInArea++;
            }
        }

        // Calculate the visible percentage of the object
        float visiblePercentage = (float)pointsInArea / screenPoints.Length;
        return visiblePercentage >= requiredVisiblePercentage;
    }

    private void OnDrawGizmos()
    {
        // Draw the screen area as a rectangle
        if (Camera.main != null)
        {
            Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
            Vector2 screenBounds = screenArea * new Vector2(Screen.width, Screen.height);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(Camera.main.ScreenToWorldPoint(new Vector3(screenCenter.x - screenBounds.x / 2, screenCenter.y - screenBounds.y / 2, Camera.main.nearClipPlane)),
                            Camera.main.ScreenToWorldPoint(new Vector3(screenCenter.x + screenBounds.x / 2, screenCenter.y - screenBounds.y / 2, Camera.main.nearClipPlane)));
            Gizmos.DrawLine(Camera.main.ScreenToWorldPoint(new Vector3(screenCenter.x + screenBounds.x / 2, screenCenter.y - screenBounds.y / 2, Camera.main.nearClipPlane)),
                            Camera.main.ScreenToWorldPoint(new Vector3(screenCenter.x + screenBounds.x / 2, screenCenter.y + screenBounds.y / 2, Camera.main.nearClipPlane)));
            Gizmos.DrawLine(Camera.main.ScreenToWorldPoint(new Vector3(screenCenter.x + screenBounds.x / 2, screenCenter.y + screenBounds.y / 2, Camera.main.nearClipPlane)),
                            Camera.main.ScreenToWorldPoint(new Vector3(screenCenter.x - screenBounds.x / 2, screenCenter.y + screenBounds.y / 2, Camera.main.nearClipPlane)));
            Gizmos.DrawLine(Camera.main.ScreenToWorldPoint(new Vector3(screenCenter.x - screenBounds.x / 2, screenCenter.y + screenBounds.y / 2, Camera.main.nearClipPlane)),
                            Camera.main.ScreenToWorldPoint(new Vector3(screenCenter.x - screenBounds.x / 2, screenCenter.y - screenBounds.y / 2, Camera.main.nearClipPlane)));
        }
    }

}
