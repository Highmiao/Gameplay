using Framework.CharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Framework.CharacterController
{
    public class PlayerInput : MonoBehaviour
    {
        public CharacterControllerBiped characterController;
        public string horizontal = "Horizontal";
        public string Vertical = "Vertical";

        void Update()
        {
            PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();

            // Build the CharacterInputs struct
            characterInputs.MoveAxisForward = Input.GetAxisRaw(Vertical);
            characterInputs.MoveAxisRight = Input.GetAxisRaw(horizontal);

            // Apply inputs to character
            characterController.SetInputs(ref characterInputs);
        }
    }

    public struct PlayerCharacterInputs
    {
        public float MoveAxisForward;
        public float MoveAxisRight;
    }
}
