using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PopupPresenter : _BasePresenter
{
    // Start is called before the first frame update

    public GameObject soundCross;
    [SerializeField] GameObject gameOverPop;
    [SerializeField] Button sound;
    [SerializeField] Button rate;
    [SerializeField] Button twitter;
    [SerializeField] Button facebook;

    [SerializeField] Button restart;
    [SerializeField] Button home;
    protected override void setButton() {
        sound.OnClickAsObservable().Subscribe(key =>
        {
            if (AudioListener.volume == 0)
            {
                soundCross.SetActive(false);
                AudioListener.volume = 1.0f;
                Managers.Audio.PlayUIClick();
            }
            else if (AudioListener.volume == 1.0f)
            {
                soundCross.SetActive(true);
                AudioListener.volume = 0f;
            }
        }).AddTo(this);

        rate.OnClickAsObservable().Subscribe(key =>
        {
            Application.OpenURL(Constants.ASSETSTORE_URL);
        }).AddTo(this);
        twitter.OnClickAsObservable().Subscribe(key  =>
        {
            Application.OpenURL(Constants.TWITTER_URL);
        }).AddTo(this);
        facebook.OnClickAsObservable().Subscribe(key  =>
        {
            Application.OpenURL(Constants.FACEBOOK_URL);
        }).AddTo(this);
        restart.OnClickAsObservable().Subscribe(key  =>
        {
            Managers.Audio.PlayUIClick();
            Managers.Grid.ClearBoard();
            Managers.Game.isGameActive = false;
            Managers.Game.SetState(typeof(GamePlayState));
            Managers.UI.inGameUI.gameOverPopUp.SetActive(false);
        }).AddTo(this);
        home.OnClickAsObservable().Subscribe(key  =>
        {
            Managers.Grid.ClearBoard();
            Managers.Audio.PlayUIClick();
            Managers.UI.panel.SetActive(false);
            Managers.Game.SetState(typeof(MenuState));
            gameOverPop.SetActive(false);
        }).AddTo(this);
    }
}
