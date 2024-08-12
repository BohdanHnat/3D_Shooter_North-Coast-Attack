using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField] private int amountToHeal;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HealthComponent>() == healthComponent)
        {
            healthComponent.OnHealed(amountToHeal);

            Destroy(gameObject);
        }
    }
}
