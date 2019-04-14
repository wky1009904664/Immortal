using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTilePanel : MonoBehaviour {

    private float alpha = 0.0f;
    private float alphaSpped = 10.0f;

    private Text toolTilePanel;
    private Text toolTileText;
    private CanvasGroup cg;

	// Use this for initialization
	void Start () {
        toolTilePanel = this.transform.GetComponent<Text>();
        cg = this.transform.GetComponent<CanvasGroup>();
        toolTileText = this.transform.GetChild(1).GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (alpha != cg.alpha)
        {
            cg.alpha = Mathf.Lerp(cg.alpha, alpha, alphaSpped * Time.deltaTime);
            if (Mathf.Abs(alpha - cg.alpha) < 0.01f)
                cg.alpha = alpha;
        }
	}
    public void ShowPanel(string str = " ")
    {
        toolTilePanel.text = str;
        toolTileText.text = str;
        alpha = 1;
    }

    public void HidePanel()
    {
        alpha = 0;
    }

    public void SetLocalPosition(Vector3 point)
    {
        this.transform.localPosition = point;
    }

}
