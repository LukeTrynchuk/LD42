using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboCorp.Core.Services;

namespace RoboCorp.Services
{
    public class WalletService : MonoBehaviour, IWalletService
    {
        public float Money => money;
        private float money;

        private void Awake()
        {
            RegisterService();
        }

        public void AddMoney(float value)
        {
            money += value;
        }

        public bool CanPurchase(float value)
        {
            if (money - value > 0) return true;
            return false;
        }

        public void RegisterService()
        {
            ServiceLocator.Register<IWalletService>(this);
        }

        public void SubtractMoney(float value)
        {
            money -= value;
        }
    }
}

