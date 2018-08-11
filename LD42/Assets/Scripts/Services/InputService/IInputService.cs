using RoboCorp.Core.Services;

namespace RoboCorp.Services
{
    /// <summary>
    /// The IInput Service is a contract 
    /// that all input services must implement.
    /// An input service is responsible for 
    /// detecting input and send out events for it.
    /// </summary>
    public interface IInputService : IService
    {
        event System.Action OnConfirmButton;

        bool IsConfirmButtonDown();
    }
}
