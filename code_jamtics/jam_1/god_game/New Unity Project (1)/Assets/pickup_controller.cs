using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class pickup_controller : MonoBehaviour
{
    //Shrink it
    //pick up the object

//    private void Shrink(Collider collider)
//    {
//        var lerpedValue = Mathf.Lerp(collider.transform.localScale.x, .01f, .05f);
//        collider.transform.localScale = new Vector3(lerpedValue, lerpedValue, lerpedValue);
//    }
//
//    private void Grow(Collider collider)
//    {
//        var lerpedValue = Mathf.Lerp(collider.transform.localScale.x, 2, .05f);
//        collider.transform.localScale = new Vector3(lerpedValue, lerpedValue, lerpedValue);
//    }
//
    private Collider collectedItem;
    [SerializeField] private Transform handTransform;

    private void Pickup(Collider collider)
    {
        collider.transform.SetParent(handTransform);
        collectedItem = collider;


        collider.attachedRigidbody.isKinematic = true;
        collider.transform.localPosition = new Vector3(0, 0, 0);
        collider.enabled = false;
    }

    private void Drop()
    {
        collectedItem.attachedRigidbody.isKinematic = false;
        collectedItem.transform.SetParent(null);

        collectedItem = null;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("grabbable")) return;

        if (!Input.GetKey(KeyCode.P)) return;

        if (!collectedItem)
            Pickup(other);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            if (collectedItem)
                Drop();
        }
    }
}