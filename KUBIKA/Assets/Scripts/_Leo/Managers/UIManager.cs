﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

namespace Kubika.Game
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager _instance;
        public static UIManager instance { get { return _instance; } }

        /*[SerializeField] Button turnRight;
        [SerializeField] Button turnLeft;

        [SerializeField] Button play;
        [SerializeField] Button mainMenu;

        [SerializeField] Button rotateRight;
        [SerializeField] Button rotateLeft;
        [SerializeField] Button restart;
        [SerializeField] Button undo;

        [SerializeField] Button burgerMenu;
        [SerializeField] Button closeMenu;*/

        //Game Canvas
        [SerializeField] Canvas gameCanvas;

        //Burger Menu
        [SerializeField] Canvas hamburgerMenuCanvas;
        [SerializeField] Button music;
        [SerializeField] Sprite musicOn;
        [SerializeField] Sprite musicOff;
        private bool musicIsOn = true;

        [SerializeField] Button sound;
        [SerializeField] Sprite soundOn;
        [SerializeField] Sprite soundOff;
        private bool soundIsOn = true;

        //Transition Canvas
        [SerializeField] Canvas transitionCanvas;
        [SerializeField] Image fadeImage;

        [SerializeField] TransitionType transitionType;
        [SerializeField] GameObject hiddenMenuButtons;
        [SerializeField] GameObject openBurgerMenuButton;

        //Transition Tween
        [SerializeField] float transitionDuration;
        [SerializeField] float timePassed;
        float startAlphaValue;
        float targetAlphaValue;
        bool gameDimmed = false;

        //Win Canvas
        [SerializeField] Canvas winCanvas;

        //World Map Canvas
        [SerializeField] Canvas worldMapCanvas;

        void Awake()
        {
            if (_instance != null && _instance != this) Destroy(this);
            else _instance = this;

            RefreshActiveScene();
        }

        public void RefreshActiveScene()
        {
            TurnOffAllCanvases();

            switch (ScenesManager.instance.currentActiveScene)
            {
                /*case ScenesIndex.MANAGER:
                    break;

                case ScenesIndex.USER_INTERFACE:
                    break;*/

                case ScenesIndex.TITLE_WORLD_MAP:
                    WorldMapPriority();
                    break;

                case ScenesIndex.GAME_SCENE:
                    GameCanvasPriority();
                    break;

                case ScenesIndex.WIN:
                    WinScreenSettings();
                    break;

                case ScenesIndex.LEVEL_EDITOR:
                    break;

                case ScenesIndex.CUSTOM_LEVELS:
                    break;

                case ScenesIndex.CREDITS:
                    break;

                default:
                    break;
            }
        }

        public void TransitionStart()
        {
            ResetCanvasSortOrder();
            fadeImage.enabled = true;
            transitionCanvas.sortingOrder = 9999;
        }

        public void TransitionOver()
        {
            transitionCanvas.sortingOrder = 0;
            transitionCanvas.enabled = false;
        }

        void ResetCanvasSortOrder()
        {
            worldMapCanvas.sortingOrder = 0;
            transitionCanvas.sortingOrder = 0;
            gameCanvas.sortingOrder = 0;
            winCanvas.sortingOrder = 0;
        }

        void TurnOffAllCanvases()
        {
            worldMapCanvas.enabled = false;
            //transitionCanvas.enabled = false;
            fadeImage.enabled = false;
            gameCanvas.enabled = false;
            winCanvas.enabled = false;
            hamburgerMenuCanvas.enabled = false;
        }

        private void WorldMapPriority()
        {
            ResetCanvasSortOrder();
            if (worldMapCanvas != null) worldMapCanvas.enabled = true;
            worldMapCanvas.sortingOrder = 1000;
        }

        private void GameCanvasPriority()
        {
            ResetCanvasSortOrder();
            gameCanvas.enabled = true;

            if (hamburgerMenuCanvas != null) hamburgerMenuCanvas.enabled = true;

            hiddenMenuButtons.SetActive(false);
            gameCanvas.sortingOrder = 1000;

            //Checking if the current level has ROtation enabled
            /*if (!_LoaderQueuer.instance._hasRotate) foreach (Button item in RotateButtons) item.gameObject.SetActive(false);
            else if (_LoaderQueuer.instance._hasRotate) foreach (Button item in RotateButtons) item.gameObject.SetActive(true);*/
        }

        private void WinScreenSettings()
        {
            ResetCanvasSortOrder();
            winCanvas.enabled = true;
            winCanvas.sortingOrder = 1000;
        }

        public void ButtonCallback(string button)
        {
            Debug.Log("Checking");

            switch (button)
            {
                #region //GAME INPUTS
                case "GAME_RotateRight":
                    break;

                case "GAME_RotateLeft":
                    break;

                case "GAME_Restart":
                    LevelsManager.instance.RestartLevel();
                    break;

                case "GAME_Undo":
                    break;

                case "GAME_BurgerMenu":
                    StartCoroutine(DimGame());
                    break;
                #endregion

                #region //WORLDMAP INPUTS
                case "WORLDMAP_Play":
                    ScenesManager.instance._LoadScene(ScenesIndex.GAME_SCENE);
                    break;

                case "WORLDMAP_TurnRight":
                    break;

                case "WORLDMAP_TurnLeft":
                    break;
                #endregion

                #region //BURGER MENU
                case "BURGER_Sound":
                    soundIsOn = !soundIsOn;
                    SwitchButtonSprite();
                    break;

                case "BURGER_Music":
                    musicIsOn = !musicIsOn;
                    SwitchButtonSprite();
                    break;

                case "BURGER_Close":
                    StartCoroutine(DimGame());
                    break;
                #endregion

                case "MAIN_MENU":
                    ScenesManager.instance._LoadScene(ScenesIndex.TITLE_WORLD_MAP);
                    break;

                default:
                    break;
            }
        }

        void SwitchButtonSprite()
        {
            if (musicIsOn == true) music.image.sprite = musicOn;
            if (musicIsOn == false) music.image.sprite = musicOff;

            if (soundIsOn == true) sound.image.sprite = soundOn;
            if (soundIsOn == false) sound.image.sprite = soundOff;
        }

        IEnumerator DimGame()
        {
            ResetCanvasSortOrder();

            if (gameDimmed == false)
            {
                fadeImage.enabled = true;
                openBurgerMenuButton.SetActive(false);
                hiddenMenuButtons.SetActive(true);

                timePassed = 0f;
                startAlphaValue = 0f;
                targetAlphaValue = .5f;
                
                StartCoroutine(FadeTransition(startAlphaValue, targetAlphaValue, transitionDuration, timePassed));
                
                hamburgerMenuCanvas.sortingOrder = 1000;
                gameDimmed = true;
            }

            else if (gameDimmed == true)
            {
                timePassed = 0f;
                startAlphaValue = .5f;
                targetAlphaValue = 0f;
                StartCoroutine(FadeTransition(startAlphaValue, targetAlphaValue, transitionDuration, timePassed));
                gameDimmed = false;

                yield return new WaitForSeconds(transitionDuration);
                
                hiddenMenuButtons.SetActive(false);
                openBurgerMenuButton.SetActive(true);
                fadeImage.enabled = false;
                
                GameCanvasPriority();
            }
        }

        IEnumerator FadeTransition(float startValue, float targetValue, float transitionDuration, float timePassed)
        {
            transitionCanvas.sortingOrder = 999;
            float valueChange = targetValue - startValue;
            Color alphaColor = new Color();

            alphaColor = fadeImage.color;

            while (timePassed <= transitionDuration)
            {
                timePassed += Time.deltaTime;

                switch (transitionType)
                {
                    case TransitionType.LinearTween:
                        alphaColor.a = TweenManager.LinearTween(timePassed, startValue, valueChange, transitionDuration);
                        yield return null;
                        break;
                    case TransitionType.EaseInQuad:
                        alphaColor.a = TweenManager.EaseInQuad(timePassed, startValue, valueChange, transitionDuration);
                        yield return null;
                        break;
                    case TransitionType.EaseOutQuad:
                        alphaColor.a = TweenManager.EaseOutQuad(timePassed, startValue, valueChange, transitionDuration);
                        yield return null;
                        break;
                    case TransitionType.EaseInOutQuad:
                        alphaColor.a = TweenManager.EaseInOutQuad(timePassed, startValue, valueChange, transitionDuration);
                        yield return null;
                        break;
                    case TransitionType.EaseInOutQuint:
                        alphaColor.a = TweenManager.EaseInOutQuint(timePassed, startValue, valueChange, transitionDuration);
                        yield return null;
                        break;
                    case TransitionType.EaseInOutSine:
                        alphaColor.a = TweenManager.EaseInOutSine(timePassed, startValue, valueChange, transitionDuration);
                        yield return null;
                        break;
                }

                fadeImage.color = alphaColor;
            }
        }

    }
}