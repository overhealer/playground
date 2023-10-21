using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
namespace playground
{
    public class ProceduralLevel : Level
    {
        [SerializeField] private List<LevelTile> _tilesPrefabs;
        [SerializeField] private Vector2 _mapSize;

        private Dictionary<LevelTile, EdgeType[]> _tileRules;
        private Tile[,] _tiles;

        public override void UpdateLevel()
        {
            base.UpdateLevel();
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                GenerateLevel();
            }
        }

        public void GenerateLevel()
        {
            //set tile rules dictionary
            _tileRules = new Dictionary<LevelTile, EdgeType[]>();
            for (int i = 0; i < _tilesPrefabs.Count; i++)
            {
                _tileRules.Add(_tilesPrefabs[i], _tilesPrefabs[i].GetAllEdgeTypes());
            }

            _tiles = new Tile[(int)_mapSize.x, (int)_mapSize.y];
            for (int y = 0; y < _mapSize.y; y++)
            {
                for (int x = 0; x < _mapSize.x; x++)
                {
                    Tile tile = new Tile(_tileRules, new Vector2(x, y), 2f);
                    _tiles[x, y] = tile;
                }
            }

            for (int y = 0; y < _mapSize.y; y++)
            {
                for (int x = 0; x < _mapSize.x; x++)
                {
                    Tile tile = _tiles[x, y];
                    if (y > 0)
                    {
                        tile.AddNeighbour(EdgeDirection.North, _tiles[x, y - 1]);
                    }
                    if (x < _mapSize.x - 1)
                    {
                        tile.AddNeighbour(EdgeDirection.East, _tiles[x + 1, y]);
                    }
                    if (y < _mapSize.y - 1)
                    {
                        tile.AddNeighbour(EdgeDirection.South, _tiles[x, y + 1]);
                    }
                    if (x > 0)
                    {
                        tile.AddNeighbour(EdgeDirection.West, _tiles[x - 1, y]);
                    }
                }
            }

            StartCoroutine(GenerationRoutine());
        }

        public void DestroyLevel()
        {

        }

        private IEnumerator GenerationRoutine()
        {
            while (WaveCollapse())
            {
                yield return null/*new WaitForSeconds(0.05f)*/;
            }
        }

        private int GetEntropy(int x, int y)
        {
            return _tiles[x, y].Entropy;
        }

        private LevelTile GetTile(int x, int y)
        {
            return _tiles[x, y].GetPossibilities()[0];
        }

        private int GetLowestEntropy()
        {
            int lowest = _tileRules.Keys.Count;
            for (int y = 0; y < _mapSize.y; y++)
            {
                for (int x = 0; x < _mapSize.x; x++)
                {
                    if (_tiles[x, y].Entropy > 0)
                    {
                        if (_tiles[x, y].Entropy < lowest)
                        {
                            lowest = _tiles[x, y].Entropy;
                        }
                    }
                }
            }
            return lowest;
        }

        private List<Tile> GetTilesLowestEntropy()
        {
            int lowest = _tileRules.Keys.Count;
            List<Tile> tileList = new List<Tile>();
            for (int y = 0; y < _mapSize.y; y++)
            {
                for (int x = 0; x < _mapSize.x; x++)
                {
                    if (_tiles[x, y].Entropy > 0)
                    {
                        if (_tiles[x, y].Entropy < lowest)
                        {
                            tileList.Clear();
                            lowest = _tiles[x, y].Entropy;
                        } 
                        else if(_tiles[x, y].Entropy == lowest)
                        {
                            tileList.Add(_tiles[x, y]);
                        }
                    }
                }
            }
            return tileList;
        }

        private bool WaveCollapse()
        {
            List<Tile> lowestEntropyTile = GetTilesLowestEntropy();

            if (lowestEntropyTile.Count == 0)
                return false;

            Tile tileToCollapse = lowestEntropyTile[Random.Range(0, lowestEntropyTile.Count)];
            tileToCollapse.Collapse();
            

            Stack<Tile> stack = new Stack<Tile>();
            stack.Push(tileToCollapse);
            while (stack.Count > 0)
            {
                Tile tile = stack.Pop();
                List<LevelTile> tilePossibilities = tile.GetPossibilities();
                List <EdgeDirection> directions = tile.GetDirections();
                for (int i = 0; i < directions.Count; i++)
                {
                    Tile neighbour = tile.GetNeighbour(directions[i]);
                    if(neighbour.Entropy != 0)
                    {
                        bool isReduced = neighbour.Constrain(tilePossibilities, directions[i]);
                        if(isReduced)
                        {
                            stack.Push(neighbour);
                        }
                    }
                }
            }
            string entropies = "";
            for (int y = 0; y < _mapSize.y; y++)
            {
                for (int x = 0; x < _mapSize.x; x++)
                {
                    entropies += _tiles[x, y].Entropy + "|";
                }
                entropies += "\n";
            }
            print(entropies);
            return true;
        }
    }

}
