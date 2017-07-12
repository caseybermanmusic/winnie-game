using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterAnimate : MonoBehaviour {
    public void deleteThis () {
        Destroy(this.gameObject);
    }
}