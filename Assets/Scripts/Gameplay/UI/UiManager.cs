using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public InvPanel invPanel;
    public FurnacePanel furnacePanel;
    public InvCraftingPanel invCraftingPanel;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            invPanel.gameObject.SetActive(!invPanel.gameObject.activeSelf);
            invPanel.CreateInv();
            invPanel.UpdateInv();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            furnacePanel.gameObject.SetActive(!furnacePanel.gameObject.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            invCraftingPanel.gameObject.SetActive(!invCraftingPanel.gameObject.activeSelf);
        }
    }
}