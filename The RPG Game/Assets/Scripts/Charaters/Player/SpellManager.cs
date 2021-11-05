using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    #region
    public static SpellManager instance;

    private void Awake() {
        instance = this;
    }

    #endregion

    public Spell currentSpell;
    public Player player;

    public void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();
    }


    public void SetSpell(Spell newSpell) {
        Spell oldSpell = null;

        if(currentSpell != null) {
            oldSpell = currentSpell;
        }
        if(currentSpell != newSpell) {
            currentSpell = newSpell;
        }
        else if(currentSpell == newSpell) {
            currentSpell = null;
        }
    }


}
