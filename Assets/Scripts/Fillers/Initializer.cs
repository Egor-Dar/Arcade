using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fillers
{
    public class Initializer : MonoBehaviour
    {
        [SerializeField] private Pool fillersPool;
        [SerializeField] private Descriptor descriptor;
        [SerializeField] private List<Transform> positions;
        [SerializeField] private StopObserver stopObserver;
        private Dictionary<Element.Type, (float, float, Sprite)> _fillers;
        private List<Coroutine> _coroutines;

        private void Awake()
        {
            stopObserver.Subscribe(UpdateStop);
            _coroutines = new List<Coroutine>();
            _fillers = descriptor.GetFillers();
            fillersPool.InitializeFinished += StartCoroutines;
        }

        private void OnDestroy()
        {
            stopObserver.Unsubscribe(UpdateStop);
            StopCoroutines();
        }

        private void UpdateStop(bool value)
        {
            if (value == false)
            {
                if (_coroutines.Count != 0)
                {
                    StartCoroutines();
                }
                else
                {
                    StopCoroutines();
                    StartCoroutines();
                }
            }
            else
            {
                StopCoroutines();
            }
        }

        private void StartCoroutines()
        {
            if (_fillers == null) return;
            foreach (var filler in _fillers)
            {
                _coroutines.Add(StartCoroutine(StartInitialize(filler.Key, filler.Value.Item1, filler.Value.Item2,
                    filler.Value.Item3)));
            }
        }

        private void StopCoroutines()
        {
            if (_coroutines == null) return;
            foreach (var coroutine in _coroutines)
            {
                StopCoroutine(coroutine);
            }

            _coroutines.Clear();
        }

        private IEnumerator StartInitialize(Element.Type type, float delay, float count, Sprite sprite)
        {
            for (;;)
            {
                var instance = fillersPool.GetInstance();
                instance.SetType(type);
                instance.SetCount(count);
                instance.SetSprite(sprite);
                var index = Random.Range(0, positions.Count);
                instance.SetPosition(positions[index].position);
                instance.SetVisible(true);
                instance.Init();
                yield return new WaitForSeconds(delay);
            }
        }
    }
}