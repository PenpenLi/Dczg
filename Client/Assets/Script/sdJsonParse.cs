using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class JsonNode
{
	public int level	=	0;
	public string name	=	"";
	public string value	=	"";
	//public Hashtable 	values	=	new Hashtable();
	public List<JsonNode>	children=	new List<JsonNode>();
	static string FindBlock(char begin,char end,string text,int last,ref int current)
	{

		int block = 1;
		last = current;
		while (true) 
		{
			char c = text [current];

			if (c == begin && begin!=0) {
				block++;
			}
			else if(end == c)
			{
				block--;
				if(block==0)
				{
					int endIndex = current-1;
					current++;
					return text.Substring(last,endIndex-last+1);
				}
			}

			current++;

		}
		return "";
	}
    static string FindEnd(char end, string text, int last, ref int current)
    {
        last = current;
        while (true)
        {
            if (current >= text.Length)
            {
                return text.Substring(last, text.Length - last);
            }
            char c = text[current];

            if (end == c)
            {
                int endIndex = current-1;
                current++;
                return text.Substring(last, endIndex - last + 1);
                
            }
            current++;
        }
        return "";
    }
	public bool Parse(string text,ref int current)
	{
		//current++;
		int last = current;
		string key = "";
		while (true) 
		{

			if(current>=text.Length)
			{
				return true;
			}
			char c = text [current];
			if (c == '{') 
			{
				current++;
				string block =	FindBlock('{','}',text,last,ref current);
				JsonNode n = new JsonNode();
				n.level = level+1;
				children.Add(n);
				int i=0;
				n.Parse(block,ref i);
			}
			if (c == '[') 
			{
				current++;
				string block =	FindBlock('[',']',text,last,ref current);
				JsonNode n = new JsonNode();
				n.level = level+1;
				children.Add(n);
				int i=0;
				n.Parse(block,ref i);
			}
			else if(c == ':')
			{
				key	=	text.Substring(last,current-last).Replace("\"","");
				current++;
				if(text[current]=='[')
				{
					current++;last=current;
					string block =	FindBlock('[',']',text,last,ref current);
					JsonNode n = new JsonNode();
					n.level = level+1;
					int i=0;
					n.Parse(block,ref i);
					n.name	=	key;
					children.Add(n);
				}
				else if(text[current]=='{')
				{
					current++;last=current;
					string block =	FindBlock('{','}',text,last,ref current);
					JsonNode n = new JsonNode();
					n.level = level+1;
					int i=0;
					n.Parse(block,ref i);
					n.name	=	key;
					children.Add(n);
				}
                else if (text[current] == '\"')
                {
                    current++; last = current;
                    string block = FindBlock((char)0, '\"', text, last, ref current);
                    //values.Add(key,block.Replace("\"",""));
                    JsonNode n = new JsonNode();
                    n.level = level + 1;
                    n.name = key;
                    n.value = block.Replace("\"", "");
                    children.Add(n);
                }
                else
                { 
                    last = current;
                    string val = FindEnd( ',', text, last, ref current);
                    JsonNode n = new JsonNode();
                    n.level = level + 1;
                    n.name = key;
                    n.value = val;
                    children.Add(n);
                }
				last = current+1;
				/*
				string line	=	text.Substring(last,current-1).Replace("\"","");
				last	=	current+1;
				string[] keyValue	=	line.Split(':');
				values.Add(keyValue[0],keyValue[1]);
				Debug.Log(keyValue[0]+"   "+keyValue[1]);
				*/
			} else if(c== ',')
			{

				current++;
				last = current;
			}
			else
			{
				current++;
			}

		}
	}
	public void DebugPrint()
	{
		Debug.Log (level + " " + name + ":" + value);
		foreach (JsonNode n in children) 
		{
			n.DebugPrint();
		}
	}
	public JsonNode	Find(string strName)
	{
		foreach (JsonNode n in children) 
		{
			if(n.name == strName)
			{
				return n;
			}
		}
		foreach (JsonNode n in children) 
		{
			JsonNode node = n.Find(strName);
			if(node!=null)
			{
				return node;
			}
		}
		return null;
	}
	public void	FindList(string strName,List<JsonNode> lst)
	{
		foreach (JsonNode n in children) 
		{
			if(n.name == strName)
			{
				lst.Add(n);
			}
			n.FindList(strName,lst);
		}
	}
	public void	FindListHasAttibuteName(string strName,List<JsonNode> lst)
	{
		foreach (JsonNode n in children) 
		{
			if(n.name == strName)
			{
				lst.Add(this);
				break;
			}
			n.FindListHasAttibuteName(strName,lst);
		}
	}

	public string Attribute(string strName)
	{
		foreach (JsonNode n in children) 
		{
			if(n.name == strName)
			{
				return n.value;
			}
		}
		return "";
	}
	public void	FindListAttribute(string strName,List<string> lst)
	{
		foreach (JsonNode n in children) 
		{
			if(n.name == strName)
			{
				lst.Add(n.value);
			}
			n.FindListAttribute(strName,lst);
		}
	}
}

public class JsonParse{

    public  List<string> cdnlist = new List<string>();
    public  List<JsonNode> serverlist = new List<JsonNode>();
    public List<string> Pushlist = new List<string>();
    public  string notice;
    public string defaultServer;

	public  void Parse(string text)
	{
		int current = 0;
        JsonNode js = new JsonNode();
		js.Parse (text, ref current);
		JsonNode areas = js.Find ("Areas");
		notice = areas.Attribute ("AreaNotice");
        defaultServer = areas.Attribute("DefaultServerID");
		areas.FindListAttribute ("address",cdnlist);
		areas.FindListHasAttibuteName ("ServerID", serverlist);
        //areas.FindListHasAttibuteName("ServerStatus", serverlist);

        JsonNode PushString = areas.Find("PushString");
        if (PushString != null)
        {
            PushString.FindListAttribute("content", Pushlist);
        }
	}
}
