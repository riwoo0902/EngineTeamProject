using UnityEngine;

namespace Lrw_Ord
{
    public interface IOrd
    {
        public Collider2D Collider { get; set; }
        public void Hit(float a);
    }
}