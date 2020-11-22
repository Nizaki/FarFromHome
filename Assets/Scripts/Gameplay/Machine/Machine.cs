using UnityEngine;

public class Machine : MonoBehaviour
{
  private Rigidbody2D rb2d;
  public string dropItemId;
  public int amount = 1;

  [SerializeField]
  private bool breakOnNoGround = true;

  public virtual void Awake()
  {
    rb2d = GetComponent<Rigidbody2D>();
  }

  public virtual void OnUse(Player player)
  {
  }

  private void LateUpdate()
  {
    if (breakOnNoGround)
      if (rb2d.velocity.y < -2f)
        Destroy(this.gameObject);
  }

  private void OnDestroy()
  {
  }

  public virtual void Break()
  {
    GameManager.Instance.SpawnItemByID(transform.position, dropItemId, amount);
    Destroy(this.gameObject);
  }
}