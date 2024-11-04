using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RayInteractor : MonoBehaviour
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

            // Calculate the end position (pointing direction)
            Vector3 endPosition = rayInteractor.transform.position + rayInteractor.transform.forward * rayInteractor.maxRaycastDistance;
            lineRenderer.SetPosition(1, endPosition);
            Debug.Log("Here0");
            // Perform raycast
            RaycastHit hit;
            if (Physics.Raycast(rayInteractor.transform.position, rayInteractor.transform.forward, out hit, rayInteractor.maxRaycastDistance))
            {
                if (hit.collider != null)
                {
                    Debug.Log("Here1");
                    // Check if the hit object is interactable
                    XRGrabInteractable interactable = hit.collider.GetComponent<XRGrabInteractable>();
                    if (interactable != null)
                    {
                        Debug.Log("Here2");
                        // Optionally highlight or trigger the interactable
                        // Example: interactable.OnSelectEntered(); // Uncomment if needed
                    }
                }
            }
        }
    }
}