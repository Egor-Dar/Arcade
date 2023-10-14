using System.Collections;
using UnityEngine;

namespace Bullet
{
    public class Initializer : MonoBehaviour
    {
        [SerializeField] private Pool pool;
        [SerializeField] private float flySpeed;
        [SerializeField] private Transform enemy;
        private float _damage;
        private Coroutine _spawn;
        private bool _isStop;
        public void UpdateStop(bool value) => _isStop = value;

        private void Awake()
        {
            _spawn = StartCoroutine(InitLoop());
        }

        private void OnDestroy()
        {
            if (_spawn != null) StopCoroutine(_spawn);
        }

        public void UpdateDamage(float damage) => _damage = damage;

        public void InitKritBullet(int coef)
        {
            var instance = pool.GetInstance();
            instance.SetDamage(_damage * coef);
            instance.IsCrit(true);
            instance.SetPosition(transform.position);
            instance.Init();
            var direction = (enemy.position - transform.position).normalized;
            instance.AddForce(direction * flySpeed);
        }

        private IEnumerator InitLoop()
        {
            for (;;)
            {
                yield return new WaitForSeconds(0.5f);
                if (_isStop) continue;
                var instance = pool.GetInstance();
                instance.SetDamage(_damage);
                instance.IsCrit(false);
                instance.SetPosition(transform.position);
                var direction = (enemy.position - transform.position).normalized;
                instance.AddForce(direction * flySpeed);
                instance.Init();
            }
        }
    }
}