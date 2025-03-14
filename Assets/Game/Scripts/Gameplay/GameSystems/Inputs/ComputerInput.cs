using UnityEngine;

namespace Game.GameSystems.Inputs
{
    public class ComputerInput : IInputStrategy
    {
        public bool StartClick()
        {
            return Input.GetMouseButtonDown(0);
        }

        public bool EndClick()
        {
            return Input.GetMouseButtonUp(0);
        }
    }
}