using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public int Health = 100;

        public void TakeDamage(int damage)
        {
            Health -= damage;

            if(Health <= 0)
            {
                Death();
            }
        }

        public void Death()
        {
            Debug.Log("Player died");
        }
    }
}