using UnityEngine;
using UnityEngine.AI;

public class MovePlayer : MonoBehaviour
{
    private NavMeshAgent _agent; // Reference to the NavMeshAgent component
    [SerializeField] private Camera _camera; // Reference to the camera for raycasting

    private void Start()
    {
        InitializeAgent();
    }

    private void Update()
    {
        HandlePlayerMovement();
    }

    private void InitializeAgent()
    {
        _agent = GetComponent<NavMeshAgent>(); // Cache the NavMeshAgent component
    }

    private void HandlePlayerMovement()
    {
        if (Input.GetMouseButtonDown(1)) // Check for right mouse button click
        {
            if (TryGetMousePositionOnNavMesh(out Vector3 targetPosition))
            {
                MoveAgentToPosition(targetPosition);
            }
        }
    }

    private bool TryGetMousePositionOnNavMesh(out Vector3 targetPosition)
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition); // Create a ray from the mouse position
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            targetPosition = new Vector3(hit.point.x, 0, hit.point.z); // Set the target position
            return true;
        }

        targetPosition = Vector3.zero; // Default value if no valid position is found
        return false;
    }

    private void MoveAgentToPosition(Vector3 position)
    {
        _agent.SetDestination(position); // Move the agent to the target position
    }
}