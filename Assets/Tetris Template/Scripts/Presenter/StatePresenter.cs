using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;



public class StatePresenter : MonoBehaviour
{
    public static StatePresenter shared = new StatePresenter();
    [SerializeField] MenuState MenuState;
    [SerializeField] GameOverState GameOverState;
    [SerializeField] GamePlayState GamePlayState;

    public ReactiveProperty<_StatesBase> State { get; set; } = new ReactiveProperty<_StatesBase>();

    void Start()
    {
        // State.Value = MenuState;
        State
        .Subscribe(_ =>
            {
                if (State.Value != null) {
                    State.Value.OnActivate();
                } else {
                    Debug.Log ("nill");
                }
            })
            .AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // protected override void setButton() {
    // }

    public void stateMenu() {
        Debug.Log ("set menu");
        State.Value = MenuState;
    }
    public void stateInGame() {
        Debug.Log ("set game");
        State.Value = GamePlayState;	
    }
    public void stateGameOver() {
        Debug.Log ("set over");
        State.Value = GameOverState;        
    }
}
