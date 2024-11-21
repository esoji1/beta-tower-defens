public interface IDamage
{
    public Health Health { get; }
    void Damage(int damage);
}