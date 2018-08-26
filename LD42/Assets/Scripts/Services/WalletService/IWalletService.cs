using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboCorp.Core.Services;

namespace RoboCorp.Services
{
    public interface IWalletService : IService
    {
        float Money { get; }
        void SubtractMoney(float value);
        void AddMoney(float value);

        bool CanPurchase(float value);
    }
}

