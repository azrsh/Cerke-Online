using System;
using System.Linq;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Azarashi.CerkeOnline.Domain.UseCase;

namespace Azarashi.CerkeOnline.Presentation.View.UI
{
    internal class NoteListItem
    {
        public string name;
        public string rulesetName;
        public string redPlayer;
        public string blackPlayer;
        public string first;
    }

    public class ReplayNoteButtonView : MonoBehaviour, INoteListPresenter
    {
        [SerializeField] Button button = default;
        [SerializeField] RectTransform scrollViewContentsParent = default;
        [SerializeField] GameObject contentPrefab = default;
        
        public IObservable<Action<IEnumerable<string>>> NoteListRequestAsObservable => noteListRequestSubject;
        Subject<Action<IEnumerable<string>>> noteListRequestSubject = new Subject<Action<IEnumerable<string>>>();

        IEnumerable<GameObject> noteListButtons = Enumerable.Empty<GameObject>();

        void Start() => noteListRequestSubject.OnNext(SetupNoteList);

        void SetupNoteList(IEnumerable<string> noteList)
        {
            UpdateNoteListUI(noteList.Select(name => new NoteListItem() { name = name }));

            button.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(_ =>
            {
                //preGameSettings.OnStartButton();
            });
        }

        

        void NotifySelectedNote(string note)
        {
            Debug.Log("Pushed " + note);
        }

        void UpdateNoteListUI(IEnumerable<NoteListItem> list)
        {
            ClearNoteListObjects();
            GenerateNoteList(list);
        }

        //ワールドリストのGUIを生成（ワールド選択画面）
        void GenerateNoteList(IEnumerable<NoteListItem> items)
        {
            scrollViewContentsParent.sizeDelta = new Vector2(scrollViewContentsParent.sizeDelta.x, items.Count() * 75f);    //スクロールビューのサイズ調整
            noteListButtons = items.Do(GenerateNoteListItemObject);
        }

        GameObject GenerateNoteListItemObject(NoteListItem item, int index)
        {
            GameObject obj = GameObject.Instantiate(contentPrefab) as GameObject;
            obj.transform.SetParent(scrollViewContentsParent.transform);
            SetTransform(obj.GetComponent<RectTransform>(), index);

            Button button = obj.GetComponent<Button>();
            button.OnClickAsObservable().Subscribe(_ => NotifySelectedNote(item.name));
            obj.GetComponent<NoteListScrollItemView>().SetNoteListItem(item);
            
            return obj;
        }

       void SetTransform(RectTransform rect, int index)
       {
            rect.sizeDelta = new Vector2(scrollViewContentsParent.sizeDelta.x, 70f);
            rect.anchoredPosition = Vector3.zero;
            rect.localPosition = new Vector3(rect.localPosition.x,
                                             rect.localPosition.y - scrollViewContentsParent.sizeDelta.y / 2f + rect.sizeDelta.y / 2f + index * 75f,
                                             rect.localPosition.z);
        }

        void ClearNoteListObjects()
        {
            foreach (var item in noteListButtons.Where(item => item != null))
                Destroy(item);
        }
    }

    internal static class LinqExtension
    {
        public static IEnumerable<TResult> Do<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> function)
        {
            return enumerable.Select(function).ToArray();
        }
        public static IEnumerable<TResult> Do<T, TResult>(this IEnumerable<T> enumerable, Func<T, int, TResult> function)
        {
            int index = 0;
            return enumerable.Select(item => function(item, index++)).ToArray();
        }

        public static void Do<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
                action(item);
        }
    }
}