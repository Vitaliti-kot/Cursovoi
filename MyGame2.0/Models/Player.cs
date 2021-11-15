using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame2._0.Models
{
    public class Player
    {
        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;
        public string Name { get; set; }
        public int Score { get; set; }
        public Heart hearts{get;set;}

        public Bitmap playerSceen = Resources.player;
        public Player(string name, int score=0)
        {
            if (name == "")
            {
                Name = "Guest";
            }
            else{
                Name = name;
            }
            Score = score;
            hearts = new Heart();
        }

        public void AddScore(int score)
        {
            Score += score;
            if (Score > 100)
            {
                AddHeart();
            }
        }

        public void RemoveScore(int score)
        {
            if (Score > score)
            {
                Score -= score;
            }
            else
            {
                Score = 0;
            }
        }

        public void AddHeart()
        {
            outputDevice = new WaveOutEvent();
            audioFile = new AudioFileReader(@"Resources/heartTick.wav");
            outputDevice.Init(audioFile);
            outputDevice.Play();
            hearts.AddHeart();
            Score = 0;
        }
        public bool RemoveHeart()
        {
            if (hearts._hearts.Count > 0)
            {
                hearts._hearts.Remove(hearts._hearts.Last());
                Score = 100;
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}
