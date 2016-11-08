using UnityEngine;
using System.Collections;

public class HumanAnimationScript : MonoBehaviour {

    Animator animator;
    float bound = 0.1f;
    float lastVelocity;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        lastVelocity = transform.parent.GetComponent<Rigidbody2D>().velocity.magnitude;

    }
	
	// Update is called once per frame
	void Update () {
	}

    //void OnTriggerEnter2D(Collider2D c)
    //{
    //    animator.SetBool("Idle", true);
    //}

    //void OnTriggerExit2D(Collider2D c)
    //{
    //    animator.SetBool("Idle", false);
    //}
}
