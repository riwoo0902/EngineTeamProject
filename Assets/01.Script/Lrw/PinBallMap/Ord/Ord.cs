using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Lrw_Ord
{
    public abstract class Ord : MonoBehaviour
    {
        [SerializeField] protected int HP = 2;
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            HP--;
            if(HP <= 0) Destroy(gameObject);

        }



    }
}

