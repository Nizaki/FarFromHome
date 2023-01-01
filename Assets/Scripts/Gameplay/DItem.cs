using Assets.Scripts.Gameplay.Item;
using UnityEngine;

public class DItem : MonoBehaviour
{
  public SpriteRenderer itemRender;
  public Item itemDate;
  public int amount = 1;
  public LayerMask player;

  private void Start()
  {
    Invoke(nameof(DestroySelf), 600);
  }

  private void DestroySelf()
  {
    Destroy(this.gameObject);
  }

  private void OnDestroy()
  {
    CancelInvoke();
  }

  private void FixedUpdate()
  {
    RaycastHit2D hit = Physics2D.CircleCast(transform.position, 1.5f, Vector2.up, 20, player);

    if (hit.collider != null)
    {
      GetComponent<Rigidbody2D>().gravityScale = 0;
      transform.position = Vector2.MoveTowards(transform.position, hit.transform.position, 4 * Time.deltaTime);
      if (Vector2.Distance(transform.position, hit.transform.position) < 1)
      {
        hit.transform.GetComponent<Player>()?.AddItem(itemDate, amount);
        DestroySelf();
      }
    }
    else
    {
      GetComponent<Rigidbody2D>().gravityScale = 1;
    }
  }
}