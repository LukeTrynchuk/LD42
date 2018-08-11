using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboCorp.Gameboard
{
    /// <summary>
    /// The entity base class represents 
    /// any kind of object that can be
    /// placed, moved and manipulated 
    /// on the game board.
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {
        public abstract void Tick();
    }
}
