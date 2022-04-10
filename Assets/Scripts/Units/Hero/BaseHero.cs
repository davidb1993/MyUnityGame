using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHero : BaseUnit
{
    [SerializeField] private GameObject aimObject;
    [SerializeField] private GameObject walkObject;
    
   
    [SerializeField] private Herostate herostate;
    private void Awake()
    {
        herostate = Herostate.Idle;
        numberofSteps = 10;
        maxHealth = 100;
        currentHealth = maxHealth;
        Healthbar.SetMaxHealth(maxHealth);
        

    }

    

    public void changeHeroState(Herostate newstate)
    {
        herostate=newstate;

        switch (newstate)
        {
            case Herostate.Idle:
                aimObject.SetActive(false);
                walkObject.SetActive(false);
                break;
            case Herostate.Walk:
                aimObject.SetActive(false);
                walkObject.SetActive(true);
                break;
            case Herostate.Shoot:
                walkObject.SetActive(false);
                aimObject.SetActive(true);
                break;
        }
    }

    public bool whichState(Herostate state)
    {
        return herostate == state;
    }


    

    public void setSteps(int steps)
    {
        numberofSteps -= steps;
        MenuManager.Instance.ShowNumberofSteps(this);
    }
}

public enum Herostate { 
    Idle=0,
    Walk=1,
    Shoot=2
}