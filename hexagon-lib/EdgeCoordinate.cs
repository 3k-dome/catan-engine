namespace HexagonLib
{
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

        public IEnumerable<EdgeCoordinate> Neighbors()
        {
            VertexCoordinate[] vertices = new[] { VertexA, VertexB };
            foreach ((VertexCoordinate current, VertexCoordinate other) in Enumerable.Zip(vertices, vertices.Reverse()))
            {
                IEnumerable<VertexCoordinate> outgoing = current.Neighbors().Where(vertex => !other.Equals(vertex));

                foreach (VertexCoordinate vertex in outgoing)
                {
                    yield return new(current, vertex);
                }
            }
        }

        public IEnumerable<EdgeCoordinate> Traverse(VertexCoordinate outgoing)
        {
            return Contains(outgoing)
                ? outgoing.Edges().Except(new[] { this })
                : throw new ArgumentException("The outgoing vertex does not belong to this edge.");
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
            return other != null && other is EdgeCoordinate && GetHashCode() == other.GetHashCode();
        }

        public override int GetHashCode()
        {
            int hashA = VertexA.GetHashCode();
            int hashB = VertexB.GetHashCode();
            return hashA < hashB ? HashCode.Combine(hashA, hashB) : HashCode.Combine(hashB, hashA);
        }
    }
}
