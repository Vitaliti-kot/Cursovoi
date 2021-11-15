using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MyGame2._0.Models
{
    public class Antivirus
    {
        public int id { get; set; }
        public Bitmap antivirus { get; set; }
        public int score { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Speed { get; set; }

        public void MoveAntivirus()
        {
            this.PositionY += this.Speed;
        }
    }
    public class Antiviruses
    {
        public static List<Antivirus> _antiviruses { get; set; }
        public Antiviruses()
        {
            _antiviruses = new List<Antivirus>();
            GetAllAntiviruses();
        }
        private void GetAllAntiviruses()
        {
            _antiviruses.Add(new Antivirus()
            {
                id = 0,
                antivirus = Resources.antivirus,
                score = 5
            });
            _antiviruses.Add(new Antivirus()
            {
                id = 1,
                antivirus = Resources.anitivirus2,
                score = 10
            });
            _antiviruses.Add(new Antivirus()
            {
                id = 2,
                antivirus = Resources.healthpack,
                score = 15
            });
            _antiviruses.Add(new Antivirus()
            {
                id = 3,
                antivirus = Resources.heart2,
                score = 15
            });
            _antiviruses.Add(new Antivirus()
            {
                id = 4,
                antivirus = Resources.syringe,
                score = 20
            });
            _antiviruses.Add(new Antivirus()
            {
                id = 5,
                antivirus = Resources.target,
                score = 25
            });
           
        }
        public Antivirus GetAntivirus(int id, int width)
        {
            var antivirus = _antiviruses.ElementAt(id);
            var randomX = new Random();
            antivirus.PositionX = randomX.Next(70, width - 50);
            antivirus.PositionY = 50;
            return antivirus;
        }
    }
}
