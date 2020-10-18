using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InfoPanel : MonoBehaviour
{
    public Image image;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.player.onBlockHover += UpdateUI;
    }

    void onDestroy()
    {
        GameManager.Instance.player.onBlockHover -= UpdateUI;
    }

    void UpdateUI(BlockBase block)
    {
        image.sprite = block.sprite ? block.sprite : null;
        text.text = block.name;
    }
}
