using Player;
using UnityEngine;

namespace Enemy
{
    public abstract class AbstractEnemy : MonoBehaviour
    {
        // Misc
        [HideInInspector]
        public string Name = "Abstract Enemy";
        public float Health = 100;
        public int Damage = 20;

        public float PulseRange;
        public float PulseDelay;

        private float t;

        protected virtual void Start()
        {
            GameManager.Instance.EnemiesInGame++;
        }

        public virtual void OnDeath()
        {
            GameManager.Instance.EnemyDeath(this);
        }

        private void Update()
        {
            if(t < PulseDelay)
            {
                t += Time.deltaTime;
                return;
            }
            DamagePulse();
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

        public virtual void DamagePulse()
        {
            RaycastHit hit;
            if(!Physics.SphereCast(transform.position, PulseRange, new Vector3(1, 1, 1), out hit)) return;

            if(hit.collider.GetComponent<PlayerHealth>())
            {
                hit.collider.GetComponent<PlayerHealth>().TakeDamage(Damage);
            }
        }

        public virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, PulseRange);
        }
    }
}