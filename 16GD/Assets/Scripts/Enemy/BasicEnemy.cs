using UnityEngine;

namespace Enemy
{
    public class BasicEnemy : AbstractEnemy
    {
        // Initializing
        protected override void Start()
        {
            base.Start();
            Name = "Basic Enemy";
            Health = 80f;
        }

        protected override void Update()
        {
            base.Update();
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