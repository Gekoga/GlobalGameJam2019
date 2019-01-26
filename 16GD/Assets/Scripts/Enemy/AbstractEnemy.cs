using UnityEngine;

namespace Enemy
{
    public abstract class AbstractEnemy : MonoBehaviour
    {
        // Misc
        [HideInInspector]
        public string Name = "Abstract Enemy";
        public float Health = 100;

        protected virtual void Start()
        {
            GameManager.Instance.EnemiesInGame++;
        }

        public virtual void OnDeath()
        {
            GameManager.Instance.EnemyDeath(this);
        }

        // Subtracts health and checks if we died
        public virtual void TakeDamage(float damage)
        {
            Health -= damage;

            if(Health <= 0)
            {
                OnDeath();
            }
        }
    }
}