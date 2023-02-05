using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet", menuName = "Bullet")]
public class BulletSO : ScriptableObject
{
    [Header("Component Settings")]
    public float Health;
    public float Damage;

    [Header("Bullet Settings")]
    public Sprite Sprite;
    public Color Color;
    public Vector3 Size;

    [Header("Collider Settings")]
    public Vector2 ColliderSize;
}