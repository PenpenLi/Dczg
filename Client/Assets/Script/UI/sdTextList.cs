
using UnityEngine;
using System.Collections.Generic;
using System.Text;

public class sdTextList : MonoBehaviour
{
    void Start()
    {
    }

    const int MaxLine = 50;

    List<UILabel> textList = new List<UILabel>();

    public GameObject msg = null;

    void AddLabel(UILabel label)
    {
        if (textList.Contains(label)) return;
        textList.Add(label);
    }

    void RemoveLabel(UILabel label)
    {
        if (textList.Contains(label))
        {
            textList.Remove(label);
        }
    }

    public void AddText(string txt)
    {
        if (textList.Count == 0)
        {
            UILabel label = msg.GetComponent<UILabel>();
            label.text = txt;
            label.MakePixelPerfect();
            AddLabel(label);
            msg.name = "0";
        }
        else if (textList.Count >= MaxLine)
        {
            UILabel label = textList[0];
            textList.Remove(label);
            Vector3 pos = msg.transform.localPosition;
            UILabel temp = msg.GetComponent<UILabel>();
            pos.y -= temp.height;
            label.transform.localPosition = pos;
            label.text = txt;
            label.MakePixelPerfect();
            AddLabel(label);
            msg = label.gameObject;
        }
        else
        {
            GameObject obj = GameObject.Instantiate(msg) as GameObject;
            obj.transform.parent = msg.transform.parent;
            obj.name = (int.Parse(msg.name) + 1).ToString();
            Vector3 pos = msg.transform.localPosition;
            UILabel label = msg.GetComponent<UILabel>();
            pos.y -= label.height;
            obj.transform.localPosition = pos;
            label = obj.GetComponent<UILabel>();
            label.text = txt;
            label.MakePixelPerfect();
            AddLabel(label);
            msg = obj;
        }

        UIDraggablePanel panel = GetComponent<UIDraggablePanel>();
        float size = panel.bounds.size.y;
        if (size > 300)
        {
            Vector3 pos = panel.transform.localPosition;
            Vector3 pos1 = msg.transform.localPosition;
            pos.y = (Mathf.Abs(pos1.y) - 152 - pos.y);
            panel.MoveRelative(pos);
        }
    }
}
