using Scenes;
using Scenes.WorldObjectTypes;
using UnityEngine;

namespace MovementScripts
{
    public class CharacterMovement : TestWorldObject
    {
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
            if (IsPaused)
                return;
            ProcessInput();
        }

        private void FixedUpdate()
        {
            if (IsPaused)
                return;
            Move();
        }
    }
}