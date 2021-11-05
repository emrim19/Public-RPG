using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeProjectile : MonoBehaviour
{
    private Player player;
    public Interactable targetfocus;
    public Ammo ammo;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        AquireTarget();
        FlyTotarget();
    }

    public void AquireTarget() {
        if (player.focus != null) {
            if (player.focus.GetComponent<Enemy>()) {
                if (targetfocus == null) {
                    targetfocus = player.focus;
                }
            }
        }
    }


    public void FlyTotarget() {
        if (targetfocus != null) {
            float step = 15 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetfocus.transform.position + new Vector3(0, 1f, 0), step);

            float distance = Vector3.Distance(transform.position, targetfocus.transform.position);
            if (distance <= 1f) {
                Destroy(gameObject);
                CharacterStats stats = targetfocus.GetComponent<CharacterStats>();
                stats.TakeDamage(player.playerStats.CalcRangeDamage(ammo));
            }
        }
    }
}
