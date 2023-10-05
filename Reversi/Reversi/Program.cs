using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

//form maken
Form scherm = new Form();
scherm.ClientSize = new Size(800, 1000);
scherm.Text = "ReversiTF";
scherm.BackColor = Color.White;


Bitmap window = new Bitmap(800, 1000);
Font font = new Font("Arial", 15);

//Kleuren
Color rood = Color.FromArgb(255, 115, 115);
Color blauw = Color.FromArgb(51, 153, 255);
Color grijs = Color.FromArgb(221, 221, 221);

//Brushes en Pens
Brush roodBrush = new SolidBrush(rood);
Brush blauwBrush = new SolidBrush(blauw);
Brush grijsBrush = new SolidBrush(grijs);
Pen gridPen = new Pen(Color.LightGray, 2);

//tijdelijk:
int aantalBlauweStenen = 0;
int aantalRodeStenen = 0;


//Knoppen en labels aangeven
Button nieuwKnop = new Button();
Button helpKnop = new Button();
Label roodStenen = new Label();
Label blauwStenen = new Label();
Label wieZet = new Label();
scherm.Controls.Add(nieuwKnop);
scherm.Controls.Add(helpKnop);
scherm.Controls.Add(roodStenen);
scherm.Controls.Add(blauwStenen);
scherm.Controls.Add(wieZet);

//nieuwKnop
nieuwKnop.Text = "Nieuw spel";
nieuwKnop.FlatStyle = FlatStyle.Flat;
nieuwKnop.ForeColor = Color.White;
nieuwKnop.BackColor = Color.DarkGray;
nieuwKnop.Location = new Point(100, 50);
nieuwKnop.Size = new Size(275, 50);

//helpKnop
helpKnop.Text = "help";
helpKnop.FlatStyle = FlatStyle.Flat;
helpKnop.ForeColor = Color.White;
helpKnop.BackColor = Color.DarkGray;
helpKnop.Location = new Point(425, 50);
helpKnop.Size = new Size(275, 50);

//Cirkels met stenenaantal + tekst
Label afbeelding = new Label();
scherm.Controls.Add(afbeelding);
afbeelding.Location = new Point(0, 0);
afbeelding.Size = new Size(800, 1000);
afbeelding.Image = window;
Graphics G = Graphics.FromImage(window);
G.FillEllipse(blauwBrush, 100, 130, 60, 60);
G.FillEllipse(roodBrush, 100, 200, 60, 60);

//Blauwe stenen
blauwStenen.Text = $"{aantalBlauweStenen} stenen";
blauwStenen.ForeColor = blauw;
blauwStenen.Location = new Point(170, 150);
blauwStenen.Size = new Size(100, 60);
blauwStenen.Font = font;

//Rode stenen
roodStenen.Text = $"{aantalRodeStenen} stenen";
roodStenen.ForeColor = rood;
roodStenen.Location = new Point(170, 220);
roodStenen.Size = new Size(100, 60);
roodStenen.Font = font;

//Placeholder
G.FillRectangle(grijsBrush, 100, 300, 600, 600);

//wieZet
wieZet.Location = new Point(100, 300);
wieZet.Size = new Size(600, 600);

//-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

//grid waardes
int aantalVakjes = 4;
int breedteVakjes = wieZet.Width / aantalVakjes;

//waar geklikt is
Point hier = new Point(0, 0);

//aparte graphics voor het speelveld
Bitmap veld = new Bitmap(600, 600);
Graphics Gveld = Graphics.FromImage(veld);
wieZet.Image = veld;

//grid tekenen
for (int i = 0; i < breedteVakjes; i++)
{
    for (int j = 0; j < breedteVakjes; j++)
    {
        Gveld.DrawRectangle(gridPen, i * breedteVakjes, j * breedteVakjes, breedteVakjes, breedteVakjes);
    }
}

//tekenReversi (AKA de bolletjes tekenen)
void tekenReversi(int gekliktVakjeX, int gekliktVakjeY)
{
    for (int i = 0; i < breedteVakjes; i++)
    {
        for (int j = 0; j < breedteVakjes; j++)
        {
            if (gekliktVakjeX == i && gekliktVakjeY == j)
            {
               Gveld.FillEllipse(roodBrush, i * breedteVakjes, j * breedteVakjes, breedteVakjes, breedteVakjes);
            }
        }
    }
    wieZet.Invalidate();
}

// muisposities aangeven
void mousePosition(object sender, MouseEventArgs muis)
{
    hier = muis.Location;

    int gekliktVakjeX = muis.X / breedteVakjes;
    int gekliktVakjeY = muis.Y / breedteVakjes;

    tekenReversi(gekliktVakjeX, gekliktVakjeY);

    scherm.Invalidate();
}
    
wieZet.MouseClick += mousePosition;


//Actie voor de knop
void klikKnop(object sender, EventArgs klik)
{

}



Application.Run(scherm);