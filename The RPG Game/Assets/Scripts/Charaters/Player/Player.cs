using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public enum ActionAnimation {
    None,
    CutWood,
    MineRock,
    Fishing
}


public class Player : MonoBehaviour {

    public Camera cam;
    public NavMeshAgent agent;
    public Animator animator;
    public Transform target;
    public Interactable focus;
    public PlayerStats playerStats;
    public CharacterCombat combat;

    public Weapon weapon;
    public Equipment shield;
    public ActionAnimation actionAnimation;

    public bool isRunning;
    public float speed;

    public bool isConstructing;
    public bool inCombat;

    //Ground layer
    public LayerMask movementMask;
    public LayerMask objectMask;

    //animtions
    public bool isIdleAnimation;
    public bool isWalkingAnimation;
    public bool isRunningAnimation;
    public bool isSwingingAxeAnimation;
    public bool isSwingingPickaxeAnimation;
    public bool isFishingAnimation;
    public bool isPunchingAnimation;
    public bool isAttackingAnimation;
    public bool isBowAnimation;

    private int isWalkingHash;
    private int isRunningHash;
    private int isSwingingAxeHash;
    private int isSwingingPickaxeHash;
    private int isFishingHash;
    private int isPunchingHash;
    private int isAttackingHash;
    private int isBowHash;

    public Vector3 spawnPoint;

    // Start is called before the first frame update
    void Start() {
        combat = GetComponent<CharacterCombat>();
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        animator = GameObject.Find("PlayerAnime").GetComponent<Animator>();
        playerStats = GetComponent<PlayerStats>();

        SetAnimations();
    }

    // Update is called once per frame
    void Update() {
        AnimatorManager();
        BasicAnimations();
        LeftClick();
        RightClick();
        GoToTarget();
        InputManager();
        CheckWeaponDurability();
        CheckShieldDurability();


        Combat();
        CombatAnimations();
    }

