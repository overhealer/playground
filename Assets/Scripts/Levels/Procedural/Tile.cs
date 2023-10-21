using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace playground
{
    public class Tile
    {
        public Vector2 TileID;
        public int Entropy;
        public LevelTile LevelTile;

        private Dictionary<EdgeDirection, Tile> _neighbours = new Dictionary<EdgeDirection, Tile>();
        private List<LevelTile> _possibilities = new List<LevelTile>();
        private Dictionary<LevelTile, EdgeType[]> _tileRules;
        private float _tileSize;

        public Tile(Dictionary<LevelTile, EdgeType[]> tileRules, Vector2 id, float tileSize)
        {
            Entropy = tileRules.Keys.Count;
            TileID = id;
            _possibilities = tileRules.Keys.ToList();
            _tileSize = tileSize;
            _tileRules = tileRules;
        }

        public void AddNeighbour(EdgeDirection direction, Tile tile)
        {
            _neighbours.Add(direction, tile);
        }

        public Tile GetNeighbour(EdgeDirection direction)
        {
            return _neighbours[direction];
        }

        public List<EdgeDirection> GetDirections()
        {
            return _neighbours.Keys.ToList();
        }

        public List<LevelTile> GetPossibilities()
        {
            return _possibilities;
        }

        public void Collapse()
        {
            float[] weights = new float[_possibilities.Count];
            for (int i = 0; i < _possibilities.Count; i++)
            {
                weights[i] = _possibilities[i].TileRandomWeight;
            }
            LevelTile tile = _possibilities[Utils.GetRandomWeightedIndex(weights)];
            GameObject.Instantiate(tile, new Vector3(TileID.x * _tileSize, 0, TileID.y * _tileSize), Quaternion.identity);
            LevelTile = tile;
            _possibilities = new List<LevelTile>() { tile };
            Entropy = 0;
        }

        public bool Constrain(List<LevelTile> neighbourPosibilities, EdgeDirection direction)
        {
            bool isReduced = false;

            if(Entropy > 0)
            {
                List<EdgeType> connectors = new List<EdgeType>();
                foreach (var neighbour in neighbourPosibilities)
                {
                    connectors.Add(_tileRules[neighbour][(int)direction]);
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

                for (int i = _possibilities.Count - 1; i >= 0; i--)
                {
                    if (!connectors.Contains(_tileRules[_possibilities[i]][(int)oppositeSide]))
                    {
                        _possibilities.RemoveAt(i);
                        isReduced = true;
                    }
                }
                Entropy = _possibilities.Count;
            }
            
            return isReduced;
        }
    }

}
