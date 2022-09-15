//  /*********************************************************************************
//   *********************************************************************************
//   *********************************************************************************
//   * Produced by Skard Games										                  *
//   * Facebook: https://goo.gl/5YSrKw											      *
//   * Contact me: https://goo.gl/y5awt4								              *											
//   * Developed by Cavit Baturalp Gürdin: https://tr.linkedin.com/in/baturalpgurdin *
//   *********************************************************************************
//   *********************************************************************************
//   *********************************************************************************/

using UnityEngine;
using System.Collections;
using UniRx;


public class ScoreManager : MonoBehaviour {

	public int currentScore=0;
	public int highScore;

    public IReadOnlyReactiveProperty<int> RCurrentScore => rCurrentScore;
    private readonly IntReactiveProperty rCurrentScore = new IntReactiveProperty();

    public IReadOnlyReactiveProperty<int> RHighScore => rHighScore;
    private readonly IntReactiveProperty rHighScore = new IntReactiveProperty();

    void Awake()
    {
        if (Managers.Game.stats.highScore != 0)
        {
            highScore = Managers.Game.stats.highScore;
            rHighScore.Value = Managers.Game.stats.highScore;
            // Managers.UI.inGameUI.UpdateScoreUI();
        }
        else
        {
            highScore = 0;
            rHighScore.Value = 0;
            // Managers.UI.inGameUI.UpdateScoreUI();
        }
    }

	public void OnScore(int scoreIncreaseAmount)
	{	
		currentScore += scoreIncreaseAmount;
        rCurrentScore.Value += scoreIncreaseAmount;
        CheckHighScore();
        // Managers.UI.inGameUI.UpdateScoreUI();
        Managers.Game.stats.totalScore += scoreIncreaseAmount;
    }

    public void CheckHighScore()
    {
        if (highScore < rCurrentScore.Value)
        {
            highScore = currentScore;
            rHighScore.Value = rCurrentScore.Value;
        }
    }

    public void ResetScore()
    {
        currentScore = 0;
        rCurrentScore.Value = 0;
        highScore = Managers.Game.stats.highScore;
        rHighScore.Value = Managers.Game.stats.highScore;
        // Managers.UI.inGameUI.UpdateScoreUI();
    }

}
