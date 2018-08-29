using RoboCorp.Core.Services;

namespace RoboCorp.Services
{
    /// <summary>
    /// IEntity Manager is a contract 
    /// that all entity managers must
    /// implement. An entity manager
    /// is a manager that contains 
    /// all the type of entities in 
    /// the game. 
    /// </summary>
    public interface IEntityManager : IService
    {
        EntityInstance[] GetEntities();
    }
}
