using UnityEngine;

public class FindQuestSpawner : MonoBehaviour
{
    public GameObject boxPrefab;
    private Player player;

    private bool hasSpawned = false;

    public void Update()
    {
        if (QuestManager.Instance.selectedQuest == null ||
            QuestManager.Instance.selectedQuest.questID != 5)
        {
            return; //stop Update()
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

        if (QuestManager.Instance.selectedQuest.questStatus == Quest.QuestStatus.Completed ||
            QuestManager.Instance.selectedQuest.questStatus == Quest.QuestStatus.Failed)
        {
            ClearScene();
            enabled = false;
        }
    }

    public void SpawnObjectives()
    {
        MeshCollider floorCol = GetComponent<MeshCollider>();
        int maxBox = 30;

        for (int i = 0; i < maxBox; i++)
        {
            Vector3 randomSpawnPoint = GetRandomPointOnMesh(floorCol);
            Instantiate(boxPrefab, randomSpawnPoint, Quaternion.identity);
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

            //Debug.DrawRay(origin, Vector3.down * 200f, Color.red, 5f);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity) && hit.collider == collider)
            {
                return hit.point + hit.normal * 0.5f;
            }
        }

        return collider.transform.position;
    }

    private void ClearScene()
    {
        foreach (var box in FindObjectsOfType<Box>())
        {
            Destroy(box.gameObject);
        }

        Debug.Log("ClearScene is running");
    }
}
