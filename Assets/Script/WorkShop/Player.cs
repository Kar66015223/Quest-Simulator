using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [Header("Hand setting")]
    public Transform RightHand;
    public Transform LeftHand;
    public List<Item> inventory = new List<Item>();

    // --- 1. ��������ù�� ---
    private IInteractable currentInteractable; // �������������·���������
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
        // *** 1. ��Ǩ�ͺ�������ö����͹������������ ***
        if (canReceiveInput)
        {
            Move(_inputDirection);
            Turn(_inputDirection);
            Attack(_isAttacking);
        }
        // *** 2. (������͡�����): ��Ҷ١��͡ �����ش RigidBody �ѹ�� ***
        else if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
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
        // *** 1. ��Ǩ�ͺ���͹䢡���Ѻ Input ����Թ ***
        if (canReceiveInput)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            _inputDirection = new Vector3(x, 0, y);
        }
        else
        {
            // *** 2. �Ӥѭ�ҡ: ��Ҷ١��͡ ��ͧ��� Input ���ٹ�� ***
            _inputDirection = Vector3.zero;
        }

        // ��ǹ Attack ��� Interact �ѧ���ӧҹ����������/���� E
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
        // ... (�� Attack �ͧ�س�١��ͧ���� ����ͧ��) ...
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

    // --- 3. ������ʹ Interact() ---
    private void Interact(bool interactable)
    {
        if (interactable) // _isInteract �١��駤�Ҩҡ HandleInput
        {
            // ����¹�ҡ 'InFront' ���� 'currentInteractable'
            if (currentInteractable != null && currentInteractable.isInteractable)
            {
                currentInteractable.Interact(this);
            }
            _isInteract = false; // ������ flag ��ѧ�ӧҹ
        }
    }


    // --- 4. ���� 2 ���ʹ��� (����Ѻ��� NPC ���¡) ---

    // ���ʹ����Ѻ��� NPC ���¡�� ����ͼ����� "���" ����
    public void SetInteractable(IInteractable interactable)
    {
        currentInteractable = interactable;
    }

    // ���ʹ����Ѻ��� NPC ���¡�� ����ͼ����� "�͡" �ҡ����
    public void ClearInteractable(IInteractable interactable)
    {
        // �礡�͹����������ǡѺ������������ (�ѹ��꡵͹���������ҧ 2 NPC)
        if (currentInteractable == interactable)
        {
            currentInteractable = null;
        }
    }

    //... (�ѧ��ѹ����ѡ������Ѻ����������¢ͧ�س) ...
}