using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class TopDownCharacterController : MonoBehaviour
    {
        public float speed;

        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
            {
                dir.x = -1;
                animator.SetInteger("Direction", 3); // Left animation
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1;
                animator.SetInteger("Direction", 2); // Right animation
            }

            if (Input.GetKey(KeyCode.W))
            {
                dir.y = 1;
                animator.SetInteger("Direction", 1); // Up animation
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir.y = -1;
                animator.SetInteger("Direction", 0); // Down animation
            }

            // Check if the player is moving
            dir.Normalize();
            animator.SetBool("IsMoving", dir.magnitude > 0);

            // Switch to idle animation when not moving
            if (dir.magnitude == 0)
            {
                animator.SetInteger("Direction", -1); // Use -1 or any default value to indicate "no movement"
                animator.SetBool("IsMoving", false);
            }


            GetComponent<Rigidbody2D>().linearVelocity = speed * dir;
        }
    }
}
