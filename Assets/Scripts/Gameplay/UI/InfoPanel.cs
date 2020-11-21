using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
  public GameObject panel;
  public Image image;
  public Text text;

  // Start is called before the first frame update
  private void Start()
  {
    GameManager.Instance.player.onBlockHover += UpdateUI;
  }

  private void onDestroy()
  {
    GameManager.Instance.player.onBlockHover -= UpdateUI;
  }

  private void UpdateUI(BlockBase block)
  {
    if (block == null) return;
    if (block.sprite == null)
    {
      panel.gameObject.SetActive(false);
      return;
    }
    panel.gameObject.SetActive(true);
    image.sprite = block.sprite;
    text.text = block.name;
  }
}