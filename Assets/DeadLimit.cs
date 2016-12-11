using UnityEngine;
using System.Collections;

public class DeadLimit : MonoBehaviour {


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag != "Player") return;
        collider.gameObject.GetComponent<Participant>().GetHurt(100f);
    }
}
