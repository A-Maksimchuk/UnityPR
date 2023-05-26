using System;
using EnemyLogic;
using Services;
using UnityEngine;

namespace DI
{
    public class ServiceRegistrar:MonoBehaviour
    {
        private void Awake()
        {
            Container.Register<IInputService>(new GameObject("InputService").AddComponent<InputService>());
        }
    }
}