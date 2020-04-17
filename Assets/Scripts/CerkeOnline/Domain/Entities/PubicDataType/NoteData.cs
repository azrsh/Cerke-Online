namespace Azarashi.CerkeOnline.Domain.Entities.PublicDataType
{
    public class NoteData
    {
        public NoteData(string black, string red, Terminologies.PieceColor first, Terminologies.Season season, MovementData[] movements)
        {
            Black = black;
            Red = red;
            First = first;
            Season = season;
            Movements = movements;
        }

        public string Black { get; }
        public string Red { get; }
        public Terminologies.PieceColor First { get; }
        public Terminologies.Season Season { get; }
        public MovementData[] Movements { get; }
    }

    public struct MovementData
    {
        public readonly IntegerVector2 start;
        public readonly IntegerVector2 via;
        public readonly IntegerVector2 end;
        public readonly Terminologies.PieceName piece;
        public readonly int waterEntryCast;
        public readonly int steppingOverCast;
        public readonly SeasonContinueOrEnd continueOrEnd;

        public MovementData(IntegerVector2 start, IntegerVector2 via, IntegerVector2 end, Terminologies.PieceName piece, int waterEntryCast, int steppingOverCast, SeasonContinueOrEnd continueOrEnd)
        {
            this.start = start;
            this.via = via;
            this.end = end;
            this.piece = piece;
            this.waterEntryCast = waterEntryCast;
            this.steppingOverCast = steppingOverCast;
            this.continueOrEnd = continueOrEnd;
        }
    }
}