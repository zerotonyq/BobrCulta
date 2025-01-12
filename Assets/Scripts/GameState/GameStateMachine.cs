using System;
using System.Collections.Generic;
using Gameplay.Services.Base;
using GameState.States;
using Signals;
using UnityEngine;
using Zenject;

namespace GameState
{
    public class GameStateMachine : IInitializable
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private List<IGameService> _services;

        public List<IGameService> Services => _services;
        public SignalBus SignalBus => _signalBus;
        
        private Dictionary<Type, Base.GameState> _states = new();
        public Base.GameState CurrentState { get; private set; }

        public void Initialize()
        {
            AddState(new InitializeState(this));
            AddState(new MenuState(this));
            AddState(new BootState(this));
            AddState(new LevelState(this));
            AddState(new DefeatedState(this));
            
            SetState<InitializeState>();
        }
        
        private void AddState<T>(T state) where T : Base.GameState => _states.TryAdd(typeof(T), state);

        public void SetState<T>() where T : Base.GameState
        {
            if (!_states.TryGetValue(typeof(T), out var state))
            {
                Debug.LogError("There is no state with such type in buffered states");
                return;
            }

            CurrentState?.Exit();

            CurrentState = state;

            CurrentState.Enter();
        }
    } 
}