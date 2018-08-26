using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboCorp.Core.Services;

namespace RoboCorp.Services
{
    public interface ISoundService : IService {

        void Play(string id);
    }
}

