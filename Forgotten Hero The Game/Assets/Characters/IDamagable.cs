using UnityEngine;

public interface IDamageabe
{
    public void TakeDamage(float damage, Vector2? knockbackFore = null);
    public void MakeUntargatable();
}