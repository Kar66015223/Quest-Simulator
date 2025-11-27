using TMPro;
using UnityEngine;
using UnityEngine.UI;

// กำหนดให้เป็น sealed เพื่อป้องกันการสืบทอด
public sealed class GameManager : MonoBehaviour
{
    // 1. Private Static Field (The Singleton Instance)
    // ใช้ backing field เพื่อควบคุมการเข้าถึง
    private static GameManager _instance;

    // 2. Public Static Property (Global Access Point)
    public static GameManager Instance
    {
        get
        {
            // ถ้า Instance ยังเป็น null (กรณีถูกเรียกใช้ก่อน Awake)
            if (_instance == null)
            {
                Debug.LogError("GameManager instance is null! Is it in the scene?");
            }
            return _instance;
        }
    }

    [Header("Game State")]
    public int currentCoin = 0;
    public int killCount = 0;
    public bool isGamePaused = false;

    [Header("UI Game")]
    public GameObject pauseMenuUI;
    public TMP_Text scoreText;
    public Slider HPBar;
    public TMP_Text killCountUI;
    public Image bossHPBar;

    // 3. Private Constructor Logic (ใช้ Awake() แทน Constructor ปกติใน Unity)
    private void Awake()
    {
        // ตรวจสอบว่ามี Instance อยู่แล้วหรือไม่
        if (_instance == null)
        {
            // กำหนดให้ Instance นี้เป็น Singleton
            _instance = this;

            // ป้องกันไม่ให้ Object นี้ถูกทำลายเมื่อมีการโหลด Scene ใหม่
            DontDestroyOnLoad(gameObject);

            Debug.Log("GameManager Singleton Initialized.");
        }
        else
        {
            // ถ้ามี Instance อื่นอยู่แล้ว (มาจาก Scene ก่อนหน้า) ให้ทำลายตัวเองทิ้ง
            Debug.Log("Duplicate GameManager found. Destroying self.");
            Destroy(gameObject);
        }
    }

    // ------------------- Singleton Functionality -------------------

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        if (HPBar != null)
        {
            HPBar.maxValue = maxHealth;
            HPBar.value = currentHealth;
            Debug.Log($"Health updated: {currentHealth}/{maxHealth}");
        }
        else
        {
            Debug.LogWarning("HPBar reference is missing in GameManager.");
        }
    }

    public void AddCoin(int amount)
    {
        currentCoin += amount;
        scoreText.text = currentCoin.ToString();
        Debug.Log($"Score updated: {currentCoin}");
        // โค้ดสำหรับอัปเดต UI, บันทึกคะแนน ฯลฯ
    }

    public void AddKill(int amount)
    {
        killCount += amount;
        killCountUI.text = killCount.ToString();
        Debug.Log($"Kill updated: {killCount}");
    }

    public void UpdateCoinUI()
    {
        scoreText.text = currentCoin.ToString();
    }

    public void UpdateKillUI()
    {
        killCountUI.text = killCount.ToString();
    }

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;
        Time.timeScale = isGamePaused ? 0f : 1f;
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(isGamePaused);
        }
        Debug.Log($"Game Paused: {isGamePaused}");
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }
    }
}