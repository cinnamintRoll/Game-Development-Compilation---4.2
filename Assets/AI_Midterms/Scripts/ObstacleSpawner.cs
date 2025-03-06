using Unity.AI.Navigation;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private NavMeshSurface meshSurface; // Reference to the NavMeshSurface
    [SerializeField] private GameObject obstaclePrefab;   // Prefab for the obstacle

    [SerializeField] private int numberOfSpawn;           // Number of obstacles to spawn
    [SerializeField] private Vector2 spawnPositionRange;  // Range for spawn positions (X and Z)
    [SerializeField] private Vector2 spawnSizeRange;     // Range for obstacle sizes (X and Z)
    [SerializeField] private LayerMask floorMask;         // Layer mask for the floor

    private void Start()
    {
        SpawnObstacles();
        meshSurface.BuildNavMesh(); // Rebuild the NavMesh after spawning obstacles
    }

    private void SpawnObstacles()
    {
        int spawnedCount = 0;

        while (spawnedCount < numberOfSpawn)
        {
            Vector3 spawnPosition = GenerateRandomPosition();
            Vector3 spawnSize = GenerateRandomSize();

            if (TryFindValidSpawnPoint(spawnPosition, spawnSize, out Vector3 validPosition))
            {
                SpawnObstacle(validPosition, spawnSize);
                spawnedCount++;
            }
        }
    }

    private Vector3 GenerateRandomPosition()
    {
        float xPos = Random.Range(spawnPositionRange.x, spawnPositionRange.y);
        float zPos = Random.Range(spawnPositionRange.x, spawnPositionRange.y);
        return new Vector3(xPos, transform.position.y, zPos);
    }

    private Vector3 GenerateRandomSize()
    {
        float xSize = Random.Range(spawnSizeRange.x, spawnSizeRange.y);
        float zSize = Random.Range(spawnSizeRange.x, spawnSizeRange.y);
        return new Vector3(xSize, 1, zSize);
    }

    private bool TryFindValidSpawnPoint(Vector3 position, Vector3 size, out Vector3 validPosition)
    {
        Ray ray = new Ray(position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, floorMask))
        {
            float radius = CalculateSphereRadius(size.x, size.z);
            Collider[] colliders = Physics.OverlapSphere(hit.point, radius);

            if (colliders.Length == 1) // Only the floor should be detected
            {
                validPosition = new Vector3(hit.point.x, 0.5f, hit.point.z);
                return true;
            }
        }

        validPosition = Vector3.zero;
        return false;
    }

    private void SpawnObstacle(Vector3 position, Vector3 size)
    {
        GameObject newObstacle = Instantiate(obstaclePrefab, position, Quaternion.identity);
        newObstacle.transform.localScale = size;
    }

    private float CalculateSphereRadius(float xSize, float zSize)
    {
        return Mathf.Max(xSize, zSize); // Return the larger of the two sizes
    }
}