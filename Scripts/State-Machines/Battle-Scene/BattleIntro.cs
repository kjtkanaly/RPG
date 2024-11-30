using Godot;
using System;
using Godot.Collections;

public partial class BattleIntro : BattleState
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    private bool introPlaying = false;
    private Array<Tween> introTweens = new Array<Tween>();

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override void Enter()
    {
        introPlaying = true;

        for (int i = 0; i < battleScene.GetBattleOrder().Count; i++) {
            BattleSceneCharacter character = battleScene.GetBattleOrder()[i];
            Vector2 goalPosition = character.Position;
            character.Position = Vector2.Zero;

            Tween introTween = GetTree().CreateTween();
            introTween.TweenProperty(
                character, 
                "position", 
                goalPosition, 
                1.0f);
            introTween.SetPauseMode(Godot.Tween.TweenPauseMode.Process);

            introTweens.Add(introTween);
        }
    }

    public override BattleState ProcessGeneral(float delta)
    {
        CheckIfAnimationsAreDone();

        if (!introPlaying) {
            return GetFirstTurn();
        }

        return null;
    }

    // Protected

    // Private
    private BattleState GetFirstTurn()
    {
        if (battleScene.GetCurrentCharacter().IsOnPlayerTeam()) {
            return playerTeamTurn;
        }
        else {
            return enemyTeamTurn;
        }
    }

    private void CheckIfAnimationsAreDone() 
    {
        for (int i = 0; i < introTweens.Count; i++) {
            if (introTweens[i].IsRunning()) {
                return;
            }
        }

        introPlaying = false;
    }

    //-------------------------------------------------------------------------
    // Debug Methods
}
