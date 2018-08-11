using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboCorp.Core.Services;

namespace RoboCorp.Services
{
    public class DateService : MonoBehaviour, IDateService
    {
        public void RegisterService()
        {
            ServiceLocator.Register<IDateService>(this);
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

