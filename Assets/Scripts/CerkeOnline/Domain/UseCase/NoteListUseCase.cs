using System;
using System.Collections.Generic;
using UniRx;

namespace Azarashi.CerkeOnline.Domain.UseCase
{
    public class NoteListUseCase
    {
        public NoteListUseCase(INoteListRepository repository, INoteListPresenter presenter)
        {
            presenter.NoteListRequestAsObservable.Subscribe(callback => callback(repository.GetNoteList()));
        }
    }

    public interface INoteListRepository
    {
        IEnumerable<string> GetNoteList();
    }

    public interface INoteListPresenter
    {
        IObservable<Action<IEnumerable<string>>> NoteListRequestAsObservable { get; }
    }
}