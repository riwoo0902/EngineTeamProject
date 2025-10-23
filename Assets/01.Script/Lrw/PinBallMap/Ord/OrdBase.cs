using UnityEngine;

namespace Lrw_Ord
{
    public abstract class OrdBase : MonoBehaviour,IOrd
    {
        [SerializeField] protected float HP = 2;
        
        public void Hit(float a)
        {
            HP -= a;
            if (HP <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

