using Pool;

namespace Bullet
{
    public class Pool : BasePool<Linker>
    {
        protected override void AddNewInstance()
        {
            var instance = Instantiate(prefab);
            instance.SetVisible(false);
            instance.TriggerDamage.DetectEnemy(true);
            instance.ReturnInPoolEvent += ReturnInPool;
            _instances.Enqueue(instance);
        }
    }
}

