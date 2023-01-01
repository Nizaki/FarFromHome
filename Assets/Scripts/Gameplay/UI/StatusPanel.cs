using UnityEngine;
using UnityEngine.UI;

public class StatusPanel : MonoBehaviour
{
  public Slider hpBar;
  public Slider hungerBar;
  public Slider oxygenBar;
  public Slider waterBar;

  // Start is called before the first frame update
  private void Start()
  {
    hpBar.maxValue = GameManager.Instance.player.maxHp;
    hungerBar.maxValue = GameManager.Instance.player.maxHunger;
    oxygenBar.maxValue = GameManager.Instance.player.maxOxygen;
    waterBar.maxValue = GameManager.Instance.player.maxWater;
  }

  private void LateUpdate()
  {
    updateUi();
  }

  private void updateUi()
  {
    hpBar.value = GameManager.Instance.player.hp;
    hungerBar.value = GameManager.Instance.player.hunger;
    oxygenBar.value = GameManager.Instance.player.oxygen;
    waterBar.value = GameManager.Instance.player.water;
  }
}