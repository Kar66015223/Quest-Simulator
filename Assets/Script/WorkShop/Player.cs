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
    [Header("Control")]
    public bool canReceiveInput = true;

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
        // *** 1. ตรวจสอบว่าสามารถเคลื่อนที่ได้หรือไม่ ***
        if (canReceiveInput)
        {
            Move(_inputDirection);
            Turn(_inputDirection);
            Attack(_isAttacking);
        }
        // *** 2. (ตัวเลือกเสริม): ถ้าถูกล็อก ให้หยุด RigidBody ทันที ***
        else if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        
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
        // *** 1. ตรวจสอบเงื่อนไขการรับ Input การเดิน ***
        if (canReceiveInput)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            _inputDirection = new Vector3(x, 0, y);
        }
        else
        {
            // *** 2. สำคัญมาก: ถ้าถูกล็อก ต้องตั้ง Input เป็นศูนย์ ***
            _inputDirection = Vector3.zero;
        }

        // ส่วน Attack และ Interact ยังคงทำงานได้ด้วยเมาส์/คีย์ E
        if (Input.GetMouseButtonDown(0))
        {
            _isAttacking = true;
        }
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