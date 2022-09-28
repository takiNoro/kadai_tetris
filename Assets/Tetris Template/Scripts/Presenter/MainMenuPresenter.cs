using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class MainMenuPresenter : _BasePresenter
{
    public Button play;
    public Button reset;
    public Button setting;
    public Button playerState;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    // void Update()
    // {
    //     super.Update();
    // }
    protected override void setButton() {
        play.OnClickAsObservable().Subscribe(key =>
        {
            Managers.Audio.PlayUIClick();
            // Managers.Game.SetState(typeof(GamePlayState));
            StatePresenter.shared.stateInGame();
        }).AddTo(this);

        reset.OnClickAsObservable().Subscribe(key =>
        {
            Managers.Audio.PlayUIClick();
            Managers.Grid.ClearBoard();
            Managers.Game.isGameActive = false;
            Managers.Game.SetState(typeof(GamePlayState));
            Managers.UI.inGameUI.gameOverPopUp.SetActive(false);
        }).AddTo(this);
        setting.OnClickAsObservable().Subscribe(key  =>
        {
            Managers.Audio.PlayUIClick();
            Managers.UI.popUps.ActivateSettingsPopUp();
            Managers.UI.panel.SetActive(true);
        }).AddTo(this);
        playerState.OnClickAsObservable().Subscribe(key  =>
        {
            Managers.Audio.PlayUIClick();
            Managers.UI.popUps.ActivatePlayerStatsPopUp();
            Managers.UI.panel.SetActive(true);
        }).AddTo(this);
    }
}
