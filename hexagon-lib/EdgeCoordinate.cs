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

        public bool Contains(VertexCoordinate vertex)
        {
            return vertex == VertexA || vertex == VertexB;
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
