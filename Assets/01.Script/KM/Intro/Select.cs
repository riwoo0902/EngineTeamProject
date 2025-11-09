using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class ObjSaves
{
    public HingeJoint2D ChainLeft;
    public HingeJoint2D ChainRight;
}

public class Select : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    private GameObject NowSelect;
    private GameObject arrow;
    private Vector2 nowPos;
    [SerializeField] private List<ObjSaves> chains = new List<ObjSaves>();
    [SerializeField] private GameObject blackFrame;
    void FixedUpdate()
    {
        nowPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D collider = Physics2D.OverlapCircle(nowPos, 0.01f, mask);
        if (collider)
        {
            Debug.Log(collider.gameObject.name);
            NowSelect = collider.gameObject;
        }
        else
        {
            NowSelect = null;
        }
    }

    private void Update()
    {
        if(Mouse.current.leftButton.isPressed && NowSelect != null)
        {
            if (NowSelect.CompareTag("Start"))
            {
                Destroy(chains[0].ChainLeft);
                Destroy(chains[0].ChainRight);
            }
            else if (NowSelect.CompareTag("Setting"))
            {
                Destroy(chains[1].ChainLeft);
                Destroy(chains[1].ChainRight);
            }
            else if (NowSelect.CompareTag("Exit"))
            {
                Destroy(chains[2].ChainLeft);
                Destroy(chains[2].ChainRight);
            }
            StartCoroutine(DownDarkFrame());
        }
        if (NowSelect != null)
        {
            if (arrow)
            {
                arrow.transform.localScale = new Vector3(arrow.transform.localScale.x, 0, arrow.transform.localScale.z);
                arrow = null;
            }
            arrow = NowSelect.transform.Find("Arrow").gameObject;
            arrow.transform.localScale = new Vector3(arrow.transform.localScale.x, 1, arrow.transform.localScale.z);
        }
        else if (arrow != null)
        {
            arrow.transform.localScale = new Vector3(arrow.transform.localScale.x, 0, arrow.transform.localScale.z);
            arrow = null;
        }
    }

    private IEnumerator DownDarkFrame()
    {
        yield return new WaitForSeconds(1f);
        blackFrame.transform.DOMoveY(0,1);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(nowPos, 0.01f);
    }
}
