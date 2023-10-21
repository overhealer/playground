using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playground
{
    public class LevelTile : MonoBehaviour
    {
        public EdgeType NorthEdge, EastEdge, SouthEdge, WestEdge;
        public float TileRandomWeight;

        public EdgeType GetEdgeTypeByDirection(EdgeDirection direction)
        {
            switch (direction)
            {
                case EdgeDirection.North:
                    return NorthEdge;
                case EdgeDirection.East:
                    return EastEdge;
                case EdgeDirection.South:
                    return SouthEdge;
                case EdgeDirection.West:
                    return WestEdge;
                default:
                    return NorthEdge;
            }
        }

        public EdgeType[] GetAllEdgeTypes()
        {
            EdgeType[] result = new EdgeType[4];
            result[0] = NorthEdge;
            result[1] = EastEdge;
            result[2] = SouthEdge;
            result[3] = WestEdge;
            return result;
        }
    }

    public enum EdgeType
    {
        Grass = 0,
        Road = 1,
        Cliff = 2
    }

    public enum EdgeDirection
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3
    }

}
