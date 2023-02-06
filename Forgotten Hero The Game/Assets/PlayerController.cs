using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Main
{
    public partial class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 1f;
        public float collisionOffset = 0.05f;
        public ContactFilter2D contactFilter;
        public enum Direction { up, down, left, right } // TODO : move out to namespace

        public SwordAttack swordAttack;
        private Direction mouseDirection;
        private Vector2 movementInput;
        private Rigidbody2D rigidBody;
        private Animator animator;
        private SpriteRenderer spriteRenderer;
        private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

        // Start is called before the first frame update
        void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            swordAttack = GetComponent<SwordAttack>();
        }

        private void FixedUpdate()
        {
            if (movementInput != Vector2.zero)
            {
                var success = TryMove(movementInput);
                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));

                    if (!success)
                    {
                        success = TryMove(new Vector2(0, movementInput.y));
                    }
                }
                animator.SetBool("isMoving", success);
            }
            else
                animator.SetBool("isMoving", false);

            xAxisFlip();
        }

        private void xAxisFlip()
        {
            if (movementInput.x < 0)
                spriteRenderer.flipX = true;
            else if (movementInput.x > 0)
                spriteRenderer.flipX = false;

            if (mouseDirection == Direction.left)
                spriteRenderer.flipX = true;
            if (mouseDirection == Direction.right)
                spriteRenderer.flipX = false;
        }

        private bool TryMove(Vector2 derection)
        {
            //Check for collisions
            int count = rigidBody.Cast(
                derection,
                contactFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset);

            if (count == 0)
            {
                rigidBody.MovePosition(rigidBody.position + derection * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else // if we colide, player cant move
                return false;
        }

        void OnMove(InputValue inputValue)
        {
            movementInput = inputValue.Get<Vector2>();

            if (movementInput != Vector2.zero)
            {
                animator.SetFloat("XInput", movementInput.x);
                animator.SetFloat("YInput", movementInput.y);
            }
        }

        void OnFire()
        {
            AnimateAttack();
            swordAttack.AttackRight();
        }

        public void AttackSword()
        {
            Debug.Log(swordAttack);
            swordAttack.AttackRight();

        }

        private void AnimateAttack()
        {
            Vector3 mouseToScreenPosition = Camera.main.ViewportToScreenPoint(Input.mousePosition);
            var mousePos = Input.mousePosition;

            mouseDirection = GetDirection(mousePos.x -= Screen.width / 2, mousePos.y -= Screen.height / 2);

            float directionToFloat = 0f; // workaroud to work with blend tree and animate 
            switch (mouseDirection) // just to turn to float: 0f = up, 0.25f = down, 0.5f = turned, 0.75f = turned
            {
                case Direction.up:
                    directionToFloat = 0f;
                    break;
                case Direction.down:
                    directionToFloat = 0.25f;
                    break;
                case Direction.right:
                    directionToFloat = 0.5f;
                    break;
                case Direction.left:
                    directionToFloat = 0.75f;
                    break;
                default:
                    break;
            }
            animator.SetTrigger("swordAttack");
            animator.SetFloat("mouseDirectionValue", directionToFloat);
        }

        private Direction GetDirection(float Xpos, float yPos)
        {
            if (yPos >= 0)
            {
                if (Math.Abs(Xpos) < yPos)
                    return Direction.up;
                else
                    return Xpos > 0 ? Direction.right : Direction.left;
            }
            else
            {
                if (Math.Abs(Xpos) < Math.Abs(yPos))
                    return Direction.down;
                else
                    return Xpos > 0 ? Direction.right : Direction.left;
            }
        }
    }
}