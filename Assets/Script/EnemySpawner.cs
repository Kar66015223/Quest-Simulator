using UnityEngine;
using System.Collections;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public int maxEnemies = 10;

    [Header("Spawn Area")]
    public Vector3 spawnAreaSize = new Vector3(20f, 0f, 20f);

    [Header("Wave Settings")]
    public float waveTime = 90f;   // เวลาในแต่ละ Wave (วินาที)
    public float timeRemaining { get; private set; }   // เวลาคงเหลือ

    public bool isWaveActive = false;
    public bool waveStarted = false;
    private int currentEnemyCount = 0;

    private Coroutine spawnRoutine;

    [Header("UI")]
    public TMP_Text waveTimerText; // ลาก Text จาก Canvas ใน Inspector

    void Start()
    {
        waveTimerText = QuestProgressUIManager.Instance.Timer.GetComponent<TMP_Text>();
        StartWave();
        UpdateTimerUI(); // อัพเดท UI ทันทีตอนเริ่มเกม
    }

    void Update()
    {
        if (isWaveActive)
        {
            timeRemaining -= Time.deltaTime;
            timeRemaining = Mathf.Max(0f, timeRemaining); // ป้องกันค่าติดลบ

            UpdateTimerUI();

            if (timeRemaining <= 0f)
            {
                StopWave();
            }
        }
    }

    void UpdateTimerUI()
    {
        if (waveTimerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60f);
            int seconds = Mathf.FloorToInt(timeRemaining % 60f);
            waveTimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    // เริ่ม Wave
    public void StartWave()
    {
        waveStarted = true;

        timeRemaining = waveTime;
        isWaveActive = true;

        if (spawnRoutine != null)
            StopCoroutine(spawnRoutine);

        spawnRoutine = StartCoroutine(SpawnEnemyRoutine());
    }

    // หยุด Wave
    public void StopWave()
    {
        isWaveActive = false;

        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
        }

        Debug.Log("Wave Ended – เวลาเป็นศูนย์แล้ว");
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (!isWaveActive)
                yield break;

            if (currentEnemyCount < maxEnemies)
                SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        float randomZ = Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2);

        Vector3 spawnPosition = transform.position + new Vector3(randomX, 0, randomZ);

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        currentEnemyCount++;

        var comp = newEnemy.GetComponent("Enemy");
        if (comp != null)
        {
            newEnemy.SendMessage("SetSpawner", this, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void EnemyDestroyed()
    {
        currentEnemyCount--;
        if (currentEnemyCount < 0) currentEnemyCount = 0;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, 0.1f, spawnAreaSize.z));
    }
}
