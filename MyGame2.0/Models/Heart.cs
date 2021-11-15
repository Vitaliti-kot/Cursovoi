using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame2._0.Models
{
    public class Heart
    {
        public List<Bitmap> _hearts { get; set; }
        public Heart()
        {
            _hearts = new List<Bitmap>();
            for(int i=0; i < 3; i++)
            {
                _hearts.Add(Resources.heart2);
            }
        }

        public void AddHeart()
        {
            _hearts.Add(Resources.heart2);
        }

        public void RemoveHeart()
        {
            if (_hearts.Count > 0)
            {
                _hearts.RemoveAt(_hearts.Count);
            }
        }
    }
}
