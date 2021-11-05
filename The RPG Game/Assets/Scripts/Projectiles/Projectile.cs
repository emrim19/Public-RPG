using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Player player;
    public Interactable targetfocus;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();
    }


    void Update() {
        if (player.focus != null) {
            if (player.focus.GetComponent<Enemy>()) {
                if (targetfocus == null) {
                    targetfocus = player.focus;
                }
            }
        }

        if (targetfocus != null) {
            float step = 15 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetfocus.transform.position + new Vector3(0, 1f, 0), step);

            float distance = Vector3.Distance(transform.position, targetfocus.transform.position);
            if (distance <= 1f) {
                Destroy(gameObject);
                CharacterStats stats = targetfocus.GetComponent<CharacterStats>();
                stats.TakeDamage(24);
            }
        }
    }


    public static void SpawnProjectile(GameObject prefab) {
        Instantiate(prefab, EquipmentManager.instance.weaponTransform.position, Quaternion.identity);
    }


}
