using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnTime : MonoBehaviour
{
    [SerializeField] private float time;
    private void OnEnable()
    {
        StartCoroutine(DisableRoutine());
    }
    private IEnumerator DisableRoutine()
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