    //INPUT
    private void LeftClick() {
        //avoid clicking through UI
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        if (Input.GetMouseButtonDown(0) && !isConstructing) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100)) {

                Interactable interact = hit.collider.GetComponent<Interactable>();
                if (interact != null) {
                    RemoveFocus();
                    SetFocus(interact);
                }
            }
        }
    }

    private void RightClick() {
        //avoid clicking through UI
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        if (Input.GetMouseButton(1) && !isConstructing) {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, objectMask)) {
                return;
            }

            else if (Physics.Raycast(ray, out hit, 100, movementMask)) {
                //Move out player
                MoveToPoint(hit.point);
                MenuBar.SetTarget_static(null);

                //stop focusing any object
                RemoveFocus();
            }

        }

    }

    private void InputManager() {
        //toggleRun
        if (Input.GetKeyDown(KeyCode.R)) {
            isRunning = !isRunning;
        }
    }



    //MOVEMENT AND INTERACTION
    public void MoveToPoint(Vector3 point) {
        agent.SetDestination(point);
    }
    public void SetFocus(Interactable newFocus) {
        if (newFocus != focus) {
            if (focus != null) {
                newFocus.OnDefocused();
            }
            focus = newFocus;
            FollowTarget(newFocus);
        }
        newFocus.OnFocused(transform);
    }
    public void RemoveFocus() {
        if (focus != null) {
            focus.OnDefocused();
        }
        focus = null;
        StopFollowingTarget();
    }
    private void FollowTarget(Interactable newTarget) {
        target = newTarget.interactionTransform;
        agent.stoppingDistance = newTarget.GetRadius() * 0.3f;
    }
    private void StopFollowingTarget() {
        target = null;
        agent.stoppingDistance = 0f;
    }
    private void GoToTarget() {
        if (target != null) {
            agent.SetDestination(target.position);
            if (focus.hasInteracted) {
                LookAtTarget(target);
            }
        }
    }
    private void LookAtTarget(Transform target) {
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0;
        Quaternion rotate = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * 2);
    }




    //WEAPONS
    public Weapon CheckWeaponEquiped() {
        if (EquipmentManager.instance.currentEquipment[4] != null) {
            weapon = EquipmentManager.instance.currentEquipment[4] as Weapon;
            return weapon;
        }
        else {
            weapon = null;
            return null;
        }
    }
    public void CheckWeaponDurability() {
        if (weapon != null) {
            if (weapon.currentDurability <= 0) {
                weapon = null;
                EquipmentManager.instance.DestroyEquipment(4);
            }
        }
    }

    public Equipment CheckShieldEquiped() {
        if (EquipmentManager.instance.currentEquipment[5] != null) {
            shield = EquipmentManager.instance.currentEquipment[5] as Equipment;
            return shield;
        }
        else {
            shield = null;
            return null;
        }
    }
    public void CheckShieldDurability() {
        if (shield != null) {
            if (shield.currentDurability <= 0) {
                shield = null;
                EquipmentManager.instance.DestroyEquipment(5);
            }
        }
    }


    public void KillPlayer() {
        //GameManager.instance.LoadGame();
    }
    public void Combat() {
        if(focus != null){
            if (focus.GetComponent<Enemy>()) {
                if (inCombat) {
                    agent.velocity = Vector3.zero;
                }
            }
            else if (!focus.GetComponent<Enemy>()) {
                inCombat = false;
            }
        }
        else if(focus == null) {
            inCombat = false;
        }
    }


    //ANIMATIONS
    private void BasicAnimations() {
        //movement animations
        if (agent.velocity.magnitude >= 0.1) {
            if (!isRunning) {
                animator.SetBool(isWalkingHash, true);
                animator.SetBool(isRunningHash, false);
                SetMovementSpeed(3);
            }
            else if (isRunning) {
                animator.SetBool(isWalkingHash, false);
                animator.SetBool(isRunningHash, true);
                SetMovementSpeed(7);
            }
        }
        else if (agent.velocity.magnitude == 0f) {
            animator.SetBool(isWalkingHash, false);
            animator.SetBool(isRunningHash, false);
        }

        //animtions for the actions by interacting
        if (focus != null) {
            if (focus.hasInteracted) {
                //cutting wood
                if (weapon != null) {
                    if (weapon.weaponType == WeaponType.Axe) {
                        if (focus.actionAnimation.Equals(ActionAnimation.CutWood)) {
                            animator.SetBool(isSwingingAxeHash, true);
                        }
                        else {
                            animator.SetBool(isSwingingAxeHash, false);
                        }
                    }
                    else if (weapon.weaponType != WeaponType.Axe) {
                        animator.SetBool(isSwingingAxeHash, false);
                    }

                    //mining rock
                    if (weapon.weaponType == WeaponType.Pickaxe) {
                        if (focus.actionAnimation.Equals(ActionAnimation.MineRock)) {
                            animator.SetBool(isSwingingPickaxeHash, true);
                        }
                        else {
                            animator.SetBool(isSwingingPickaxeHash, false);
                        }
                    }
                    else if (weapon.weaponType != WeaponType.Pickaxe) {
                        animator.SetBool(isSwingingPickaxeHash, false);
                    }

                    //fishing
                    if (weapon.weaponType == WeaponType.FishingPole) {
                        if (focus.actionAnimation.Equals(ActionAnimation.Fishing)) {
                            animator.SetBool(isFishingHash, true);
                        }
                        else {
                            animator.SetBool(isFishingHash, false);
                        }

                    }
                    else if (weapon.weaponType != WeaponType.FishingPole) {
                        animator.SetBool(isFishingHash, false);
                    }
                }
                else if (weapon == null) {
                    actionAnimation = ActionAnimation.None;
                }
            }
            else if (!focus.hasInteracted) {
                actionAnimation = ActionAnimation.None;
            }
        }
        else if (focus == null) {
            actionAnimation = ActionAnimation.None;
        }

        if (actionAnimation.Equals(ActionAnimation.None)) {
            animator.SetBool(isSwingingAxeHash, false);
            animator.SetBool(isSwingingPickaxeHash, false);
            animator.SetBool(isFishingHash, false);
        }

    }
    private void CombatAnimations() {
        if(focus != null && focus.GetComponent<Enemy>()) {
            if (inCombat) {
                //fighting barehanded
                if (CheckWeaponEquiped() == null) {
                    animator.SetBool(isPunchingHash, true);
                    animator.SetBool(isAttackingHash, false);
                    animator.SetBool(isBowHash, false);
                }

                //fighting with weapon
                else if (CheckWeaponEquiped() != null) {

                    //bow and arrow
                    if (CheckWeaponEquiped().weaponType == WeaponType.Bow) {
                        if(EquipmentManager.instance.currentAmmo != null) {
                            animator.SetBool(isPunchingHash, false);
                            animator.SetBool(isAttackingHash, false);
                            animator.SetBool(isBowHash, true);
                        }
                        if (EquipmentManager.instance.currentAmmo == null) {
                            animator.SetBool(isPunchingHash, false);
                            animator.SetBool(isAttackingHash, false);
                            animator.SetBool(isBowHash, false);
                        }
                    }
                    //magic
                    else if (CheckWeaponEquiped().weaponType == WeaponType.Staff) {
                        //missing
                    }
                    else {  //meele
                        if (agent.velocity.magnitude > 0.1f) {
                            animator.SetBool(isPunchingHash, false);
                            animator.SetBool(isAttackingHash, true);
                            animator.SetBool(isBowHash, false);
                        }
                        else {
                            animator.SetBool(isPunchingHash, false);
                            animator.SetBool(isAttackingHash, true);
                            animator.SetBool(isBowHash, false);
                        }
                    }
                }
            }
            else {
                animator.SetBool(isPunchingHash, false);
                animator.SetBool(isAttackingHash, false);
                animator.SetBool(isBowHash, false);
            }
        }
        else {
            animator.SetBool(isPunchingHash, false);
            animator.SetBool(isAttackingHash, false);
            animator.SetBool(isBowHash, false);
        }
    }

    private void AnimatorManager() {
        isIdleAnimation = animator.GetCurrentAnimatorStateInfo(0).IsName("Idle");
        isWalkingAnimation = animator.GetCurrentAnimatorStateInfo(0).IsName("Walk");
        isRunningAnimation = animator.GetCurrentAnimatorStateInfo(0).IsName("Run");
        isSwingingAxeAnimation = animator.GetCurrentAnimatorStateInfo(0).IsName("Axe");
        isSwingingPickaxeAnimation = animator.GetCurrentAnimatorStateInfo(0).IsName("Pickaxe");
        isSwingingAxeAnimation = animator.GetCurrentAnimatorStateInfo(0).IsTag("Fish");
        isPunchingAnimation = animator.GetCurrentAnimatorStateInfo(0).IsTag("Punching");
        isAttackingAnimation = animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack");
        isBowAnimation = animator.GetCurrentAnimatorStateInfo(0).IsTag("Bow");
    }

    private void SetAnimations() {
        isWalkingHash = Animator.StringToHash("Walking");
        isRunningHash = Animator.StringToHash("Running");
        isSwingingAxeHash = Animator.StringToHash("Axe");
        isSwingingPickaxeHash = Animator.StringToHash("Pickaxe");
        isFishingHash = Animator.StringToHash("Fishing");
        isPunchingHash = Animator.StringToHash("Punch");
        isAttackingHash = Animator.StringToHash("WeaponAttack");
        isBowHash = Animator.StringToHash("Bow");
    }


    //getters and setters
    public void SetMovementSpeed(float speed) {
        this.speed = speed;
        agent.speed = speed;
    }

    public void SetSpawnPoint(int size) {
        spawnPoint = new Vector3(size/2, 0, size/2);
        transform.position = spawnPoint;
    }

}


