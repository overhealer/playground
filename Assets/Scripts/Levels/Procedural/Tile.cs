using playground.Assets.Scripts.Common;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace playground.Assets.Scripts.Levels.Procedural
{
    public class Tile
    {
        public Vector2 TileID;
        public int Entropy;
        public LevelTile LevelTile;

        private Dictionary<EdgeDirection, Tile> neighbours = new Dictionary<EdgeDirection, Tile>();
        private List<LevelTile> possibilities = new List<LevelTile>();
        private Dictionary<LevelTile, EdgeType[]> tileRules;
        private float tileSize;

        public Tile(Dictionary<LevelTile, EdgeType[]> tileRules, Vector2 id, float tileSize)
        {
            Entropy = tileRules.Keys.Count;
            TileID = id;
            possibilities = tileRules.Keys.ToList();
            this.tileSize = tileSize;
            this.tileRules = tileRules;
        }

        public void AddNeighbour(EdgeDirection direction, Tile tile)
        {
            neighbours.Add(direction, tile);
        }

        public Tile GetNeighbour(EdgeDirection direction)
        {
            return neighbours[direction];
        }

        public List<EdgeDirection> GetDirections()
        {
            return neighbours.Keys.ToList();
        }

        public List<LevelTile> GetPossibilities()
        {
            return possibilities;
        }

        public void Collapse()
        {
            float[] weights = new float[possibilities.Count];
            for (int i = 0; i < possibilities.Count; i++)
            {
                weights[i] = possibilities[i].TileRandomWeight;
                //Debug.Log(_possibilities[i] + "|" + _tileRules[_possibilities[i]]);
            }
            int randomID = RandomUtility.GetRandomWeightedIndex(weights);
            LevelTile tile = possibilities[randomID];
            LevelTile tileInstance = Object.Instantiate(tile, new Vector3(TileID.x * tileSize, 0, -TileID.y * tileSize), Quaternion.identity);
            LevelTile = tileInstance;
            tileInstance.SetIDText(TileID);
            possibilities = new List<LevelTile>() { possibilities[randomID] };
            Entropy = 0;
        }

        public bool Constrain(List<LevelTile> neighbourPosibilities, EdgeDirection direction)
        {
            bool isReduced = false;

            if (Entropy > 0)
            {
                List<EdgeType> connectors = new List<EdgeType>();
                foreach (var neighbour in neighbourPosibilities)
                {
                    connectors.Add(tileRules[neighbour][(int)direction]);
                }

                EdgeDirection oppositeSide = EdgeDirection.North;
                switch (direction)
                {
                    case EdgeDirection.North:
                        oppositeSide = EdgeDirection.South;
                        break;
                    case EdgeDirection.East:
                        oppositeSide = EdgeDirection.West;
                        break;
                    case EdgeDirection.South:
                        oppositeSide = EdgeDirection.North;
                        break;
                    case EdgeDirection.West:
                        oppositeSide = EdgeDirection.East;
                        break;
                    default:
                        break;
                }

                for (int i = possibilities.Count - 1; i >= 0; i--)
                {
                    if (!connectors.Contains(tileRules[possibilities[i]][(int)oppositeSide]))
                    {
                        possibilities.Remove(possibilities[i]);
                        isReduced = true;
                    }
                }
                Entropy = possibilities.Count;
            }

            return isReduced;
        }
    }
}