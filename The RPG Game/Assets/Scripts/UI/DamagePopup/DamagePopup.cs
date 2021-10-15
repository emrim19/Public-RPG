using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
   
    
    private float disappearTimer;
    private TextMeshPro textMesh;
    private Color textColor;
    private Vector3 moveVector;

    private void Awake() {
        textMesh = GetComponent<TextMeshPro>();
    }

    private void Update() {
        MovePopup();
    }

    public static DamagePopup Create(Vector3 position, int damage) {
        Transform damagePopupTrans = Instantiate(GameAssets.instance.damagePopup, position, Quaternion.identity);
        DamagePopup theDamagePopup = damagePopupTrans.GetComponent<DamagePopup>();
        theDamagePopup.Setup(damage);
        
        return theDamagePopup;
    }

    public void Setup(int damage) {
        textMesh.SetText(damage.ToString());
        textColor = textMesh.color;
        disappearTimer = 1f;
        moveVector = new Vector3(Random.Range(-0.2f, 0.2f), 1.5f, 0) * 10f;

    }

    private void MovePopup() {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 6f * Time.deltaTime;
        transform.LookAt(Camera.main.transform.position);
        transform.rotation = Quaternion.LookRotation(transform.position - (Camera.main.transform.position));


        disappearTimer -= Time.deltaTime;
        if(disappearTimer < 0) {
            float disappearSpeed = 3f;
            //start disappearing
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a <= 0) {
                Destroy(gameObject);
            }
        }
    }

}

