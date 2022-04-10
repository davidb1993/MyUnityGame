using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    
    private List<ScriptableUnit> _units;
    
    public BaseHero SelectedHero;
    public List<BaseEnemy> allEnemies=new List<BaseEnemy>(); 

    void Awake()
    {
        Instance = this;
        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
    }

    public void SpawnHero()
    {
        var heroCount = 1;
            for(int i=0; i< heroCount; i++)
        {
            var firstPrefab = GetFirstUnit<BaseHero>(Faction.Hero);
            var spawnedHero = Instantiate(firstPrefab);
            var randomSpawnTile = GridManager.Instance.GetHeroSpawnTile();
            

            randomSpawnTile.SetUnit(spawnedHero);
            SelectedHero = spawnedHero;
            GameManager.Instance.ChangeState(Gamestate.SpawnEnemies);
        }
    }
    public void SpawnEnemy()
    {
        var enemyCount = 3;
        for (int i = 0; i < enemyCount; i++)
        {
            var firstPrefab = GetRandomUnit<BaseEnemy>(Faction.Enemy);
            var spawnedEnemy = Instantiate(firstPrefab);
            allEnemies.Add(spawnedEnemy);
            var randomSpawnTile = GridManager.Instance.GetEnemySpawnTile();

            randomSpawnTile.SetUnit(spawnedEnemy);
            GameManager.Instance.ChangeState(Gamestate.HeroTurn);

        }
    }
    private T GetFirstUnit<T>(Faction faction) where T : BaseUnit
    {
        return (T)_units.Where(u => u.Faction == faction).First().UnitPrefab;
    }
    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit
    {
        
        var lol= _units.FindAll(u => u.Faction == faction);
        var i= Random.Range(0,lol.Count);
        Debug.Log(i);
        return (T) lol[i].UnitPrefab;
    }

    public void nextTurn()
    {
        if (SelectedHero == null)
        {
            MenuManager.Instance.displayLost();
        }
        else GameManager.Instance.ChangeState(Gamestate.HeroTurn);
    }
}
