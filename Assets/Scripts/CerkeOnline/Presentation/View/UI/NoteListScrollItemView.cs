using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Azarashi.CerkeOnline.Presentation.View.UI
{
    public class NoteListScrollItemView
    {
        [SerializeField] Text noteNameText;

        internal void SetNoteListItem(NoteListItem noteListItem)
        {
            noteNameText.text = "ファイル名 : " + noteListItem.name;
            
        }
    }
}