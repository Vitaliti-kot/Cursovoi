using MyGame2._0.Models;
using NAudio.Wave;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGame2._0
{
    public partial class PlayArea : Form
    {
        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;
        private int ApplyHealthPack { get; set; }
        private int ApplyViruses { get; set; }
        private Player _player { get; set; }
        private Virus _currentVirus { get; set; }
        private Antivirus _currentAntivirus { get; set; }
        private bool CanPlay { get; set; }
        public PlayArea(Player player)
        {
            ApplyViruses = 0;
            ApplyHealthPack = 0;
            _player = player;
            if (_player.hearts._hearts.Count > 0)
            {
                CanPlay = true;
            }
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint, true);
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.Text = label3.Text +" "+_player.Name;
            label3.BackColor = Color.Transparent;
            label3.ForeColor = label2.ForeColor;
            label4.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            Sound();
        }
        private WaveOut waveOut;

        private void Sound()
        {
            if (waveOut == null)
            {
                Mp3FileReader reader = new Mp3FileReader(@"Resources\Sound.mp3");
                LoopStream loop = new LoopStream(reader);
                waveOut = new WaveOut();
                waveOut.Init(loop);
                waveOut.Play();
            }
            else
            {
                waveOut.Stop();
                waveOut.Dispose();
                waveOut = null;
            }
        }

        private void OnceAudioEffect(string path)
        {
            outputDevice = new WaveOutEvent();
            audioFile = new AudioFileReader(path);
            outputDevice.Init(audioFile);
            outputDevice.Play();
        }

        private void PlayArea_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            var localPosition = this.PointToClient(Cursor.Position);
            var handlerRect = new Rectangle(localPosition.X - 25, localPosition.Y - 25, 100, 100);
            graphics.DrawImage(_player.playerSceen, handlerRect);
            VirusOperations(graphics);
            AntivirusOperations(graphics);
            HeartsPaint(graphics);
        }

        private void HeartsPaint(Graphics graphics)
        {
            var hearts = _player.hearts._hearts;
            for (int i = 0; i < hearts.Count; i++)
            {
                var Hearts = new Rectangle(80+(i*60), 80, 50, 50);
                graphics.DrawImage(hearts[i], Hearts);
            }
            
        }

        private void ScoreOperationAntivirus(Point localPosition)
        {
            Point between = new Point((localPosition.X - _currentAntivirus.PositionX), (localPosition.Y - _currentAntivirus.PositionY));
            float distance = (float)Math.Sqrt((between.X * between.X) + (between.Y * between.Y));
            if (distance < 50)
            {
                _player.AddScore(_currentAntivirus.score);
                label1.Text = _player.Score.ToString();
                _currentAntivirus = null;
                var color = label2.ForeColor;
                ApplyHealthPack++;
                OnceAudioEffect(@"Resources/medicine.mp3");
                label2.ForeColor = Color.Green;
                label2.ForeColor = color;
            }
        }
        private void ScoreOperationVirus(Point localPosition)
        {
            Point between = new Point((localPosition.X - _currentVirus.PositionX), (localPosition.Y - _currentVirus.PositionY));
            float distance = (float)Math.Sqrt((between.X * between.X) + (between.Y * between.Y));
            if (distance < 50)
            {
                _player.RemoveScore(_currentVirus.score);
                if (_player.Score < 1)
                {
                    CanPlay = _player.RemoveHeart();
                    OnceAudioEffect(@"Resources/hardBreath.wav");
                    if (!CanPlay)
                    {
                        timer1.Stop();
                        timer2.Stop();
                        label4.Visible = true;
                        button1.Visible = true;
                        button2.Visible = true;
                        OnceAudioEffect(@"Resources/Dead.mp3");
                        label4.Text = $"Your score HealthPack: {ApplyHealthPack}. Viruses: {ApplyViruses}";
                    }
                }
                ApplyViruses++;
                label1.Text = _player.Score.ToString();
                _currentVirus = null;
                OnceAudioEffect(@"Resources/cough.mp3");
                var color = label2.ForeColor;
                label2.ForeColor = Color.Red;
                label2.ForeColor = color;
            }
        }
        private void AntivirusOperations(Graphics graphics)
        {
            if (_currentAntivirus != null)
            {

                var antivirusPosition = new Rectangle(_currentAntivirus.PositionX, _currentAntivirus.PositionY, 50, 50);
                graphics.DrawImage(_currentAntivirus.antivirus, antivirusPosition);
                _currentAntivirus.MoveAntivirus();
                ScoreOperationAntivirus(this.PointToClient(Cursor.Position));
                if (_currentAntivirus != null && _currentAntivirus.PositionY > this.Height - 100)
                {
                    _currentAntivirus = null;
                }
            }
        }
        private void VirusOperations(Graphics graphics)
        {
            if (_currentVirus != null)
            {

                var virusPosition = new Rectangle(_currentVirus.PositionX, _currentVirus.PositionY, 50, 50);
                graphics.DrawImage(_currentVirus.virus, virusPosition);
                _currentVirus.MoveVirus();
                ScoreOperationVirus(this.PointToClient(Cursor.Position));
                if (_currentVirus != null && _currentVirus.PositionY > this.Height - 100)
                {
                    _currentVirus = null;
                }
            }
        }
        private void timer1_Tick(object sender, System.EventArgs e)
        {
            Refresh();

        }

        private void timer2_Tick(object sender, System.EventArgs e)
        {
            var randomer = new Random();
            timer2.Interval = randomer.Next(200, 1000);
            if (randomer.Next(1, 20) < 10)
            {
                AddVirus();
            }
            else
            {
                AddAntivirus();
            }

        }
        private void AddVirus()
        {
            if (_currentVirus == null)
            {
                var viruses = new Viruses();
                var virusId = new Random();
                var virus = viruses.GetVirus(virusId.Next(0, 9), this.Width);
                var randomValue = new Random();
                var speed = randomValue.Next(1, 8);
                virus.Speed = speed;
                _currentVirus = virus;
            }
        }
        private void AddAntivirus()
        {
            if (_currentAntivirus == null)
            {
                var antiviruses = new Antiviruses();
                var antivirusId = new Random();
                var antivirus = antiviruses.GetAntivirus(antivirusId.Next(0, 5), this.Width);
                var randomValue = new Random();
                var speed = randomValue.Next(1, 8);
                antivirus.Speed = speed;
                _currentAntivirus = antivirus;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _player = new Player(Name = _player.Name);
            timer1.Start();
            timer2.Start();
            label4.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
