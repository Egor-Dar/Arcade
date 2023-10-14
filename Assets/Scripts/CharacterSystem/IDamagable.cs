namespace CharacterSystem
{
    public interface IDamagable
    {
        public bool IsEnemy { get; }
        public void TakeDamage(float value);
        public void TakeHeal(float value);
    }
}