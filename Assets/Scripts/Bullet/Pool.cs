using System.Collections.Generic;
using Pool;

namespace Bullet
{
    public class Pool : BasePool<Linker>
    {
        private bool _isDetectEnemy;

        protected override void Awake()
        {
            
        }

        public void Initialize(bool value)
        {
            _instances = new Queue<Linker>();
            _isDetectEnemy = value;
            for (int i = 0; i < countInAwake; i++)
            {
                AddNewInstance();
            }

            InitializeCallBack();
        }

        protected override void AddNewInstance()
        {
            var instance = Instantiate(prefab);
            instance.SetVisible(false);
            instance.TriggerDamage.DetectEnemy(_isDetectEnemy);
            instance.ReturnInPoolEvent += ReturnInPool;
            _instances.Enqueue(instance);
        }
    }
}