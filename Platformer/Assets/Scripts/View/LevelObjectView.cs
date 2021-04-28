using System;
using UnityEngine;


public class LevelObjectView : MonoBehaviour
{
    public Transform Transform;
    public SpriteRenderer SpriteRenderer;
    public Rigidbody2D Rigidbody2D;
    public Collider2D Collider2D;
    public TrailRenderer TrailRenderer;
    public event Action<LevelObjectView> OnTriggerEnter;
    public event Action<LevelObjectView> OnCollisionEnter;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var view = collision.GetComponent<LevelObjectView>();
        if (view != null)
            OnTriggerEnter?.Invoke(view);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        var view = collision.collider.GetComponent<LevelObjectView>();
        if (view != null)
            OnCollisionEnter?.Invoke(view);
    }
}
