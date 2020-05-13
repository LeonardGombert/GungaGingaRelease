using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kubika.Game
{
    public class ButtonCaller : MonoBehaviour
    {        
        public void ButtonCallback(string button)
        {
            switch (button)
            {
                #region //GAME INPUTS
                case "GAME_RotateRight":
                    UIManager.instance.CheckButton(ButtonCalls.RotateRight);
                    break;

                case "GAME_RotateLeft":
                    UIManager.instance.CheckButton(ButtonCalls.RotateLeft);
                    break;

                case "GAME_Restart":
                    UIManager.instance.CheckButton(ButtonCalls.Restart);
                    break;

                case "GAME_Undo":
                    UIManager.instance.CheckButton(ButtonCalls.Undo);
                    break;

                case "GAME_BurgerMenu":
                    UIManager.instance.CheckButton(ButtonCalls.BurgerMenu);
                    break;
                #endregion

                #region //WORLDMAP INPUTS
                case "WORLDMAP_Play":
                    UIManager.instance.CheckButton(ButtonCalls.Play);
                    break;

                case "WORLDMAP_TurnRight":
                    UIManager.instance.CheckButton(ButtonCalls.TurnRight);
                    break;

                case "WORLDMAP_TurnLeft":
                    UIManager.instance.CheckButton(ButtonCalls.TurnLeft);
                    break;
                #endregion

                #region //BURGER MENU
                case "BURGER_Sound":
                    UIManager.instance.CheckButton(ButtonCalls.SoundOnOff);
                    break;

                case "BURGER_Music":
                    UIManager.instance.CheckButton(ButtonCalls.MusicOnOff);
                    break;

                case "BURGER_Close":
                    UIManager.instance.CheckButton(ButtonCalls.CloseMenu);
                    break;
                #endregion

                case "MAIN_MENU":
                    UIManager.instance.CheckButton(ButtonCalls.MainMenu);
                    break;

                default:
                    break;
            }
        }
    }
}
