using UnityEngine;

public class MovableObstacle : MonoBehaviour
{
    [SerializeField] private float _speedForward; // Speed of the obstacle movement
    private bool _isMovingForward = true; // Tracks the current movement direction

    private const float MaxZPosition = 2f; // Maximum Z position for direction switch
    private const float MinZPosition = -2f; // Minimum Z position for direction switch

    private void Update()
    {
        MoveObstacle();
        CheckAndSwitchDirection();
    }

    private void MoveObstacle()
    {
        Vector3 movementDirection = _isMovingForward ? Vector3.forward : Vector3.back;
        transform.Translate(movementDirection * _speedForward * Time.deltaTime);
    }

    private void CheckAndSwitchDirection()
    {
        if (transform.position.z >= MaxZPosition)
        {
            _isMovingForward = false; // Switch to moving backward
        }
        else if (transform.position.z <= MinZPosition)
        {
            _isMovingForward = true; // Switch to moving forward
        }
    }
}