using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace FlyingKittens
{
    public partial class GlobalForm : System.Windows.Forms.Form
    {
        class DoubleBufferedPictureBox : PictureBox
        {
            public DoubleBufferedPictureBox()
                : base()
            {
                this.DoubleBuffered = true;
            }
        }


        Field field;
        List<FlyingFigure> figures;

        public GlobalForm()
        {
            InitializeComponent();

            InitFlyingKittens();
        }

        void InitFlyingKittens()
        {
            field = new Field(pictureBoxArea.Width, pictureBoxArea.Height);

            figures = new List<FlyingFigure>();
            figures.Add(
                new FlyingRectangle(
                    Color.Black, Color.LawnGreen,
                    new Rectangle(
                        field.Width / 2, field.Height / 2, 30, 100
                    )
                )
            );
        }

        const int LEFT = 0, RIGHT = LEFT + 1, UP = RIGHT + 1, DOWN = UP + 1, NONE = DOWN + 1;

        readonly Point[] SHIFTS =
        {
            new Point(-1, 0),
            new Point(1, 0),
            new Point(0, -1),
            new Point(0, 1),
            new Point(0, 0)
        };

        const int DELAY_MS = 25;

        Thread movingThread;
        volatile bool isStarted = false;
        volatile int direction = NONE;

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Start();
        }

        void Start()
        {
            if (isStarted) return;

            if (direction == NONE)
            {
                direction = new Random().Next(LEFT, NONE);
            }

            isStarted = true;

            movingThread = new Thread(MoveFigures);
            movingThread.Start();
        }


        private void buttonPause_Click(object sender, EventArgs e)
        {
            Pause();
        }

        void Pause()
        {
            isStarted = false;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        void Stop()
        {
            if (movingThread == null) return;

            try
            {
                movingThread.Abort();
            }
            finally
            {
            }
        }


        private void buttonLeft_Click(object sender, EventArgs e)
        {
            direction = LEFT;
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {
            direction = RIGHT;
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            direction = UP;
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            direction = DOWN;
        }

        void MoveFigures()
        {
            while (true)
            {
                Bitmap bitmap = new Bitmap(field.Width, field.Height);
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    foreach (FlyingFigure figure in figures)
                    {
                        if (figure.IsOutside(field)) continue;
                        figure.Draw(g);
                    }
                }

                pictureBoxArea.Image = bitmap;

                Point shift = SHIFTS[isStarted ? direction : NONE];
                Point cycleShift = new Point(field.Width * -shift.X, field.Height * -shift.Y);

                List<FlyingFigure> nextFigures = new List<FlyingFigure>();
                foreach (FlyingFigure figure in figures)
                {
                    if (figure.IsOutside(field)) continue;

                    nextFigures.Add(figure);

                    bool curIsInsideX = figure.IsInsideX(field);
                    bool curIsInsideY = figure.IsInsideY(field);
                    figure.Move(shift);
                    bool nextIsInsideX = figure.IsInsideX(field);
                    bool nextIsInsideY = figure.IsInsideY(field);

                    if (curIsInsideX && !nextIsInsideX || curIsInsideY && !nextIsInsideY)
                    {
                        FlyingFigure cycleBro = figure.Clone();
                        cycleBro.Move(cycleShift);

                        nextFigures.Add(cycleBro);
                    }
                }

                figures = nextFigures;
                Thread.Sleep(DELAY_MS);
            }
        }
    }
}
