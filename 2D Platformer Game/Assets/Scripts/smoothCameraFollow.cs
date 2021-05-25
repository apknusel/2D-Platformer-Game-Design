using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoothCameraFollow : MonoBehaviour
{
    public Camera mainCamera;
    public Transform target;
    public Vector3 offset;
    [Range(1, 10)]
    public float smoothFactor;
    public Vector3 minValues, maxValue;
    public bool isCurScreenBox;

    Vector2[] screenBoxCorners = new Vector2[4];

    void Update()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, minValues.x, maxValue.x),
            Mathf.Clamp(targetPosition.y, minValues.y, maxValue.y),
            Mathf.Clamp(targetPosition.z, minValues.z, maxValue.z));
        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }

    private void OnDrawGizmos()
    {
        float halfCamHeight = mainCamera.orthographicSize;
        float halfCamWidth = halfCamHeight * mainCamera.aspect;

        Vector2 minSBDimensions = new Vector2(minValues.x - halfCamWidth + transform.position.x,
                                              minValues.y - halfCamHeight + transform.position.y);
        Vector2 maxSBDimensions = new Vector2(maxValue.x + halfCamWidth + transform.position.x,
                                              maxValue.y + halfCamHeight + transform.position.y);

        screenBoxCorners[0] = new Vector2(minSBDimensions.x, minSBDimensions.y);
        screenBoxCorners[1] = new Vector2(minSBDimensions.x, maxSBDimensions.y);
        screenBoxCorners[2] = new Vector2(maxSBDimensions.x, maxSBDimensions.y);
        screenBoxCorners[3] = new Vector2(maxSBDimensions.x, minSBDimensions.y);

        Gizmos.color = isCurScreenBox ? Color.green : Color.red;

        for (int i = 0; i < screenBoxCorners.Length; i++)
        {
            int nextPos = (i + 1) % 4;
            Gizmos.DrawLine(screenBoxCorners[i], screenBoxCorners[nextPos]);
        }
    }
}
