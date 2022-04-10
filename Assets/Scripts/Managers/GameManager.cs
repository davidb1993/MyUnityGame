using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool GameWon=false;
    public Gamestate Gamestate;


    void Awake() {
        Instance=this;
    }

    void Start()
    {
        ChangeState(Gamestate.GenerateGrid);
    }
   
    public void ChangeState(Gamestate newState)
    {
        Gamestate = newState;

        switch (newState){
            case Gamestate.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;
            case Gamestate.SpawnHero:
                UnitManager.Instance.SpawnHero();
                break;
            case Gamestate.SpawnEnemies:
                UnitManager.Instance.SpawnEnemy();
                break;
            case Gamestate.HeroTurn:
               
                if (UnitManager.Instance.SelectedHero.numberofSteps > 5)
                {
                    UnitManager.Instance.SelectedHero.setSteps(UnitManager.Instance.SelectedHero.numberofSteps -10);
                }
                else UnitManager.Instance.SelectedHero.setSteps(-5);
                MenuManager.Instance.displayGameStateHero();
                break;
            case Gamestate.EnemiesTurn:
                /*foreach (BaseEnemy enemy in UnitManager.Instance.allEnemies)
                {
                    //GameWon = !enemy.isActiveAndEnabled;
                }*/
                if (GameWon == true)
                {
                    MenuManager.Instance.displayWon();
                    GameManager.Instance.ChangeState(Gamestate.GameEnd);
                }
                MenuManager.Instance.displayGameStateEnemy();
                foreach (BaseEnemy enemy in UnitManager.Instance.allEnemies) {
                    enemy.attack();
                    if (!UnitManager.Instance.SelectedHero.gameObject.activeSelf)
                    {
                        MenuManager.Instance.displayLost();
                        GameManager.Instance.ChangeState(Gamestate.GameEnd);
                        break;
                    }
                    else  ChangeState(Gamestate.HeroTurn);
                }
               
                break;
            case Gamestate.GameEnd:
                Application.Quit();
                break;
        }
    }
}

public enum Gamestate
{
   GenerateGrid=0,
   SpawnHero=1,
   SpawnEnemies=2,
   HeroTurn=3,
   EnemiesTurn=4,
   GameEnd=5
}