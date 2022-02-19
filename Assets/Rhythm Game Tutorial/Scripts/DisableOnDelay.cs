using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnDelay : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] bool usePooler = true;

    private void OnEnable()
    {
        Invoke("DisableObjectDelayed", time);
    }

    private void DisableObjectDelayed()
    {
        if (usePooler) gameObject.GetComponent<PooledObject>().ReturnToPool();
        else gameObject.SetActive(false);
    }
}
