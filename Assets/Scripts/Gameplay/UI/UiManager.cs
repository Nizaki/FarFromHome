using UnityEngine;

public class UiManager : MonoBehaviour
{
  public static UiManager Instance;
  public InvPanel invPanel;
  public FurnacePanel furnacePanel;
  public InvCraftingPanel invCraftingPanel;
  public StorageBoxPanel storageBoxPanel;
  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else if (Instance != this)
    {
      Destroy(this);
    }
  }
  // Start is called before the first frame update
  private void Start()
  {
  }

  // Update is called once per frame
  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      GameManager.Instance.EnableControl();
      invCraftingPanel.gameObject.SetActive(false);
      invPanel.gameObject.SetActive(false);
      furnacePanel.gameObject.SetActive(false);
      storageBoxPanel.gameObject.SetActive(false);
    }

    if (Input.GetKeyDown(KeyCode.E))
    {
      GameManager.Instance.DisableControl();
      invPanel.gameObject.SetActive(true);
      invPanel.CreateInv();
      invPanel.UpdateInv();
    }
    if (Input.GetKeyDown(KeyCode.F))
    {
      GameManager.Instance.DisableControl();
      furnacePanel.gameObject.SetActive(true);
    }
    if (Input.GetKeyDown(KeyCode.C))
    {
      GameManager.Instance.DisableControl();
      invCraftingPanel.gameObject.SetActive(true);
    }
  }
}