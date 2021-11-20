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
    public Enemy enemy;
    public PlayerStats playerStats;
    private AbilityUI abilityUI;

    public Weapon weapon;
    public Equipment shield;

    public ActionAnimation actionAnimation;

    public bool isRunning;

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
    public bool isSpellcats1Animation;
    public bool isSpellcats2Animation;
    public bool isSpellcats3Animation;
    public bool isSpellcats4Animation;
    public bool isSpellcats5Animation;
    public bool isSpellcats6Animation;

    private int isWalkingHash;
    private int isRunningHash;
    private int isSwingingAxeHash;
    private int isSwingingPickaxeHash;
    private int isFishingHash;
    private int isPunchingHash;
    private int isAttackingHash;
    private int isBowHash;
    private int isSpellcats1Hash;
    private int isSpellcats2Hash;
    private int isSpellcats3Hash;
    private int isSpellcats4Hash;
    private int isSpellcats5Hash;
    private int isSpellcats6Hash;

    public Vector3 spawnPoint;

    // Start is called before the first frame update
    void Start() {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        animator = GameObject.Find("PlayerAnime").GetComponent<Animator>();
        playerStats = GetComponent<PlayerStats>();

        SetAnimations();
        playerStats.SetSpeed(playerStats.defaultSpeed);
        abilityUI = GameObject.Find("Abilities").GetComponent<AbilityUI>();

    }

    // Update is called once per frame
    void Update() {
        AnimatorManager();
        LeftClick();
        RightClick();
        GoToTarget();
        InputManager();
        CheckWeaponDurability();
        CheckShieldDurability();

        BasicAnimations();
        Combat();
        CombatAnimations();
        SpellAnimations();
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
    private void CheckWeaponDurability() {
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
    private void CheckShieldDurability() {
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


    public void SpellAnimations() {
        if (focus != null && focus.GetComponent<Enemy>() && inCombat) {
            if (SpellManager.instance.currentSpell != null) {
                Spell theSpell = SpellManager.instance.currentSpell;
                if(playerStats.stamina >= theSpell.manaCost) {
                    if (theSpell.spellAnimationNumber == 1) {
                        animator.SetBool(isPunchingHash, false);
                        animator.SetBool(isAttackingHash, false);
                        animator.SetBool(isBowHash, false);

                        animator.SetBool(isSpellcats1Hash, true);
                        animator.SetBool(isSpellcats2Hash, false);
                        animator.SetBool(isSpellcats3Hash, false);
                        animator.SetBool(isSpellcats4Hash, false);
                        animator.SetBool(isSpellcats5Hash, false);
                        animator.SetBool(isSpellcats6Hash, false);
                    }
                    if (theSpell.spellAnimationNumber == 2) {
                        animator.SetBool(isPunchingHash, false);
                        animator.SetBool(isAttackingHash, false);
                        animator.SetBool(isBowHash, false);

                        animator.SetBool(isSpellcats1Hash, false);
                        animator.SetBool(isSpellcats2Hash, true);
                        animator.SetBool(isSpellcats3Hash, false);
                        animator.SetBool(isSpellcats4Hash, false);
                        animator.SetBool(isSpellcats5Hash, false);
                        animator.SetBool(isSpellcats6Hash, false);
                    }
                    if (theSpell.spellAnimationNumber == 3) {
                        animator.SetBool(isPunchingHash, false);
                        animator.SetBool(isAttackingHash, false);
                        animator.SetBool(isBowHash, false);

                        animator.SetBool(isSpellcats1Hash, false);
                        animator.SetBool(isSpellcats2Hash, false);
                        animator.SetBool(isSpellcats3Hash, true);
                        animator.SetBool(isSpellcats4Hash, false);
                        animator.SetBool(isSpellcats5Hash, false);
                        animator.SetBool(isSpellcats6Hash, false);
                    }
                    if (theSpell.spellAnimationNumber == 4) {
                        animator.SetBool(isPunchingHash, false);
                        animator.SetBool(isAttackingHash, false);
                        animator.SetBool(isBowHash, false);

                        animator.SetBool(isSpellcats1Hash, false);
                        animator.SetBool(isSpellcats2Hash, false);
                        animator.SetBool(isSpellcats3Hash, false);
                        animator.SetBool(isSpellcats4Hash, true);
                        animator.SetBool(isSpellcats5Hash, false);
                        animator.SetBool(isSpellcats6Hash, false);
                    }
                    if (theSpell.spellAnimationNumber == 5) {
                        animator.SetBool(isPunchingHash, false);
                        animator.SetBool(isAttackingHash, false);
                        animator.SetBool(isBowHash, false);

                        animator.SetBool(isSpellcats1Hash, false);
                        animator.SetBool(isSpellcats2Hash, false);
                        animator.SetBool(isSpellcats3Hash, false);
                        animator.SetBool(isSpellcats4Hash, false);
                        animator.SetBool(isSpellcats5Hash, true);
                        animator.SetBool(isSpellcats6Hash, false);
                    }
                    if (theSpell.spellAnimationNumber == 6) {
                        animator.SetBool(isPunchingHash, false);
                        animator.SetBool(isAttackingHash, false);
                        animator.SetBool(isBowHash, false);

                        animator.SetBool(isSpellcats1Hash, false);
                        animator.SetBool(isSpellcats2Hash, false);
                        animator.SetBool(isSpellcats3Hash, false);
                        animator.SetBool(isSpellcats4Hash, false);
                        animator.SetBool(isSpellcats5Hash, false);
                        animator.SetBool(isSpellcats6Hash, true);
                    }
                }
                else {
                    SpellManager.instance.currentSpell = null;
                    abilityUI.SetChosenSpell();
                    animator.SetBool(isSpellcats1Hash, false);
                    animator.SetBool(isSpellcats2Hash, false);
                    animator.SetBool(isSpellcats3Hash, false);
                    animator.SetBool(isSpellcats4Hash, false);
                    animator.SetBool(isSpellcats5Hash, false);
                    animator.SetBool(isSpellcats6Hash, false);
                }
            }
            else {
                animator.SetBool(isSpellcats1Hash, false);
                animator.SetBool(isSpellcats2Hash, false);
                animator.SetBool(isSpellcats3Hash, false);
                animator.SetBool(isSpellcats4Hash, false);
                animator.SetBool(isSpellcats5Hash, false);
                animator.SetBool(isSpellcats6Hash, false);
            }
        }
        else {
            animator.SetBool(isSpellcats1Hash, false);
            animator.SetBool(isSpellcats2Hash, false);
            animator.SetBool(isSpellcats3Hash, false);
            animator.SetBool(isSpellcats4Hash, false);
            animator.SetBool(isSpellcats5Hash, false);
            animator.SetBool(isSpellcats6Hash, false);
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
        isSpellcats1Animation = animator.GetCurrentAnimatorStateInfo(0).IsName("Spellcast1");
        isSpellcats2Animation = animator.GetCurrentAnimatorStateInfo(0).IsName("Spellcast2");
        isSpellcats3Animation = animator.GetCurrentAnimatorStateInfo(0).IsName("Spellcast3");
        isSpellcats4Animation = animator.GetCurrentAnimatorStateInfo(0).IsName("Spellcast4");
        isSpellcats5Animation = animator.GetCurrentAnimatorStateInfo(0).IsName("Spellcast5");
        isSpellcats6Animation = animator.GetCurrentAnimatorStateInfo(0).IsName("Spellcast6");
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
        isSpellcats1Hash = Animator.StringToHash("Spellcast1");
        isSpellcats2Hash = Animator.StringToHash("Spellcast2");
        isSpellcats3Hash = Animator.StringToHash("Spellcast3");
        isSpellcats4Hash = Animator.StringToHash("Spellcast4");
        isSpellcats5Hash = Animator.StringToHash("Spellcast5");
        isSpellcats6Hash = Animator.StringToHash("Spellcast6");
    }


    //getters and setters
    public void SetMovementSpeed(float speed) {
        agent.speed = speed;
    }

    public void SetSpawnPoint(int size) {
        spawnPoint = new Vector3(size/2, 0, size/2);
        transform.position = spawnPoint;
    }

}


