using Godot;
using System;

public partial class State : Node
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected
    protected CharacterDirector characterDirector;
    protected string animationPath;
    protected AudioStream soundEffectStream;
    protected Main main;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        main = GetNode<Main>("/root/Main");
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    virtual public void Init(
        CharacterDirector characterDirectorRef) 
    {
        characterDirector = characterDirectorRef;
    }

    virtual public void Enter() {
        if (!string.IsNullOrEmpty(animationPath)) {
            characterDirector.GetAnimationPlayer().Play(animationPath);
        }

        if (characterDirector.GetAudioPlayer() != null) {
            characterDirector.GetAudioPlayer().Stream = soundEffectStream;
            characterDirector.GetAudioPlayer().Play();
        }

        return;
    }

    virtual public void Exit() {
        return;
    }

    virtual public State ProcessInput(InputEvent inputEvent) {
        return null;
    }

    virtual public State ProcessPhysics(float delta) {
        return null;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
