using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using RoboCorp.Core.Services;

namespace RoboCorp.Services
{
    public class DateService : MonoBehaviour, IDateService
    {
        public string date => DateToString();

        private string Date;
        private DateTime dateTime = new DateTime(2050,1,1);

        private void Awake()
        {
            RegisterService();
        }

        public void IncrementDate()
        {
            dateTime.AddDays(1);
        }

        public void RegisterService()
        {
            ServiceLocator.Register<IDateService>(this);
        }

        private string DateToString()
        {
            return dateTime.ToString("M",null) + " " + dateTime.Year.ToString();
        }
    }
}

