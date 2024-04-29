using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace DouShuQiTan{
    public class GridSystem :SystemBase {
        public UI_棋盘格集合 GoSlotSet;
        public UI_棋槽集合 GoGridSet;

        public UnityEvent<UI_棋槽, UI_棋盘格> PlaceFromSlotToGrid = new UnityEvent<UI_棋槽, UI_棋盘格>();
        public UnityEvent<UI_棋盘格, UI_棋槽> RemoveFromGridToSlot = new UnityEvent<UI_棋盘格, UI_棋槽>();
        public UnityEvent<UI_棋槽, UI_棋槽> MoveInSlot = new UnityEvent<UI_棋槽, UI_棋槽>();
        public UnityEvent<UI_棋盘格, UI_棋盘格> MoveInGrid = new UnityEvent<UI_棋盘格, UI_棋盘格>();




    }
}
