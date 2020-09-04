using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NoteContent : MonoBehaviour
{
    public TextMeshProUGUI m_Text;
    public TMP_InputField inputField;
    public Udp udp;
    private Note note;
    private string content = "  Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla finibus ac nisl a "
        +"\r\n" + "rutrum. In vel elit at justo convallis pharetra. Aliquam sit amet scelerisque nisl,"
        +"\r\n" + " sed tempor velit. Morbi mattis lorem lorem, id ornare orci lacinia sit amet. Morbi "
        +"\r\n" + "iaculis tortor diam, sed facilisis nunc dapibus sit amet. Maecenas bibendum tempus "
        +"\r\n" + "metus et dictum. Fusce pharetra pulvinar lacinia. Duis a eros vitae lectus cursus "
        +"\r\n" + "commodo at ut erat. Orci varius natoque penatibus et magnis dis parturient montes, "
        +"\r\n" + "nascetur ridiculus mus. Nam fermentum dolor non metus sagittis lobortis."
        + "\r\n"
        +"\r\n" + "Quisque sem metus, malesuada quis tempus quis, efficitur vitae diam.Mauris enim risus,"
        +"\r\n" + " facilisis at egestas at, mattis et diam.Aliquam risus sapien, ornare quis venenatis ut, "
        +"\r\n" + "convallis vitae elit.Donec lacinia venenatis ornare. Duis molestie eros ipsum, nec dapibus"
        +"\r\n" + " libero imperdiet ut.Integer sollicitudin mi ac aliquet dictum. Ut purus leo, ultrices ac "
        +"\r\n" + "condimentum non, viverra id lectus.Suspendisse vel mi posuere, tristique lorem ut, venenatis "
        +"\r\n" + "ligula. Nulla lacinia ut nisi eget dapibus. Vivamus accumsan sed lectus et porttitor. Duis "
        +"\r\n" + "dapibus, massa a lacinia aliquam, elit leo finibus dui, ac fermentum odio justo at enim.";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnContentChange()
    {
        Debug.Log("NoteContent.OnContentChange : " + inputField.text);
        if (note != null)
        {
            note.Content = inputField.text;
            udp.BroadThread(System.Text.Encoding.Default.GetBytes("U|" + note.Guid + "|" + note.Name + "|" + note.Content));
        }
    }

    public void ShowContent(Note note)
    {
        this.note = note;
        Debug.Log("NoteContent.ShowContent : " + note.Content);
        m_Text.text = note.Content;
    }
}
