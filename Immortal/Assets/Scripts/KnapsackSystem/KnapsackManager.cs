using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class KnapsackManager : MonoBehaviour {

    private static KnapsackManager _instance;
    public static KnapsackManager GetInstance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("KnapsackManager").GetComponent<KnapsackManager>();
            }
            return _instance;
        }
    }

    private bool isShowToolTilePanel = false;

    private List<KnapsackGood> goodlist;
    private void Awake()
    {
        ParseGoodJson();
    }

    private GameObject toolTilePanel;
    private GameObject canvase;

    // Use this for initialization
    void Start () {
        toolTilePanel = GameObject.Find("ToolTilePanel");
        canvase = GameObject.Find("KnapsackCanvas");
	}
	
	// Update is called once per frame
	void Update () {
        if (isShowToolTilePanel)
        {
            Vector2 point = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvase.GetComponent<Canvas>().transform as RectTransform, Input.mousePosition, null, out point);
            toolTilePanel.GetComponent<ToolTilePanel>().SetLocalPosition(point + new Vector2(40, -15));//Panel appear in right bottom of the mouse
        }
	}

    void ParseGoodJson()
    {
        goodlist = new List<KnapsackGood>();

        string path = Application.streamingAssetsPath + "/GoodJson.json";

        StreamReader sr = new StreamReader(path);
        string json = sr.ReadToEnd();
        sr.Close();

        JsonData data = JsonMapper.ToObject(json);
        for(int i = 0; i < data.Count; i++)
        {
           
            BaseProperty gp = JsonMapper.ToObject<BaseProperty>(data[i]["goodProperty"].ToJson());
            
            KnapsackGood kg = new KnapsackGood(gp);
            goodlist.Add(kg);
        }
    }

    public KnapsackGood GetGoodWithID(int ID)
    {
        foreach(KnapsackGood good in goodlist)
        {
            if (ID == good.goodProperty.ID)
                return good;
        }

        return null;
    }



    public void ShowToolTilePanel(string str=" ")
    {
        isShowToolTilePanel = true;
        toolTilePanel.GetComponent<ToolTilePanel>().ShowPanel(str); 
    }

    public void HideToolTilePanel()
    {
        isShowToolTilePanel = false;
        toolTilePanel.GetComponent<ToolTilePanel>().HidePanel();
    }

}
