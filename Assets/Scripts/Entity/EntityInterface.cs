
public interface IDamageable
{
    public void TakeDamage(int amount);
    public void OnDead();
}

public interface IAttackable
{
    public void ApplyDamage(IDamageable damageable, int damage);
}
