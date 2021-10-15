using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Player player;
    public Interactable targetfocus;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.focus != null) {
            if (player.focus.GetComponent<Enemy>()) {
                if(targetfocus == null) {
                    targetfocus = player.focus;
                }
            }
        }
        FlyTowardsTarget();
    }


    public void FlyTowardsTarget() {
        if (targetfocus != null) {
            transform.LookAt(targetfocus.transform);

            float step = 15 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetfocus.transform.position + new Vector3(0, 1f, 0), step);

            float distance = Vector3.Distance(transform.position, targetfocus.transform.position);
            if (distance <= 1f) {
                player.combat.Attack(targetfocus.GetComponent<CharacterStats>());
                Destroy(gameObject);
            }
        }
    }


}
