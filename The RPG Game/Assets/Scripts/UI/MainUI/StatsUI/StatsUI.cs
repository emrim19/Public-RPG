using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsUI : UI
{
    public Transform statsParent;
    public StatBox[] stats;
    public PlayerStats playerStats;

    private void Start() {
        stats = statsParent.gameObject.GetComponentsInChildren<StatBox>();
    }

    private void Update() {
        stats[0].UpdateStats(playerStats.damage);
        stats[1].UpdateStats(playerStats.defence);
        stats[2].UpdateStats(playerStats.attackSpeed);
        stats[3].UpdateStats(playerStats.magic);
    }
}
