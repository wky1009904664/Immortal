using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler
{

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int GetGoodId()
    {
        if (this.transform.childCount == 0)
            return -1;
        GoodUI gu=this.transform.GetChild(0).GetComponent<GoodUI>();
        return gu.good.goodProperty.ID;
    }

    public void DestroyGoodUI()
    {
        foreach (Transform child in transform)
        {
            //Destroy(child.gameObject);
            DestroyImmediate(child.gameObject);
        }
    }

    public GameObject goodPrefab;
    public void StoreGood(KnapsackGood good)
    {
        if (this.transform.childCount == 0)
        {
            GameObject obj = GameObject.Instantiate(goodPrefab);
            obj.transform.SetParent(this.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;

            obj.GetComponent<GoodUI>().SetGood(good);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.transform.childCount > 0)
        {
            
            string text = this.transform.GetChild(0).GetComponent<GoodUI>().good.GetDescribe();

            KnapsackManager.GetInstance.ShowToolTilePanel(text); 
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (this.transform.childCount > 0)
            KnapsackManager.GetInstance.HideToolTilePanel();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (this.transform.childCount > 0)
        {
            GoodUI gu = this.transform.GetChild(0).GetComponent<GoodUI>();
            if (gu.good.goodProperty.canbeused == 1)
            {
                GameObject goodUI = this.transform.GetChild(0).GetComponent<GameObject>();
                KnapsackManager.GetInstance.HideToolTilePanel();
                DestroyImmediate(this.transform.GetChild(0).gameObject);
                Inventory.GetInstance.SaveInventory();
                Inventory.GetInstance.LoadInventory();
            }
        }
    }

}
