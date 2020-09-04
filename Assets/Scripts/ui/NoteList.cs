﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteList : MonoBehaviour
{
    public NoteContent noteContent;
    public Udp udp;
    private IDictionary<string, Note> Notes { set; get; }
    private ConcurrentQueue<Note> inQueue = new ConcurrentQueue<Note>();
    private string currentNoteGuid;
    private string updateNoteGuid;

    // Start is called before the first frame update
    void Start()
    {
        Notes = new Dictionary<string, Note>();
    }

    // Update is called once per frame
    void Update()
    {
        Note note;
        if (inQueue.TryDequeue(out note))
        {
            AddNote(note.Guid, note.Name, note.Content);
        } 
        else if (updateNoteGuid != null)
        {
            noteContent.ShowContent(Notes[updateNoteGuid]);
            updateNoteGuid = null;
        }
    }

    public void SyncNote(string op, string guid, string name, string content)
    {
        if (op == "C")
        {
            inQueue.Enqueue(new Note
            {
                Guid = guid,
                Content = content,
                Name = name
            });
        }
        else
        {
            if (Notes.ContainsKey(guid))
            {
                Debug.Log("NoteList.Update." + currentNoteGuid + " and " + guid);
                Notes[guid].Content = content;
                if (currentNoteGuid == guid)
                {
                    updateNoteGuid = guid;
                }
            }
        }
    }

    public void Clear()
    {
        Debug.Log("NoteList.Clear.");
        Notes.Clear();
        int childCount = this.transform.childCount;
        Debug.Log("childCount = " + childCount);
        for (int i = childCount - 1; i >= 0; i--)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }
    }

    public void AddNote()
    {
        string guid = Guid.NewGuid().ToString();
        AddNote(guid, guid, guid);
        udp.BroadThread(System.Text.Encoding.Default.GetBytes("C|" + guid + "|" + guid + "|" + guid));
    }

    public void AddNote(string guid, string name, string content)
    {
        Debug.Log("NoteList.AddNote : " + guid + ", " + name + ", " + content);
        if (Notes.ContainsKey(guid))
        {
            Debug.Log("Notes.ContainsKey : " + Notes[guid]);
            return;
        }
        int index = Notes.Count;
        name = "笔记-" + index;
        Debug.Log("NoteList.AddNote index: " + index);
        Note note = new Note
        {
            Guid = guid,
            Content = content,
            Name = name
        };
        Notes[guid]=note;
        Debug.Log("NoteList.AddNote name: " + name);

        GameObject button = new GameObject(guid, typeof(Button), typeof(RectTransform), typeof(Text));
        Debug.Log("NoteList.AddNote button: " + name);
        button.transform.SetParent(this.transform);
        Debug.Log(this.transform.position.x + ", " + this.transform.position.y);
        button.GetComponent<Text>().text = name;
        button.GetComponent<Text>().font = this.gameObject.GetComponent<Text>().font;
        button.GetComponent<Text>().fontSize = 30;
        button.GetComponent<Text>().color = Color.black;
        button.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 60);
        button.GetComponent<RectTransform>().position = new Vector3(this.transform.position.x + 400, this.transform.position.y - (index + 1) * 60);
        Debug.Log("NoteList.AddNote y : " + (index * 20));
        Button _btn = button.GetComponent<Button>();
        _btn.onClick.AddListener(() =>
        {
            Debug.Log("NoteList.ClickNote : " + note.ToString());
            currentNoteGuid = note.Guid;
            noteContent.ShowContent(note);
        });
    }
}
