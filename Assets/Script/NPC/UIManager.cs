using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public GameObject interactPrompt;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // <<< ตัวนี้สำคัญมาก
        }
        else
        {
            Destroy(gameObject);
        }

        if (interactPrompt != null)
        {
            interactPrompt.SetActive(false);
        }
    }

    // เมธอดสำหรับเปิด/ปิด Prompt และกำหนดตำแหน่ง
    public void SetPromptActive(bool isActive, Transform target = null)
    {
        if (interactPrompt != null)
        {
            interactPrompt.SetActive(isActive);

            // ถ้าให้แสดง Prompt และมีการส่งตำแหน่งมา
            if (isActive && target != null)
            {
                
            }
        }
    }
}