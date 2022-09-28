using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;



public class StatePresenter : MonoBehaviour
{
    public static StatePresenter shared;
    public static _StatesBase menuState = new MenuState();
    public static _StatesBase gameOverState = new GameOverState();
    public static _StatesBase gamePlayState = new GamePlayState();
    // [SerializeField] MenuState menuState;
    // [SerializeField] GameOverState gameOverState;
    // [SerializeField] GamePlayState gamePlayState;

    // private _StatesBase[] stateArray = new _StatesBase[] {menuState,gameOverState,gamePlayState};

    public ReactiveProperty<_StatesBase> prevState { get; set; } = new ReactiveProperty<_StatesBase>();
    public ReactiveProperty<_StatesBase> state { get; set; } = new ReactiveProperty<_StatesBase>();

    private float gamePlayDuration;

    private void Awake()
    {
        Debug.Log ("state");
        if(shared == null)
        {
            shared = this;
        }
    }
    void Start()
    {
        Debug.Log ("start");
        menuState.ActiveAction = menuActive;
        menuState.DeactivateAction = menuDeactivate;
        gameOverState.ActiveAction = gameOverActive;
        gameOverState.DeactivateAction = gameOverDeactivate;
        gamePlayState.ActiveAction = gamePlayActive;
        gamePlayState.DeactivateAction = gamePlayDeactivate;

        state.Value = menuState;

        state
        .Subscribe(_ =>
            {
                state.Value.ExecuteActive();
            })
            .AddTo(this);
        prevState
        .Subscribe(_ =>
            {
                if(prevState.Value != null)
                prevState.Value.ExecuteDeactivate();
            })
            .AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (state.Value == gamePlayState) {
            if(Managers.Game.currentShape!=null)
            Managers.Game.currentShape.movementController.ShapeUpdate();
        }
    }
    // protected override void setButton() {
    // }

    public void stateMenu() {
        Debug.Log ("set menu");
        
        state.Value = menuState;
        prevState.Value = state.Value;
    }
    public void stateInGame() {
        Debug.Log ("set game");
        state.Value = gamePlayState;
        prevState.Value = state.Value;
    }
    public void stateGameOver() {
        Debug.Log ("set over");
        state.Value = gameOverState;
        prevState.Value = state.Value;
    }


    private void menuActive()
    {
        Debug.Log ("<color=green>Menu State</color> OnActive");	

		Managers.UI.ActivateUI (Menus.MAIN);
        Managers.Cam.ZoomOut();
        Managers.UI.mainMenu.MainMenuStartAnimation();
        Managers.UI.MainMenuArrange();
    }

    private void gameOverActive()
    {
        Managers.Game.isGameActive = false;
        Managers.Game.stats.highScore = Managers.Score.currentScore;
        Managers.Game.stats.numberOfGames++;
        Managers.UI.popUps.ActivateGameOverPopUp();
        Managers.Audio.PlayLoseSound();
    }

    private void gamePlayActive()
    {
        Managers.UI.panel.SetActive(false);
        Managers.UI.ActivateUI(Menus.INGAME);

        gamePlayDuration = Time.time;
        Managers.Cam.ZoomIn();
        Debug.Log ("<color=green>Gameplay State</color> OnActive");
    }

    private void menuDeactivate()
    {
		Debug.Log ("<color=red>Menu State</color> OnDeactivate");    }

    private void gameOverDeactivate()
    {
        Managers.Adv.ShowRewardedAd();
        Debug.Log ("<color=red>Game Over State</color> OnDeactivate");
    }

    private void gamePlayDeactivate()
    {
        Managers.Game.stats.timeSpent += Time.time - gamePlayDuration;
		Debug.Log ("<color=red>Gameplay State</color> OnDeactivate");
    }
}
