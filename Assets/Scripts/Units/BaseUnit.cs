using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit :MonoBehaviour
{

    public string UnitName;
    public Tile OccupiedTile;
    public Faction Faction;
    public int maxHealth;
    public int currentHealth;
    public HealthBar Healthbar;
    [SerializeField] public int numberofSteps;

    
    public void takeDamage(int damage)
    {
        if (currentHealth > damage)
        {
            currentHealth -= damage;
            Healthbar.SetHealth(currentHealth);
        }
        else
        {
            this.gameObject.SetActive(false);
        }

    }

}
