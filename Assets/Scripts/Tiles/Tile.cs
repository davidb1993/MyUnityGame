using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Tile: MonoBehaviour, IHeapItem<Tile>
{
    public string TileName;
    private SpriteRenderer m_SpriteRenderer;
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] public GameObject _highlightinReach;
    [SerializeField] public GameObject _highlightnotinReach;
    [SerializeField] public bool _isWalkable;

    [SerializeField] public int g;
    [SerializeField] public int h;
    int heapIndex;

    public int f { get { return g + h; } }
    public Tile parent;

   
   

    public BaseUnit OccupiedUnit;
    public int HeapIndex { get
        {
            return heapIndex;
        }
        set {
            heapIndex = value;
        }
    }

    public int CompareTo(Tile tileToCompare)
    {
        int compare = f.CompareTo(tileToCompare.f);
        if (compare == 0)
        {
            compare = h.CompareTo(tileToCompare.h);
        }
        return -compare;
    }

    public bool Walkable => _isWalkable && OccupiedUnit == null;

    public virtual void Init(int x, int y)
    {
        
    }
    public void SetUnit(BaseUnit unit)
    {
        if (unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }
  

    void OnMouseDown()
    {
        if (UnitManager.Instance.SelectedHero.whichState(Herostate.Walk))
        {
            int number = 0;
           
            
            if (UnitManager.Instance.SelectedHero.numberofSteps < GridManager.Instance.path.Count)
            {
                if (UnitManager.Instance.SelectedHero.numberofSteps > 0)
                {
                    GridManager.Instance.path[UnitManager.Instance.SelectedHero.numberofSteps - 1].SetUnit(UnitManager.Instance.SelectedHero);
                    number = UnitManager.Instance.SelectedHero.numberofSteps;
                }
            }
            else
            {
                GridManager.Instance.path[GridManager.Instance.path.Count - 1].SetUnit(UnitManager.Instance.SelectedHero);
                number = GridManager.Instance.path.Count;
            }
            UnitManager.Instance.SelectedHero.setSteps(number);
            foreach(Tile tile in GridManager.Instance.path) { tile.hightlightDeactivate(); }
            UnitManager.Instance.SelectedHero.changeHeroState(Herostate.Idle);
          
        }   
    }

    void OnMouseEnter()
    {
        
        
        GridManager.Instance.mouseTile = this;
        if (UnitManager.Instance.SelectedHero.whichState(Herostate.Walk)&& this.Walkable)
        {
            if (UnitManager.Instance.SelectedHero.transform.position.x >= transform.position.x && UnitManager.Instance.SelectedHero.GetComponent<SpriteRenderer>().flipX == false)
            {

                UnitManager.Instance.SelectedHero.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (UnitManager.Instance.SelectedHero.transform.position.x < transform.position.x && UnitManager.Instance.SelectedHero.GetComponent<SpriteRenderer>().flipX == true)
            {

                UnitManager.Instance.SelectedHero.GetComponent<SpriteRenderer>().flipX = false;
            }
            // _highlightinReach.SetActive(true);
            GridManager.Instance.FindPath(UnitManager.Instance.SelectedHero.OccupiedTile, this);
            foreach(Tile tile in GridManager.Instance.path)
            {
                tile.hightlightActivate();
            }
           
        }
    }
    private void Update()
    {
        


    }

    void hightlightActivate()
    {
        
            if (GridManager.Instance.path.IndexOf(this) < UnitManager.Instance.SelectedHero.numberofSteps)
                _highlightinReach.SetActive(true);
            else _highlightnotinReach.SetActive(true);
       

    }
    void hightlightDeactivate()
    {

        
            _highlightinReach.SetActive(false);
        _highlightnotinReach.SetActive(false);


        
    }

    void OnMouseExit()
    {
        GridManager.Instance.mouseTile = null;
        if (UnitManager.Instance.SelectedHero.whichState(Herostate.Walk)) 
        foreach (Tile tile in GridManager.Instance.path) { tile.hightlightDeactivate(); }
  
    }
}
