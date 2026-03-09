using overhealer.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playground.Assets.Scripts.Levels.Procedural
{
    public class ProceduralLevelGenerator :
        MonoBehaviour,
        IInitialisable,
        IUpdatable
    {
        public Vector2 MapSize;

        [SerializeField]
        private List<LevelTile> tilesPrefabs;

        [SerializeField]
        private ProceduralLevelController proceduralLevelController;

        private Dictionary<LevelTile, EdgeType[]> tileRules;
        private Tile[,] tiles;
        private Coroutine generateCoroutine;

        public void Init()
        {
            //set tile rules dictionary
            tileRules = new Dictionary<LevelTile, EdgeType[]>();
            for (int i = 0; i < tilesPrefabs.Count; i++)
            {
                tileRules.Add(tilesPrefabs[i], tilesPrefabs[i].GetAllEdgeTypes());
            }

            proceduralLevelController.Init(this);
        }

        public void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                GenerateLevel();
            }
        }

        public void GenerateLevel()
        {
            DestroyLevel();

            tiles = new Tile[(int)MapSize.x, (int)MapSize.y];
            for (int y = 0; y < MapSize.y; y++)
            {
                for (int x = 0; x < MapSize.x; x++)
                {
                    Tile tile = new Tile(tileRules, new Vector2(x, y), 2f);
                    tiles[x, y] = tile;
                }
            }

            for (int y = 0; y < MapSize.y; y++)
            {
                for (int x = 0; x < MapSize.x; x++)
                {
                    Tile tile = tiles[x, y];
                    if (y > 0)
                    {
                        tile.AddNeighbour(EdgeDirection.North, tiles[x, y - 1]);
                    }
                    if (x < MapSize.x - 1)
                    {
                        tile.AddNeighbour(EdgeDirection.East, tiles[x + 1, y]);
                    }
                    if (y < MapSize.y - 1)
                    {
                        tile.AddNeighbour(EdgeDirection.South, tiles[x, y + 1]);
                    }
                    if (x > 0)
                    {
                        tile.AddNeighbour(EdgeDirection.West, tiles[x - 1, y]);
                    }
                }
            }

            generateCoroutine = StartCoroutine(GenerationRoutine());
        }

        public void DestroyLevel()
        {
            if (tiles == null || tiles.Length == 0)
                return;

            if (generateCoroutine != null)
            {
                StopCoroutine(generateCoroutine);
            }

            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                for (int x = 0; x < tiles.GetLength(0); x++)
                {
                    Destroy(tiles[x, y].LevelTile.gameObject);
                }
            }
        }

        private IEnumerator GenerationRoutine()
        {
            while (WaveCollapse())
            {
                yield return null/*new WaitForSeconds(5.05f)*/;
            }
        }

        private int GetEntropy(int x, int y)
        {
            return tiles[x, y].Entropy;
        }

        private LevelTile GetTile(int x, int y)
        {
            return tiles[x, y].GetPossibilities()[0];
        }

        private int GetLowestEntropy()
        {
            int lowest = tileRules.Keys.Count;
            for (int y = 0; y < MapSize.y; y++)
            {
                for (int x = 0; x < MapSize.x; x++)
                {
                    if (tiles[x, y].Entropy > 0)
                    {
                        if (tiles[x, y].Entropy < lowest)
                        {
                            lowest = tiles[x, y].Entropy;
                        }
                    }
                }
            }
            return lowest;
        }

        private List<Tile> GetTilesLowestEntropy()
        {
            int lowest = tileRules.Keys.Count;
            List<Tile> tileList = new List<Tile>();
            for (int y = 0; y < MapSize.y; y++)
            {
                for (int x = 0; x < MapSize.x; x++)
                {
                    if (tiles[x, y].Entropy > 0)
                    {
                        if (tiles[x, y].Entropy < lowest)
                        {
                            tileList.Clear();
                            lowest = tiles[x, y].Entropy;
                        }
                        if (tiles[x, y].Entropy == lowest)
                        {
                            tileList.Add(tiles[x, y]);
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
                List<EdgeDirection> directions = tile.GetDirections();
                for (int i = 0; i < directions.Count; i++)
                {
                    Tile neighbour = tile.GetNeighbour(directions[i]);
                    if (neighbour.Entropy != 0)
                    {
                        bool isReduced = neighbour.Constrain(tilePossibilities, directions[i]);
                        if (isReduced)
                        {
                            stack.Push(neighbour);
                        }
                    }
                }
            }
            /*string entropies = "";
            for (int y = 0; y < _mapSize.y; y++)
            {
                for (int x = 0; x < _mapSize.x; x++)
                {
                    entropies += _tiles[x, y].Entropy + "|";
                }
                entropies += "\n";
            }
            print(entropies);*/
            return true;
        }
    }
}