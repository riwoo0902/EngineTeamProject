using UnityEngine;

namespace _01.Script.Lrw.PinBallMap.Ord
{
    public interface IOrd
    {
        public Collider2D Collider { get; set; }
        public void Hit(float a);
        public void ReSet();
    }
}