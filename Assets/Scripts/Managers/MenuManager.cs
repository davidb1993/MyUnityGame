using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] private GameObject   _buttonFireball, _buttonWalk, _buttonNextTurn;
    [SerializeField] private TextMeshProUGUI numberofstepsDisplay,_displaygameState;
    public bool AttackOn;

    void Awake()
    {
        Instance = this;
    }

   

    public void ShowSelectedHero(BaseHero hero)
    {
    
    }

    public void ShowNumberofSteps(BaseHero hero)
    {
        if (hero == null) {
            numberofstepsDisplay.text="";
            return; }
        numberofstepsDisplay.text = "APs left: " + hero.numberofSteps;
        
    }

    public void OnClickNextStep()
    {
        UnitManager.Instance.SelectedHero.changeHeroState(Herostate.Idle);
        GameManager.Instance.ChangeState(Gamestate.EnemiesTurn);
    }

    public void OnClickMove()
    {
        if (!UnitManager.Instance.SelectedHero.whichState(Herostate.Walk))
        {
            UnitManager.Instance.SelectedHero.changeHeroState(Herostate.Walk);
        }
        else
        {
            UnitManager.Instance.SelectedHero.changeHeroState(Herostate.Idle);
        }
    }

    public void OnClickMagic()
    {
        if (!UnitManager.Instance.SelectedHero.whichState(Herostate.Shoot)&& UnitManager.Instance.SelectedHero.numberofSteps>=5)
        {
            UnitManager.Instance.SelectedHero.changeHeroState(Herostate.Shoot);
        }
        else
        {
            UnitManager.Instance.SelectedHero.changeHeroState(Herostate.Idle);
        }
    }

    public void displayGameStateHero()
    {
        _buttonFireball.SetActive(true);
        _buttonWalk.SetActive(true);
        _buttonNextTurn.SetActive(true);
        ShowNumberofSteps(UnitManager.Instance.SelectedHero);
        _displaygameState.text = "Player";
    }

    public void displayLost()
    {
        _displaygameState.text = "You lost!";
    }
    public void displayWon()
    {
        _displaygameState.text = "You won!";
    }
    public void displayGameStateEnemy()
    {
        _buttonFireball.SetActive(false);
        _buttonWalk.SetActive(false);
        _buttonNextTurn.SetActive(false);
        ShowNumberofSteps(UnitManager.Instance.SelectedHero);
        _displaygameState.text = "Enemies";
    }
  
}
