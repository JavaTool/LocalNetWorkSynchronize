using UnityEngine;
using UnityEngine.UI;

public class NoteContent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowContent(Note note)
    {
        Debug.Log("NoteContent.ShowContent : " + note.Content);
        this.transform.GetComponent<InputField>().text = note.Content;
    }
}
