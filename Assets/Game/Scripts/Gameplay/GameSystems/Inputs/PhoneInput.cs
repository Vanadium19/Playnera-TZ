using UnityEngine;

namespace Game.GameSystems.Inputs
{
    public class PhoneInput : IInputStrategy
    {
        public bool StartClick()
        {
            if (Input.touchCount == 1)
                return Input.GetTouch(0).phase == TouchPhase.Began;

            return false;
        }

        public bool EndClick()
        {
            if (Input.touchCount == 1)
                return Input.GetTouch(0).phase == TouchPhase.Ended;

            return false;
        }
    }
}