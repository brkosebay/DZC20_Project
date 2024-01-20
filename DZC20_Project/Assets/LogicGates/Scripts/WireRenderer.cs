using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class WireRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetPositions(Vector3 start, Vector3 end)
    {
        lineRenderer.positionCount = 2; // A wire consists of 2 points
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
}
