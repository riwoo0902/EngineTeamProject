using System;
using System.Collections.Generic;
using System.Linq;
using Lrw_Ord;
using UnityEngine;
using UnityEngine.Events;

public class Ords : MonoBehaviour
{
    private Dictionary<Collider2D,IOrd> _ords = new Dictionary<Collider2D,IOrd>();
    private void Awake()
    {
        OrdBase[] a = GetComponentsInChildren<OrdBase>(true);
        
        
    }
}
