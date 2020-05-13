using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kubika.Game
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager _instance;
        public static UIManager instance { get { return _instance; } }

        void Awake()
        {
            if (_instance != null && _instance != this) Destroy(this);
            else _instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void CheckButton(ButtonCalls buttonCalls)
        {
            switch (buttonCalls)
            {
                case ButtonCalls.RotateRight:
                    break;

                case ButtonCalls.RotateLeft:
                    break;

                case ButtonCalls.Restart:
                    break;

                case ButtonCalls.Undo:
                    break;

                case ButtonCalls.BurgerMenu:
                    break;

                case ButtonCalls.SoundOnOff:
                    break;

                case ButtonCalls.MusicOnOff:
                    break;

                case ButtonCalls.CloseMenu:
                    break;

                case ButtonCalls.TurnRight:
                    break;

                case ButtonCalls.TurnLeft:
                    break;

                case ButtonCalls.Play:
                    break;

                case ButtonCalls.MainMenu:
                    break;

                default:
                    break;
            }
        }
    }
}
