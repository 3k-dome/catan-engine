using CatanLib.Enums;

namespace CatanLib.Interfaces.Interaction
{
    public interface IPieceCollection
    {
        Dictionary<PieceType, int> Pieces { get; }

        void PlacePiece(PieceType piece)
        {
            if (piece == PieceType.None) { return; }

            if (Pieces[piece] <= 0)
            {
                throw new Exception("Player can't spend any more of this Resource!");
            }

            Pieces[piece]--;
        }

        void TakePiece(PieceType piece)
        {
            if (piece == PieceType.None) { return; }

            Pieces[piece]++;
        }

        bool HasPiece(PieceType piece)
        {
            return piece == PieceType.None || Pieces[piece] > 0;
        }
    }
}
