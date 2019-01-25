using UnityEngine;

namespace Enemy
{
    public class BasicEnemy : AbstractEnemy
    {
        // Initializing
        private void Start()
        {
            Name = "Basic Enemy";
            Health = 80f;
        }

        private void Update()
        {
            // Testing
            if (Input.GetKey(KeyCode.K))
            {
                TakeDamage(1000);
            }
        }

        public override void OnDeath()
        {
            GameManager.Instance.EnemyDeath(this);
        }
    }
}