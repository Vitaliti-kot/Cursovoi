using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MyGame2._0.Models
{
    public class Virus
    {
        public int id { get; set; }
        public Bitmap virus { get; set; }
        public int score { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Speed { get; set; }

        public void MoveVirus()
        {
            this.PositionY += this.Speed;
        }
    }
    public class Viruses
    {
        public static List<Virus> _viruses { get; set; }
        public Viruses()
        {
            _viruses = new List<Virus>();
            GetAllViruses();
        }
        private void GetAllViruses()
        {
            _viruses.Add(new Virus()
            {
                id = 0,
                virus = Resources.virus,
                score = 10
            });
            _viruses.Add(new Virus()
            {
                id = 1,
                virus = Resources.virus1,
                score = 10
            });
            _viruses.Add(new Virus()
            {
                id = 2,
                virus = Resources.virus2,
                score = 50
            });
            _viruses.Add(new Virus()
            {
                id = 3,
                virus = Resources.virus3,
                score = 50
            });
            _viruses.Add(new Virus()
            {
                id = 4,
                virus = Resources.virus4,
                score = 50
            });
            _viruses.Add(new Virus()
            {
                id = 5,
                virus = Resources.virus5,
                score = 50
            });
            _viruses.Add(new Virus()
            {
                id = 6,
                virus = Resources.virus6,
                score = 50
            });
            _viruses.Add(new Virus()
            {
                id = 7,
                virus = Resources.virus7,
                score = 100
            });
            _viruses.Add(new Virus()
            {
                id = 8,
                virus = Resources.virus8,
                score = 100
            });
            _viruses.Add(new Virus()
            {
                id = 9,
                virus = Resources.virus9,
                score = 150
            });
        }
        public Virus GetVirus(int id, int width)
        {
            var virus = _viruses.ElementAt(id);
            var randomX = new Random();
            virus.PositionX = randomX.Next(70, width - 50);
            virus.PositionY = 50;
            return virus;
        }
    }
}
