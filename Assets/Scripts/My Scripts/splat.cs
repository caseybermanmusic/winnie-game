using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splat : MonoBehaviour {
    private GameController gameController;
    Animator animator;
    public int splatHealth;
    private int TotalSplatHealth;

    public void Awake() {
        gameController = Object.FindObjectOfType<GameController>();
    }
    void Start() {
		animator = GetComponent<Animator>();
        TotalSplatHealth = splatHealth;
    }

    void DestroyThis() {
        Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Bullet") {
            splatHealth -= 1;
            if (splatHealth <= 2 * (TotalSplatHealth/3) && splatHealth > TotalSplatHealth/3) {
                animator.SetTrigger("Damaged1");
            }
            if (splatHealth <= (TotalSplatHealth/3) && splatHealth > 0) {
                animator.SetTrigger("Damaged2");
            } 

            if (splatHealth <= 0) {
                gameController.splatsDown += 1;
                gameController.moneyMade += 1;
                gameController.updateMoney();
                if (gameController.splatsDown == gameController.windowsToClean) {
                    gameController.nextSegment();
                }
                DestroyThis();
            }
        }
    }
}