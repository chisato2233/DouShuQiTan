using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace DouShuQiTan{
    public static class GameIntro {
        public static PlayerState playerState = new PlayerState(52);
        public static int MapLayer = 0;
        public static UnityEvent OnSave = new UnityEvent();
        public static UnityEvent OnLoad = new UnityEvent();

        public static void Load() {
            OnLoad.Invoke();
            playerState = ES3.Load<PlayerState>("PlayerState",new PlayerState(52));
            MapLayer = ES3.Load<int>("MapLayer", 0);
        }

        public static void Save() {
            OnSave.Invoke();
            ES3.Save("PlayerState", playerState);
            ES3.Save("MapLayer", MapLayer);
        }
    }



    public struct PlayerState {
        public float MaxHp;
        public float CurrentHp;

        public PlayerState (float maxHp = 52) {
            MaxHp = maxHp;
            CurrentHp = MaxHp;
        }
    }
}
