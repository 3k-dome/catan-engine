using HexagonLib.Enums;

namespace HexagonLib;

public class EdgeCoordinate
{
    public VertexCoordinate VertexA { get; private set; }
    public VertexCoordinate VertexB { get; private set; }

    public EdgeCoordinate(VertexCoordinate vertexA, VertexCoordinate vertexB)
    {
        if (vertexA.Equals(vertexB))
        {
            throw new ArgumentException("Two distinct vertices are needed to form an edge.");
        }

        (VertexA, VertexB) = (vertexA, vertexB);
    }


    public EdgeCoordinate GetNeighbor(VertexCoordinate vertex, VertexNeighborAxis axis)
    {
        if (!Contains(vertex))
        {
            throw new ArgumentException("Vertex must be part of this edge!");
        }

        VertexCoordinate secondary = vertex.GetNeighbor(axis);

        return Contains(secondary)
            ? throw new ArgumentException("Outgoing vertex equals this edges seconday vertex!")
            : new(vertex, secondary);
    }

    public IEnumerable<EdgeCoordinate> Neighbors(VertexCoordinate outgoing)
    {
        if (!Contains(outgoing))
        {
            throw new ArgumentException("The outgoing vertex does not belong to this edge.");
        }

        foreach (EdgeCoordinate edge in outgoing.Edges())
        {
            if (!Equals(edge))
            {
                yield return edge;
            }
        }
    }

    public IEnumerable<EdgeCoordinate> Neighbors()
    {
        foreach (VertexCoordinate vertex in Vertices())
        {
            foreach (EdgeCoordinate edge in Neighbors(vertex))
            {
                yield return edge;
            }
        }
    }

    public VertexCoordinate Other(VertexCoordinate current)
    {
        return Contains(current)
            ? VertexA.Equals(current) ? VertexB : VertexA
            : throw new ArgumentException("The current vertex does not belong to this edge.");
    }

    public bool Contains(VertexCoordinate vertex)
    {
        return vertex.Equals(VertexA) || vertex.Equals(VertexB);
    }

    public IEnumerable<VertexCoordinate> Vertices()
    {
        yield return VertexA;
        yield return VertexB;
    }

    public override bool Equals(object? other)
    {
        return other != null
            && other is EdgeCoordinate coordinate
            && GetHashCode() == coordinate.GetHashCode();
    }

    public override int GetHashCode()
    {
        int hashA = VertexA.GetHashCode();
        int hashB = VertexB.GetHashCode();
        return hashA < hashB ? HashCode.Combine(hashA, hashB) : HashCode.Combine(hashB, hashA);
    }
}
