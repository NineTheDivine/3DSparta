
public interface IDamageable
{
    public void TakeDamage(int amount, bool ignoreCooldown = false);
    public void OnDead();
}