using EasyTransition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DouShuQiTan {
    
    public static class GameSceneName {
        public static string MapScene = "map";
        public static string GameScene = "SampleScene";
        public static string TitleScene = "Title";
        public static string StartScene = "start";
    }
    
    public class SceneChangeMgr:MonoBehaviour {
        private static SceneChangeMgr instance;
        public static SceneChangeMgr Instance {
            get {
                if (instance == null) {
                    // 如果不存在实例，则在场景中查找，或创建新的GameObject
                    instance = FindObjectOfType<SceneChangeMgr>();
                    if (instance == null) {
                        GameObject go = new GameObject("SceneChangeMgr");
                        instance = go.AddComponent<SceneChangeMgr>();
                    }
                }
                return instance;
            }
        }


        private TransitionManager transitionManager;

        void Awake() {
            if (instance == null) {
                instance = this;
                DontDestroyOnLoad(gameObject); // 确保单例对象在加载新场景时不被销毁
            }
            else if (instance != this) {
                Destroy(gameObject); // 确保不会有重复的实例

            }
        }

        void Start() {
            transitionManager = GameObject.Find("TransitionManager").GetComponent<TransitionManager>();
        }
        public void ToMapScene() {
            transitionManager.LoadScene(GameSceneName.MapScene, "SlowFade", 0);
        }

        public void ToTitleScene() {
            transitionManager.LoadScene(GameSceneName.TitleScene, "SlowFade", 0);
        }

        public void ToGameScene() {
            transitionManager.LoadScene(GameSceneName.GameScene, "SlowFade", 0);
        }

        public void ToStartScene() {
            transitionManager.LoadScene(GameSceneName.StartScene, "SlowFade", 0);
        }

    }
}
