using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public Slot[] slotList;

    private float TargetAlpha = 1;
    private CanvasGroup cg;

    private static Inventory _instance;
    public static Inventory GetInstance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.Find("KnapsackPanel").GetComponent<Inventory>();
            return _instance;
        }
        
    }

	// Use this for initialization
	void Start () {
        slotList = this.GetComponentsInChildren<Slot>();
        cg = this.GetComponent<CanvasGroup>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool StoreGood(int ID)
    {
        KnapsackGood good = KnapsackManager.GetInstance.GetGoodWithID(ID);
        return StoreGood(good);
    }

    public bool StoreGood(KnapsackGood good)
    {
        if (good == null)
            return false;
        Slot slot = FindEmptySlot();
        if (slot != null)
        {
            slot.StoreGood(good);
            return true;
        }
        return false;
    }

    private Slot FindEmptySlot()
    {
        foreach(Slot s in slotList)
        {
            if (s.transform.childCount == 0 || s.transform.gameObject.activeInHierarchy == false)
                return s;
        }
        Debug.Log("Is filled");
        return null;
    }

    public void ShowPanel()
    {
        this.cg.blocksRaycasts = true;
        this.cg.alpha = 1;
    }

    public void HidePanel()
    {
        this.cg.blocksRaycasts = false;
        this.cg.alpha = 0;
    }

    public void ChangePanelState()
    {
        this.cg.alpha = this.cg.alpha == 0 ? 1 : 0;
        this.cg.blocksRaycasts = this.cg.blocksRaycasts ? false : true;
    }

}
