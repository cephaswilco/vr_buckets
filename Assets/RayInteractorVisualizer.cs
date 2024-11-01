using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RayInteractorVisualizer : MonoBehaviour
{
    private XRRayInteractor rayInteractor;
    private LineRenderer lineRenderer;

    void Start()
    {
        rayInteractor = GetComponent<XRRayInteractor>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2; // We need two points: origin and target
    }

    void Update()
    {
        if (rayInteractor)
        {
            // Set the start position (controller position)
            lineRenderer.SetPosition(0, rayInteractor.transform.position);

            // Set the end position (pointing direction)
            Vector3 endPosition = rayInteractor.transform.position + rayInteractor.transform.forward * rayInteractor.maxRaycastDistance;
            lineRenderer.SetPosition(1, endPosition);
        }
    }
}