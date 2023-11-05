using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float defaultSpeed = 5.0f;  //기본 이동속도
    public float speed = 5.0f; // 이동 속도 
    public float jumpForce = 8.0f; // 점프 힘
    public float dashSpeed = 10.0f; // 대시 속도
    public float dashDuration = 0.5f; // 대시 지속 시간
    public float maxJumpAngle = 30.0f; // 최대 점프 각도 (좌우로 움직일 수 있는 각도)
    Vector2 lockOnMovement = new Vector2();  //락온 중 이동 변경을 위한 값

    private Vector3 movement = Vector3.zero;
    public bool isGrounded = true; // 땅에 닿았는지 여부
    [SerializeField]
    private int jumpCount = 0; // 점프 횟수
    private bool isDashing = false; // 대시 중인지 여부
    //private float dashTimer = 0.0f; // 대시 타이머
    public Rigidbody rb;

    public Transform enemyToLookAt; // 시선을 고정할 적 캐릭터
    public bool isTargeting = false; // 플레이어가 바라보는 중인지 여부
    private float maxDistanceForTargetLock = 15f; // 시선 고정을 위한 최대 거리
    private Camera mainCamera; // 시야를 체크할 카메라
    private Transform mainCameraTransform;

    Animator anim;//animation variable

    GameObject nearObject;
    public Weapon equipWeapon;

    public bool isDodge;
    public TrailsFX.TrailEffect dodgeTrail;

    //weapon variable
    public GameObject[] weapons;
    public bool[] hasWeapons;
    bool isSwap;
    bool isAttack;
    int equipWeaponIndex = -1;
    MeleeAreaSetup meleeAreaSetup;

    //attack
    public bool canDoCombo;

    //hitted
    PlayerParryGuard playerParryG;
    public bool isHitted_pc = false;

    //die
    PlayerStats playerStats;
    public bool isDead = false;

    //movement Lock/Unlock
    public bool canMovePlayer = true; // 회전 가능 여부를 제어하는 플래그 (Dodge에서 연동해서 씀)

    //canRotate
    public bool canPlayerRotate = false;
    private float animatableRotationTime = 0.1f;

    // 인벤토리 컴포넌트
    [SerializeField]
    private Inventory theInventory;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        mainCameraTransform = Camera.main.transform;
        mainCamera = Camera.main;

        anim = GetComponent<Animator>();//animation

        meleeAreaSetup = GetComponent<MeleeAreaSetup>();

        playerParryG = GetComponent<PlayerParryGuard>();

        playerStats = GetComponent<PlayerStats>();

        theInventory = FindObjectOfType<Inventory>();

        //Invoke("StartSwap",1f);
    }

    void Update()
    {
        if (!isDead)
        {
            #region Update속 이동입력을 받는 곳
            // WASD 키를 사용하여 캐릭터 이동
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 cameraForward = mainCameraTransform.forward;
            Vector3 cameraRight = mainCameraTransform.right;
            cameraForward.y = 0f; // y 축 회전 방향을 무시합니다.
            cameraRight.y = 0f;

                if (canMovePlayer) // Dodge 할때 정지 시켜서 
                {
                movement = (cameraForward * verticalInput + cameraRight * horizontalInput).normalized;


                // Rigidbody를 사용하여 이동 처리
                rb.velocity = new Vector3(movement.x * speed, rb.velocity.y, movement.z * speed);
                //콜라이더의 좌표상의 이동 담당

                if (movement != Vector3.zero)
                {
                    transform.forward = movement.normalized; //콜라이더의 회전담당
                }

                lockOnMovement.x = horizontalInput;         //기본이동 및 락온이동을 위한 입력값 패러미터로 받기
                lockOnMovement.y = verticalInput;           //기본이동 및 락온이동을 위한 입력값 받기

                anim.SetFloat("horizon", lockOnMovement.x);     //락온 중 이동 변경을 위한 값
                anim.SetFloat("vertical", lockOnMovement.y);    //락온 중 이동 변경을 위한 값
                anim.SetFloat("movement", Mathf.Abs(lockOnMovement.magnitude)); //기본idle상태를 입력값에 따라 달리는 애니메이션 출력

                #endregion
                }
                else if (canPlayerRotate) {
                    movement = (cameraForward * verticalInput + cameraRight * horizontalInput).normalized;
                    //콜라이더의 좌표상의 이동 담당
                    if (movement != Vector3.zero) {
                        transform.forward = movement.normalized; //콜라이더의 회전담당
                    }
                }

            #region ----------------------------------------------업데이트에 쓰는 실시간 함수들--------------------------------------
            if (!PlayerFlag.isAttacking) { //공격중일때 불가능

                Jump();
                //weapon
                Interraction();
                Swap();
            }

            Dodge();
            #endregion
            PortalEnter();

            if (playerStats.curHealth <= 0)
            {
                Die();
            }
            #region ---Targeting Function----
            // 탭 키가 눌렸을 때
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (!isTargeting)
                {
                    FindNearestEnemy();
                    if (enemyToLookAt != null && IsInCameraView(enemyToLookAt))
                    {
                        isTargeting = true;
                        anim.SetLayerWeight(1, 1);
                        Debug.Log("TargetLock ON");
                    }
                }
                else
                {
                    isTargeting = false;
                    anim.SetLayerWeight(1, 0);
                    Debug.Log("TargetLock OFF");
                }
            }

            // 일정 거리를 벗어나면 시선 고정 해제
            if (isTargeting && enemyToLookAt != null)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemyToLookAt.position);
                // 대상과의 거리를 검사하여 시선고정 여부를 결정 또는 카메라의 범위밖이면 동작X
                if (distanceToEnemy > maxDistanceForTargetLock)
                {
                    isTargeting = false;
                    anim.SetLayerWeight(1, 0);
                    Debug.Log("TargetLock OFF(Out of Range)");
                }
            }

            // 대상이 파괴되었을 때 시선고정 해제
            if (enemyToLookAt == null)
            {
                isTargeting = false;
                anim.SetLayerWeight(1, 0);
                //Debug.Log("TargetLock OFF(Target is null)");
            }

            
            #endregion
        }
    }
    void FreezeRotation()
    {
        rb.angularVelocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        FreezeRotation();
    }
    #region 트리거들 OnCollisionEnter, OnCollisionExit, OnTriggerStay, OnTriggerExit---------------
    void OnCollisionEnter(Collision collision)
    {
        //점프 애니메이션 관리 
        // Ground 태그에 닿았는지 판별해서 닿았으면 땅에 닿음 상태 전달
        if (collision.gameObject.CompareTag("Ground"))
        {

            isGrounded = true; //땅에 닿음
        }
    }

    void OnCollisionExit(Collision collision)
    {
        //점프 애니메이션 관리    
        // Ground 태그에 닿았는지 판별해서 공중에 있는지 판별
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; //땅에 닿지 않음
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "item")
            nearObject = other.gameObject;
        if (other.tag == "Portal")
            nearObject = other.gameObject;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "item")
            nearObject = null;
        if (other.tag == "Portal")
            nearObject = null;
    }

    #endregion

    void PortalEnter() {
        if(nearObject == null) {
            return;
        }
        if (nearObject.tag == "Portal") {
            if(Input.GetKeyDown(KeyCode.E)) {
                Portal portal = nearObject.GetComponent<Portal>();
                GameManager.instance.SceneLoad_normalMap(portal.portalNumber);  
            }
        }
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 1 && !isDashing && !isSwap && !isDodge && isGrounded)
        {
            PlayerFlag.isInteracting = true;
            // 만약 스페이스 바를 누르고, 아직 점프 횟수가 1 미만이며 대시 중이 아니며, 무기 교체나 회피 중이 아니라면:
            anim.SetTrigger("JumpTrigger"); // JumpTrigger
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // 점프 힘을 Rigidbody에 추가
            jumpCount++; // 점프 횟수를 증가
            Invoke("JumptoFallen", 0.1f);
        }
        if (isGrounded) //땅에 닿을 시 jumpCount = 0;
        {
            jumpCount = 0;

            anim.SetBool("isJumping", false);//Jump to Land animation
            PlayerFlag.isInteracting = false;
        }
    }
    void JumptoFallen()
    {
        anim.SetBool("isJumping", true);
    }
    //weapon
    void Swap()
    {
        if (Input.GetKeyDown("1") && (!hasWeapons[0] || equipWeaponIndex == 0))
            return;
        if (Input.GetKeyDown("2") && (!hasWeapons[1] || equipWeaponIndex == 1))
            return;
        if (Input.GetKeyDown("3") && (!hasWeapons[2] || equipWeaponIndex == 2))
            return;

        int weaponIndex = -1;
        if (Input.GetKeyDown("1")) weaponIndex = 0;
        if (Input.GetKeyDown("2")) weaponIndex = 1;
        if (Input.GetKeyDown("3")) weaponIndex = 2;

        if (Input.GetKeyDown("1") || Input.GetKeyDown("2") || Input.GetKeyDown("3"))
        {
            PlayerFlag.isInteracting = true; //플래그 설정
            if (equipWeapon != null)
                equipWeapon.gameObject.SetActive(false);

            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            equipWeapon.gameObject.SetActive(true);
            meleeAreaSetup.LoadWeaponDamageCollider();

            anim.SetTrigger("doSwap");
            isSwap = true;
            speed = 0f;
            PlayerFlag.isInteracting = false; //플래그 설정
            Invoke("SwapOut", 0.6f);
        }
    }
    void SwapOut()
    {
        speed = 5.0f;
        isSwap = false;
    }
    void StartSwap() {
        int weaponIndex = 1;
        PlayerFlag.isInteracting = true; //플래그 설정
        if (equipWeapon != null)
            equipWeapon.gameObject.SetActive(false);

        equipWeaponIndex = weaponIndex;
        equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
        equipWeapon.gameObject.SetActive(true);
        meleeAreaSetup.LoadDefaultDamageCollider();

        anim.SetTrigger("doSwap");
        isSwap = true;
        speed = 0f;
        PlayerFlag.isInteracting = false; //플래그 설정
        Invoke("SwapOut", 0.6f);
    }


    void Interraction()
    {
        if (Input.GetKeyDown(KeyCode.E) && nearObject != null)
        {
            ItemPickUp itemPickUp = nearObject.GetComponent<ItemPickUp>();
            if (itemPickUp != null)
            {
                Item item = itemPickUp.item;

                //theInventory 변수를 통해 아이템을 인벤토리에 추가
                theInventory.AcquireItem(item);

                // 아이템을 주웠을때 능력치 증가
                playerStats.ApplyItemModifiers(item);

                if (item.itemType == Item.ItemType.Red)
                    playerStats.redCount += 1;

                else if (item.itemType == Item.ItemType.Yellow)
                    playerStats.yellowCount += 1;

                else
                    playerStats.blueCount += 1;

                // GameObject 파괴
                Destroy(nearObject);

                // 'nearObject'를 null로 초기화
                nearObject = null;
            }

            else
            {
                Debug.LogWarning("근처 오브젝트에 'ItemPickUp' 컴포넌트가 없거나 아이템이 아닙니다.");
            }
        }
    }


    #region 구르기하는 부분 Dodge
    void Dodge()  // 0.5초 동안 강제로 이동함
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && jumpCount < 1 && !isDashing && !isSwap && !isDodge)
        {
            dodgeTrail.active = true;
            // 현재 속도를 저장하여 나중에 복원할 수 있도록 합니다.
            float originalSpeed = speed;

            // Dodge 동작을 시작합니다.
            speed *= 1.6f;
            isDodge = true;

            anim.SetTrigger("DodgeTrigger"); // Dodge 애니메이션
            canMovePlayer = false; // 회전 비활성화

            // 플레이어의 forward 방향으로 설정
            Vector3 forwardDirection = transform.forward;

            // Dodge 동작 중에는 forward 방향으로만 전진합니다.
            float horizontalInput = forwardDirection.x;
            float verticalInput = forwardDirection.z;

            rb.velocity = new Vector3(horizontalInput * speed, rb.velocity.y, verticalInput * speed);

            // Coroutine을 시작하여 Dodge를 일정 시간 후에 종료합니다.
            StartCoroutine(EndDodgeAfterDelay(0.6f, originalSpeed));
        }

    }

    // Dodge 동작을 0.5초 후 종료하는 코루틴
    IEnumerator EndDodgeAfterDelay(float delay, float originalSpeed)
    {
        yield return new WaitForSeconds(delay);
        dodgeTrail.active = false;
        // Dodge 동작 종료
        isDodge = false;
        speed = originalSpeed; // 속도를 원래 값으로 복원

        canMovePlayer = true; // 이동 활성화
    }
    private void Die() {
        isDead = true;
        canMovePlayer = false;
        //Destroy(this, 2f);
        anim.SetTrigger("doDie");
        GameManager.instance.OnPlayerDead();
    }

    /*  ------------- 속도만 조절 하는 부분 -------------------
    void Dodge()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && jumpCount < 1 && !isDashing && !isSwap && !isDodge)
        {
            speed *= 2;
            anim.SetTrigger("DodgeTrigger");//Jump animation
            //                Ͽ      
            isDodge = true;

            Invoke("DodgeOut", 0.5f);
        }
    }

    void DodgeOut()
    {
        speed = defaultSpeed;
        isDodge = false;
    }
    */
    #endregion

    bool IsInCameraView(Transform target) //카메라 시야범위 
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not assigned.");
            return false;
        }

        Vector3 targetPosition = mainCamera.WorldToViewportPoint(target.position);
        if (targetPosition.x >= 0.06 && targetPosition.x <= 0.94 && targetPosition.y >= 0.05 && targetPosition.y <= 1 && targetPosition.z >= 0)
        {
            return true; // 시야 범위 내에 있음
        }
        else
        {
            Debug.Log("TargetLock OFF(Out of view)");
            return false; // 시야 범위 밖에 있음
        }

    }

    void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyLockonPosition");
        Transform nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            Transform enemyTransform = enemy.transform;
            float distance = Vector3.Distance(transform.position, enemyTransform.position);

            // 최대 거리 이내에 있고 시야에 있으면
            if (distance <= maxDistanceForTargetLock && IsInCameraView(enemyTransform))
            {
                if (nearestEnemy == null)
                {
                    nearestEnemy = enemyTransform;
                }
                else
                {
                    // 이미 선택한 적이 있다면, 가장 가까운 적으로 교체
                    float nearestDistance = Vector3.Distance(transform.position, nearestEnemy.position);
                    if (distance < nearestDistance)
                    {
                        nearestEnemy = enemyTransform;
                    }
                }
            }
        }

        enemyToLookAt = nearestEnemy;
    }
    void LateUpdate()
    {
        if (canMovePlayer)
        {
            // 시선을 고정한 적 오브젝트를 바라보도록 회전
            if (isTargeting && enemyToLookAt != null)
            {
                Vector3 targetPosition = new Vector3(enemyToLookAt.position.x, transform.position.y, enemyToLookAt.position.z);
                transform.LookAt(targetPosition);
            }
        }
    }


    public void ActivateSkill(SOSkill skill)
    {
        LockPlayerInput_ForAnimRootMotion();
        anim.Play(skill.animationName);
        print(string.Format("스킬 {0} 사용 ---- {1} 의 피해를 주었습니다.", skill.name, skill.skillDamage));
        Invoke("UnlockPlayerInput_ForAnimRootMotion", 1f);
    }

    #region 플레이어 이동 제한 발동함수 +애님 루트모션 활성화도 추가
    //플레이어 이동제한/해제
    public void LockPlayerInput()
    {
        canMovePlayer = false;
    }
    public void UnlockPlayerInput()
    {
        canMovePlayer = true;
    }
    //애니메이터 루트모션 활성화/비활성화 + 플레이어 이동제한/해제
    public void LockPlayerInput_ForAnimRootMotion()
    {
        anim.applyRootMotion = true;
        canMovePlayer = false;
    }
    public void UnlockPlayerInput_ForAnimRootMotion()
    {
        anim.applyRootMotion = false;
        canMovePlayer = true;
    }

    public void UnlockAnimRootMotion()
    {
        anim.applyRootMotion = false;
    }
    #endregion

    public IEnumerator AnimationingRotation() {
        canPlayerRotate = true;
        float canRotate = 0;
        while (canRotate < animatableRotationTime) {
            canRotate += Time.deltaTime;
            yield return null;
        }
        yield return null;    
        canPlayerRotate = false;
    }

    void test()
    {
        if (anim.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.5f)
        {
            //애니메이터의 레이어 -base부터 0,1,2,3 순으로 내림차순으로 된다.
            //.normalizedTime은 애니메이션 플레이 시간을 0부터 1까지로 표현하는데
            //위 내용은 현재 실행 중인 애니메이션이 절반 실행되었을 떄를 기준으로 한다
            anim.SetLayerWeight(1, 0);
            //그때 1번 레이어의 가중치를 0으로 한다 즉 안보이게 한다는 것
            //tmep -=TimedeltaTime; 으로 가중치를 temp로 하면 자연스럽게 바꿀 수도있을것이다.
        }
    }




    #region     제거 대기중인 예전 대시
    /*
    // 대시 중인 경우 대시 속도로 이동
    if (isDashing) {
        movement = transform.forward * dashSpeed;
        dashTimer += Time.deltaTime;

        // 대시 지속 시간이 지나면 대시 종료
        if (dashTimer >= dashDuration) {
            isDashing = false;
            dashTimer = 0.0f;
        }
    }
    else {
        // 일반 이동: 이동 방향을 이용하여 이동
        //movement *= speed;

        if (!isGrounded) {
            float angle = Vector3.Angle(movement, transform.forward);
            if (angle > maxJumpAngle) {
                // 만약 현재 움직이는 각도가 제한된 각도보다 크면, 이동 벡터를 수정하여 원하는 각도 내에서 움직일 수 있도록 합니다.
                movement = Quaternion.Euler(0, maxJumpAngle, 0) * transform.forward * speed;
            }
        }
    }
    //if (Input.GetKeyDown(KeyCode.Space) && !isDashing && isGrounded) {
    //    anim.SetTrigger("DashTrigger");//Dash animation
    //    isDashing = true;
    //}
    */
    #endregion


}