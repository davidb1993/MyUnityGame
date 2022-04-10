using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    
    public static GridManager Instance;
    [SerializeField] private int _width, _height;

    [SerializeField] private Tile _grassTile, _mountainTile, _treeTile;

    [SerializeField] private Transform _cam;
    public Tile mouseTile;
    public List<Tile> path =new List<Tile>();
    private Tile latestend;
    public List<Tile> neighbor = new List<Tile>();
    private Dictionary<Vector2, Tile> _tiles;
    public int maxSize
    {
        get
        {
            return _width * _height;
        }
    }
    void Awake()
    {
        Instance = this;
    }

   
   public void GenerateGrid()
    {_tiles=new Dictionary<Vector2, Tile>();
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var randomTile=Random.Range(0,10)==3 ?  _treeTile: _grassTile; 
                var spawnedTile = Instantiate(randomTile, new Vector3(x, _height-y-1), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {_height - y - 1}";
                //spawnedTile.GetComponent<Renderer>().sortingOrder =y+1;

                
                spawnedTile.Init(x, _height - y - 1);
                _tiles[new Vector2(x, _height - y - 1)]=spawnedTile;
            }
        }
        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);

        GameManager.Instance.ChangeState(Gamestate.SpawnHero);
    }

    public List<Tile> neighbour(Tile tile) {
        List<Tile> neighbour = new List<Tile>();
        for(int x = -1; x<= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if ((x == 0 && y == 0)||x*y!=0) { continue; }
                if ((int)tile.transform.position.x + x >= 0 && (int)tile.transform.position.x + x < _width && (int)tile.transform.position.y + y >= 0 && (int)tile.transform.position.y + y < _height) {
                    neighbour.Add(_tiles[new Vector2((int)tile.transform.position.x + x, (int)tile.transform.position.y + y)]);
                }
            }
        }
       
        return neighbour;
    }

    private int distance(Tile tile1, Tile tile2) {
        int distance =(int)(Mathf.Abs(tile1.transform.position.x- tile2.transform.position.x)+0.1)+ (int)(Mathf.Abs(tile1.transform.position.y - tile2.transform.position.y) + 0.1);
        return distance;
    }


    public void FindPath(Tile startTile, Tile endTile)
    {

        Stopwatch sw = new Stopwatch();
        
        Heap<Tile> openSet = new Heap<Tile>(GridManager.Instance.maxSize);
        HashSet<Tile> closedSet = new HashSet<Tile>();

        openSet.Add(startTile);

        while( openSet.Count > 0)
        {
            Tile currentTile=openSet.RemoveFirst();

           
            closedSet.Add(currentTile);

            if (endTile == currentTile) {
                
                retracePath(startTile, endTile);
                return;
                }

            foreach(Tile neighbour in neighbour(currentTile))
            {
                if (!(neighbour.Walkable||neighbour.OccupiedUnit==UnitManager.Instance.SelectedHero)  || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCosttoNeighbour = currentTile.g + 1;
                if (!openSet.Contains(neighbour)|| newMovementCosttoNeighbour<neighbour.g) {
                    neighbour.g = newMovementCosttoNeighbour;
                    neighbour.h = distance(neighbour, endTile);
                    neighbour.parent = currentTile;
                   
               

                    if (!openSet.Contains(neighbour)){openSet.Add(neighbour); }
                }
            }
        }
        
    }



    void retracePath(Tile startTile,Tile endTile) {
        List<Tile> path = new List<Tile>() ;
        Tile currenttile =endTile;
        while (currenttile != startTile)
        {
            path.Add(currenttile);
            currenttile = currenttile.parent;            
            
        }
        path.Reverse();
        GridManager.Instance.path = path;
        return; 
    }



    public Tile GetHeroSpawnTile()
    {
        return _tiles.Where(t => t.Key.x < _width / 2 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;   
    }
    public Tile GetEnemySpawnTile()
    {
        return _tiles.Where(t => t.Key.x >_width / 2 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
    }
    public Tile GetTileAtPosition(Vector2 pos){
    if(_tiles.TryGetValue(pos, out var tile))
    {
        return tile;
    }
    return null;
}

 
}
