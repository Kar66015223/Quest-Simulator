using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public class CollectQuestSpawner : MonoBehaviour, IObjectiveSpawner
{
    public CollectQuest collectQuest;
    public GameObject coinPrefab;
    private Player player;

    private bool hasSpawned = false;

    public void Update()
    {
        enabled = false;

        if (QuestManager.Instance.selectedQuest != null)
        {
            if (QuestManager.Instance.selectedQuest.questID == 2)
            {
                enabled = true;
            } 
        }

        if (player == null)
        {
            player = FindFirstObjectByType<Player>();
        }

        if (player != null && !hasSpawned)
        {
            SpawnObjectives();
            hasSpawned = true;
        }

        if (QuestManager.Instance.selectedQuest == null)
        {
            ClearScene();
        }
    }

    public void SpawnObjectives()
    {
        MeshCollider floorCol = GetComponent<MeshCollider>();
        int maxCoins = 30;

        for (int i = 0; i < maxCoins; i++)
        {
            Vector3 randomSpawnPoint = GetRandomPointOnMesh(floorCol);
            Instantiate(coinPrefab, randomSpawnPoint, Quaternion.identity); 
        }
    }

    private Vector3 GetRandomPointOnMesh(MeshCollider collider)
    {
        Bounds bounds = collider.bounds;
        for (int attempts = 0; attempts < 20; attempts++)
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float z = Random.Range(bounds.min.z, bounds.max.z);
            float rayHeight = 100f;
            Vector3 origin = new Vector3(x, rayHeight, z); // shoot ray from above
            Ray ray = new Ray(origin, Vector3.down);

            Debug.DrawRay(origin, Vector3.down * 200f, Color.red, 5f);

            if (collider.Raycast(ray, out RaycastHit hit, Mathf.Infinity) && hit.collider == collider)
            {
                return hit.point + hit.normal * 1;
            }
        }

        return collider.transform.position;
    }

    private void ClearScene()
    {
        foreach (var coin in FindObjectsOfType<Coin>())
        {
            Destroy(coin.gameObject);
        }
    }
}
