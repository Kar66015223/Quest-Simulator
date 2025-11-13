using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [Header("Hand setting")]
    public Transform RightHand;
    public Transform LeftHand;
    public List<Item> inventory = new List<Item>();

    // --- 1. เพิ่มตัวแปรนี้ ---
    private IInteractable currentInteractable; // ตัวแปรเก็บเป้าหมายที่อยู่ใกล้

    Vector3 _inputDirection;
    bool _isAttacking = false;
    bool _isInteract = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        health = maxHealth;
    }

    public void FixedUpdate()
    {
        Move(_inputDirection);
        Turn(_inputDirection);
        Attack(_isAttacking);
        Interact(_isInteract);
    }

    public void Update()
    {
        HandleInput();
    }

    public void AddItem(Item item)
    {
        inventory.Add(item);
    }

    private void HandleInput()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        _inputDirection = new Vector3(x, 0, y);
        if (Input.GetMouseButtonDown(0))
        {
            _isAttacking = true;
        }

        // --- 2. แก้ไขเงื่อนไขนี้ (ให้เช็คว่ามีเป้าหมายไหม) ---
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            _isInteract = true;
        }
    }

    public void Attack(bool isAttacking)
    {
        // ... (โค้ด Attack ของคุณถูกต้องแล้ว ไม่ต้องแก้) ...
        if (isAttacking)
        {
            animator.SetTrigger("Attack");
            var e = InFront as Idestoryable;
            if (e != null)
            {
                e.TakeDamage(Damage);
                Debug.Log($"{gameObject.name} attacks for {Damage} damage.");
            }
            _isAttacking = false;
        }
    }

    // --- 3. แก้ไขเมธอด Interact() ---
    private void Interact(bool interactable)
    {
        if (interactable) // _isInteract ถูกตั้งค่าจาก HandleInput
        {
            // เปลี่ยนจาก 'InFront' มาใช้ 'currentInteractable'
            if (currentInteractable != null && currentInteractable.isInteractable)
            {
                currentInteractable.Interact(this);
            }
            _isInteract = false; // เคลียร์ flag หลังทำงาน
        }
    }

    // --- 4. เพิ่ม 2 เมธอดนี้ (สำหรับให้ NPC เรียก) ---

    // เมธอดสำหรับให้ NPC เรียกใช้ เมื่อผู้เล่น "เข้า" ระยะ
    public void SetInteractable(IInteractable interactable)
    {
        currentInteractable = interactable;
    }

    // เมธอดสำหรับให้ NPC เรียกใช้ เมื่อผู้เล่น "ออก" จากระยะ
    public void ClearInteractable(IInteractable interactable)
    {
        // เช็คก่อนว่าใช่ตัวเดียวกับที่เก็บไว้มั้ย (กันบั๊กตอนอยู่ระหว่าง 2 NPC)
        if (currentInteractable == interactable)
        {
            currentInteractable = null;
        }
    }

    //... (ฟังก์ชันการรักษาและรับความเสียหายของคุณ) ...
}