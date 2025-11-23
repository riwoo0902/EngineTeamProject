using System;
using _01.Script.Lrw.PinBallCompo;
using Lrw_PinBall;
using UnityEngine;

namespace _01.Script.Lrw.PinBallMap.PinBallEvent
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class ChoiceMap : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<ICanTriggerEvent>(out ICanTriggerEvent iCanTriggerEvent))
            {
                
                //MapDir.
            }
        }
    }
}