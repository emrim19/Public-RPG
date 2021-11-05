using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool isFocus = false;
    public bool hasInteracted = false;

    public Player player;
    public Transform playerTransform;
    public Transform interactionTransform;

    public LayerMask groundLayer;

    private float radius = 2.5f;
    [SerializeField]
    protected float timer, minTimer, maxTimer;
    public bool isDoingAction;
    protected bool success;
    protected int randomChance;
    
    public ActionAnimation actionAnimation;

    void Awake() {
        player = GameObject.Find("Player").GetComponent<Player>();
        timer = Random.Range(minTimer, maxTimer);
    }

    // Start is called before the first frame update
    void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();
        gameObject.tag = "StaticObject";
        groundLayer = LayerMask.GetMask("GroundLayer");
    }

    // Update is called once per frame
    void Update() {
        InitUpdate();
    }

    
    protected virtual void InitUpdate() {
        if (isFocus && !hasInteracted) {
            float distance = Vector3.Distance(playerTransform.position, interactionTransform.position);

            if (distance <= radius) {
                Interact();
                hasInteracted = true;
            }
        }

        if (isDoingAction) {
            DoingAction();
        }
    }

    protected virtual void Interact() {
        //different for all interactables
    }
    public virtual void DoingAction() {
        player.actionAnimation = actionAnimation;
        //Do something
    }


    protected void SpawnGameObject(GameObject item) {
        RaycastHit hit;
        float randPosX = Random.Range(-3, 3);
        float randPosZ = Random.Range(-3, 3);

        if (randPosX == 0 || randPosZ == 0) {
            randPosX = Random.Range(1, 3);
            randPosZ = Random.Range(1, 3);
        }
        Ray ray = new Ray(transform.position + Vector3.up * 100, Vector3.down);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer)) {
            if (hit.collider != null) {
                GameObject thePrefab = Instantiate(item, new Vector3(transform.position.x + randPosX, hit.point.y, transform.position.z + randPosZ), Quaternion.Euler(new Vector3(item.transform.rotation.x, Random.Range(0, 360), item.transform.rotation.z)));
            }
        }
    }


    public void OnFocused(Transform playerTransform) {
        isFocus = true;
        this.playerTransform = playerTransform;
        hasInteracted = false;
    }
    public void OnDefocused() {
        isFocus = false;
        playerTransform = null;
        hasInteracted = false;
        isDoingAction = false;
    }

    private void OnDrawGizmosSelected() {
        if (interactionTransform == null) {
            interactionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }


    //getters and setters
    public float GetRadius() {
        return radius;
    }

    public void SetRadius(float radius) {
        this.radius = radius;
    }
}
