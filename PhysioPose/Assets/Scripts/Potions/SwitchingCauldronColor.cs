using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingCauldronColor : MonoBehaviour
{
    public Material SwitchingMat;

    public Material WaterMat;

    private MeshRenderer rend;
    
    private Color InitialColor;

    public Stirring StirringScript;

    public Color ColorSelected;

    private Color TmpColor;

    public Cauldron CauldronScript;

    public bool IsCauldronLocked;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        IsCauldronLocked = false;

        InitialColor = new Color(1, 1, 1);
        InitialColor.a = 40f / 255f;
        WaterMat.color = InitialColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (StirringScript.StirredBegin && !IsCauldronLocked)
        {
            CauldronScript.LockingCauldron();
            IsCauldronLocked = true;
        }

        if (StirringScript.Percentage > 0f && StirringScript.Percentage <= 1f)
        {
            rend.material = SwitchingMat;
            TmpColor.a = Mathf.Lerp(0, 1, StirringScript.Percentage);
            TmpColor.r = Mathf.Lerp(0, ColorSelected.r, StirringScript.Percentage);
            TmpColor.g = Mathf.Lerp(0, ColorSelected.g, StirringScript.Percentage);
            TmpColor.b = Mathf.Lerp(0, ColorSelected.b, StirringScript.Percentage);
            SwitchingMat.color = TmpColor;
        }

        if (StirringScript.Percentage >= 1 && IsCauldronLocked)
        {
            CauldronScript.UnlockingAndClearCauldron();
            IsCauldronLocked = false;
        }
    }

    public void EmptyTheCauldron()
    {
        rend.material = WaterMat;
    }
    
    
}
