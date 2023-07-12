using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static int currentHealth;
    private readonly int MAX_HEALTH = 5;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MAX_HEALTH;
    }

    public static bool isDead()
    {
        return currentHealth == 0;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
