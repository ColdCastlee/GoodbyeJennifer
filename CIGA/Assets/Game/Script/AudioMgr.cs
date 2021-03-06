﻿using Game.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Game.Const;
using Game.MemorySystem;
using UnityEngine;

namespace Game.Script
{
    public class AudioMgr : MonoSingleton<AudioMgr>
    {
        private Dictionary<string, AudioClip> audioclips = new Dictionary<string, AudioClip>();
        private AudioSource audioBg;
        private AudioSource audioEffect;

        public float BgSound
        {
            get { return audioBg.volume; }
            set { audioBg.volume = value; }
        }

        public float EffectVolume
        {
            get { return audioEffect.volume; }
            set { audioEffect.volume = value; }
        }

        protected override void Awake()
        {
            base.Awake();
            if (!Directory.Exists(Application.dataPath + "//Resources"))
            {
                Directory.CreateDirectory(Application.dataPath + "//Resources");
                Directory.CreateDirectory(Application.dataPath + "//Resources/Audio");
            }
            else if(!Directory.Exists(Application.dataPath + "//Resources/Audio"))
            {
                Directory.CreateDirectory(Application.dataPath + "//Resources/Audio");
            }

            var type = typeof(AudioName);
            var infoList = type.GetFields(BindingFlags.Static|BindingFlags.Public);
            foreach (var fieldInfo in infoList)
            {
                string audioName = fieldInfo.GetValue(null) as string;
                var res = Resources.Load<AudioClip>(DirPath.AudioDir +audioName);
                if (res == null)
                    Debug.Log("音频加载失败: "+audioName);
                audioclips.Add(audioName,res);
            }
            
//            //实现从文件夹读取所有文件
//            DirectoryInfo source = new DirectoryInfo(Application.dataPath + "//Resources/Audio");
//            foreach (FileInfo diSourceSubDir in source.GetFiles())
//            {
//                if (diSourceSubDir.Name.EndsWith(".meta"))
//                    continue;
//                var strs = diSourceSubDir.Name.Split('.');
//                var res = MemoryMgr.GetSourceFromResources<AudioClip>("Audio/" + strs[0]);
//                if (res == null)
//                    Debug.Log("音频资源加载失败");
//                audioclips.Add(strs[0], res);
//            }


            //加载组件
            this.audioBg = this.gameObject.AddComponent<AudioSource>();
            this.audioBg.loop = true;
            this.audioBg.playOnAwake = true;

            this.audioEffect = this.gameObject.AddComponent<AudioSource>();
            this.audioBg.loop = false;
            this.audioBg.playOnAwake = false;



        }

        void Start()
        {
            //PlayAuBg("Bg");
            //this.audioBg.volume = 0f;
        }



        public void PlayAuBg(string name, float delayTime = 0)
        {
            if (!audioclips.ContainsKey(name))
                throw new Exception("没有这个音频文件: "+name);
            this.audioBg.clip = audioclips[name];
            this.audioBg.PlayDelayed(delayTime);
            //GameMgr.MessageBox(IntPtr.Zero, "PlayAudioBg", "Title", 0);

        }

        public void PlayAuEffect(string name, float delayTime = 0)
        {
            if (!audioclips.ContainsKey(name))
                throw new Exception("没有这个音频文件");
            this.audioEffect.clip = audioclips[name];
            this.audioEffect.PlayDelayed(delayTime);
        }

        public void PlayAuEffect(string name, Vector3 pos)
        {
            if (!audioclips.ContainsKey(name))
                throw new Exception("没有这个音频文件");
            AudioSource.PlayClipAtPoint(this.audioclips[name], pos);
        }
    }
}
