using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewNote : MonoBehaviour
{
    public NoteList noteList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateNote()
    {
        Debug.Log("NewNote.CreateNote.");
        noteList.AddNote();
    }
}
