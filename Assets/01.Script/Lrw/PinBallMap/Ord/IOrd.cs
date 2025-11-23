using _01.Script.Lrw.PinBallCompo;
using UnityEngine;

namespace _01.Script.Lrw.PinBallMap.Ord
{
    public interface IOrd
    {
        public Collider2D Collider { get; set; }
        public void Hit(float a,PinBallBase b);
        public void ReSet();
    }
}