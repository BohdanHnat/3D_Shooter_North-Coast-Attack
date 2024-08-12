using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private HealthComponent playerHealth;
    [SerializeField] private Transform screenRoot;
    [SerializeField] private GameObject deathScreen;
    private void Start()
    {
        playerHealth.OnDie += DeathScreen;
    }
    private void DeathScreen()
    {
        Instantiate(deathScreen, screenRoot);
        
        Context.Instance.AudioSystem.PlaySFX(new AudioData("DM-CGS-35", volume: 0.5f));
        
        StartCoroutine(DeathRoutine());
    }
    private IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(1);
        
        Context.Instance.AudioSystem.PlaySFX(new AudioData("DM-CGS-50", volume: 1f));
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
