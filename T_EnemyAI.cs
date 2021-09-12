using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_EnemyAI : MonoBehaviour
{
    [SerializeField] float startingHealth;
    [SerializeField] float lowHealthThreshold;
    [SerializeField] float healthRestorationRate;

    [SerializeField] float chasingRange;
    [SerializeField] float shootingRange;

    [SerializeField] Transform playerTransform;
    [SerializeField] Transform 

    Material material;

    Transform bestCover;

    private float currentHealth
    {
        get { return currentHealth; }
        set { currentHealth = Mathf.Clamp(value, 0, startingHealth); }
    }

    private void Start()
    {
        currentHealth = startingHealth;
        material = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        currentHealth = Mathf.MoveTowards(currentHealth, startingHealth, healthRestorationRate * Time.deltaTime);
    }

    public void SetColor(Color col)
    {
        material.color = col;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetBestCover(Transform cover)
    {
        this.bestCover = cover;
    }



}
