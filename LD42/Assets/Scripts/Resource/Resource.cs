﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboCorp.Core.Services;
using RoboCorp.Services;

namespace RoboCorp.Resources
{
    /// <summary>
    /// A resource is a unit of something
    /// that is used to create other higher
    /// value items.
    /// </summary>
    public abstract class Resource : MonoBehaviour
    {
        public Vector3 TranslatePoint;
        private ServiceReference<ITickService> tickService = new ServiceReference<ITickService>();
        private float currentTime = 0;
        #region Main Methods
        public void SetTargetPosition(Vector3 targetPosition) => TranslatePoint = targetPosition;

        private void OnEnable()
        {
            tickService.AddRegistrationHandle(RegisterTick);
            currentTime = 0;
        }
        private void OnDisable()
        {
            if (tickService.isRegistered())
            {
                tickService.Reference.OnTick -= OnTick;
            }
        }

        private void OnTick()
        {
            currentTime = 0;
        }

        private void Update()
        {
            currentTime += Time.deltaTime;
            this.transform.position = Vector3.Lerp(this.transform.position, TranslatePoint, Mathf.Clamp01(currentTime / (tickService.Reference.TickLength * 0.5f)));
        }
        #endregion

        #region Utility Methods
        private void RegisterTick()
        {
            tickService.Reference.OnTick -= OnTick;
            tickService.Reference.OnTick += OnTick;
        }
        #endregion
    }
}
