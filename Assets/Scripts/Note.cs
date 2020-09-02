using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note
{
    public string Guid { set; get; }
    public string Content { set; get; }
    public string Name { set; get; }

    public string AppendContent(string str)
    {
        Content += str;
        return Content;
    }

    override public string ToString()
    {
        return "Note[guid=" + Guid + ", name=" + Name + "].";
    }
}
