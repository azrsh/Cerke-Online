using System;
using System.Collections.Generic;
using System.Linq;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;

namespace Azarashi.CerkeOnline.Domain.UseCase
{
    public class NotePlayerUseCase
    {
        readonly IGame game;
        readonly NoteData noteData;
        readonly IEnumerator<MovementData> movementEnumerator;

        public NotePlayerUseCase(IGame game, INoteRepository noteRepository)
        {
            noteData = noteRepository.GetNoteData();
            movementEnumerator = noteData.Movements.AsEnumerable().GetEnumerator();
            this.game = game;
        }

        public void NextMove()
        {
            var movement = movementEnumerator.Current;
            var valueProvider = new SequenceProvider(Enumerable.Range(movement.waterEntryCast, 1).Append(movement.steppingOverCast));
            game.Board.MovePiece(movement.start, movement.end, movement.via, game.CurrentPlayer, valueProvider, x =>
            {
                if (x.isSuccess)
                    movementEnumerator.MoveNext();
            });
        }

        private class SequenceProvider : IValueInputProvider<int>
        {
            readonly IEnumerable<int> sequence;
            int index = 0;

            public SequenceProvider(IEnumerable<int> sequence)
            {
                this.sequence = sequence;
            }

            public bool IsRequestCompleted { get; private set; } = false;
            public void RequestValue(Action<int> callback) => callback(sequence.Skip(index++).First());
        }
    }

    public interface INoteRepository
    {
        NoteData GetNoteData();
        void SetNoteData(NoteData noteData);
    }
}