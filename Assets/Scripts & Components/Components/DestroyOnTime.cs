using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTime : MonoBehaviour
{
    [SerializeField] private float time;
    private void OnEnable()
    {
        StartCoroutine(DestroyRoutine());
    }
    private IEnumerator DestroyRoutine()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
