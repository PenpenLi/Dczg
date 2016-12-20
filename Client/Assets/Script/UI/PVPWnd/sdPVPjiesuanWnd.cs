using UnityEngine;
using System.Collections;

public class sdPVPjiesuanWnd : MonoBehaviour
{
    UILabel label_congratulation = null;
    UILabel label_score = null;
    UILabel label_reputation = null;

    void Awake()
    {
        GameObject Root = GameObject.Find("Sprite_jiesuan_bg");
        label_congratulation = Root.transform.FindChild("Label_jiesuan_congratulation").GetComponent<UILabel>();
        label_score = Root.transform.FindChild("Label_jieusan_score").GetComponent<UILabel>();
        label_reputation = Root.transform.FindChild("Label_jiesuan_reputation").GetComponent<UILabel>();
    }
    // Use this for initialization
    void Start()
    {
        if (sdPVPManager.Instance.mbWin)
        {
            label_congratulation.text = "恭喜你挑战成功！";
        }
        else
        {
            label_congratulation.text = "挑战失败，加油吧！";
        }
        if (sdPVPManager.Instance.nJiesuan_score >= 0)
            label_score.text = "[2DD212]" + "+" + sdPVPManager.Instance.nJiesuan_score;
        else
            label_score.text = "[FF0000]" + "-" + (-1 * sdPVPManager.Instance.nJiesuan_score).ToString();
        
        if (sdPVPManager.Instance.nJiesuan_reputation >= 0)
            label_reputation.text = "[2DD212]" + "+" + sdPVPManager.Instance.nJiesuan_reputation;
        else
            label_reputation.text = "[FF0000]" + "-" + (-1 * sdPVPManager.Instance.nJiesuan_reputation).ToString();
    }

    void OnClick()
    {
		if (gameObject.name == "Button_quit" || gameObject.name == "Button_quit2")
        {
            GameObject root = GameObject.Find("Sprite_jiesuan_bg");
            root.transform.parent.gameObject.SetActive(false);
            //sdUICharacter.Instance.TuiTu_To_WorldMap();
            //sdUICharacter.Instance.nType = 1;
            sdUICharacter.Instance.ShowPVPMain(true,false);
            GameObject.Destroy(root.transform.parent.gameObject);
        }
    }
}
