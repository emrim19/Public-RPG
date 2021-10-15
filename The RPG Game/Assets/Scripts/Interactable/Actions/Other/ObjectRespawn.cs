using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRespawn : MonoBehaviour
{
    public GameObject respawnObject;
    public float defaultTimer;
    public float timer;

    public void Awake() {
        timer = defaultTimer;
    }

    public void Update() {
        Respawn();
    }

    public void Respawn() {
        if (!respawnObject.activeInHierarchy) {
            if (timer > 0) {
                timer = timer - Time.deltaTime;
                if (timer <= 0) {
                    respawnObject.SetActive(true);
                    timer = defaultTimer;
                }
            }
        }
    }

}
