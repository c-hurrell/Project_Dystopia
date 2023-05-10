using Combat;
using UnityEngine;


namespace MovementScripts
{
    public class CharacterMovement : MonoBehaviour
    {
        public Animator animator;
        private Vector2 _inputDirection;
        private Rigidbody2D _rigidbody;
        [SerializeField] private float speed = 5f;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void ProcessInput()
        {
            // Get the input from the player
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            _inputDirection = new(horizontal, vertical);
            _inputDirection = Vector2.ClampMagnitude(_inputDirection, 1);
        }

        private void Move()
        {
            // Move the player
            _rigidbody.MovePosition(_rigidbody.position + _inputDirection * (speed * Time.fixedDeltaTime));
        }

        private void Update()
        {
            if (CombatManager.IsInCombat) return;
            ProcessInput();

            if (Input.GetKeyDown(KeyCode.S))
            {
                animator.SetBool("South", true);
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                animator.SetBool("South", false);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                animator.SetBool("North", true);
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                animator.SetBool("North", false);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetBool("West", true);
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                animator.SetBool("West", false);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                animator.SetBool("East", true);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                animator.SetBool("East", false);
            }
        }

        private void FixedUpdate()
        {
            if (CombatManager.IsInCombat) return;
            Move();
        }
    }
}