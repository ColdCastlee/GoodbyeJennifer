using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Global
{
    public class GlobalVar
    {
        public static GameObject G_Canvas;

        public static void InitGlovalVar()
        {
            G_Canvas = GameObject.Find("Canvas");
            Assert.IsTrue(G_Canvas);

            //GameMgr.MessageBox("InitGlobalVar_Canvas");
        }

        public static GameObject startPos;
        
        public static void InitSceneMark()
        {
            startPos = GameObject.Find("Environment/Marks/StartPos");
            
            Assert.IsTrue(startPos);
            
            //GameMgr.MessageBox("InitGlobalVar_SceneMark");
        }
    }

}

