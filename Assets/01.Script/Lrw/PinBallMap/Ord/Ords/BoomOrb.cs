using _01.Script.Lrw.PinBallCompo;
using Lrw_Ord;
using UnityEngine;

namespace _01.Script.Lrw.PinBallMap.Ord.Ords
{
    public class BoomOrb : OrdBase
    {
        [SerializeField] private float boomPower = 10;
        protected override void Awake()
        {
            base.Awake();
            OnDestroyEvent += Boom;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            OnDestroyEvent -= Boom;
        }

        private void Boom(PinBallBase b)
        {
            PinBallBase a = FindAnyObjectByType<PinBallBase>();
            a.Rigid.AddForce((a.transform.position - transform.position).normalized * boomPower,ForceMode2D.Impulse);
        } 
    }
}