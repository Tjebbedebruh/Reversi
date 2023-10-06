using Reversi;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Xml.Linq;



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
int aantalBlauweStenen = 2;
int aantalRodeStenen = 2;


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
helpKnop.Text = "Help";
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
int aanZet = 0; //0 = rood, 1 = blauw
List<Steen> steenList = new List<Steen>();
List<Steen> roodList = new List<Steen>();
List<Steen> blauwList = new List<Steen>();

//waar geklikt is
Point hier = new Point(0, 0);

//aparte graphics voor het speelveld
Bitmap veld = new Bitmap(600, 600);
Graphics Gveld = Graphics.FromImage(veld);
wieZet.Image = veld;

void reset()
{
    steenList.Clear();
    //grid tekenen en objecten aanmaken
    for (int i = 0; i < aantalVakjes; i++)
    {
        for (int j = 0; j < aantalVakjes; j++)
        {
            //raster tekenen
            Gveld.DrawRectangle(gridPen, j * breedteVakjes, i * breedteVakjes, breedteVakjes, breedteVakjes);

            //object aanmaken voor elke stip in het raster
            Steen steen = new Steen(new Point(j, i), 2, false, false);

            //steen toevoegen aan de lijst van alle stenen
            steenList.Add(steen);
        }
    }

    //door alle vakjes loopen om te checken voor de 4 middelste
    for (int i = 0; i < aantalVakjes; i++)
    {
        for (int j = 0; j < aantalVakjes; j++)
        {
            //tekenen van de 4 middelste cirkels en hun objecten updaten
            int tijdelijk1 = aantalVakjes / 2;
            int tijdelijk2 = aantalVakjes / 2 - 1;
            if (i == tijdelijk2 && j == tijdelijk2)
            {
                Gveld.FillEllipse(blauwBrush, tijdelijk2 * breedteVakjes, tijdelijk2 * breedteVakjes, breedteVakjes, breedteVakjes);
                steenList[j * aantalVakjes + i].Kleur = 0;
                steenList[j * aantalVakjes + i].Bezet = true;
            }
            else if (i == tijdelijk2 && j == tijdelijk1)
            {
                Gveld.FillEllipse(roodBrush, tijdelijk1 * breedteVakjes, tijdelijk2 * breedteVakjes, breedteVakjes, breedteVakjes);
                steenList[j * aantalVakjes + i].Kleur = 1;
                steenList[j * aantalVakjes + i].Bezet = true;
            }
            else if (i == tijdelijk1 && j == tijdelijk2)
            {
                Gveld.FillEllipse(roodBrush, tijdelijk2 * breedteVakjes, tijdelijk1 * breedteVakjes, breedteVakjes, breedteVakjes);
                steenList[j * aantalVakjes + i].Kleur = 1;
                steenList[j * aantalVakjes + i].Bezet = true;
            }
            else if (i == tijdelijk1 && j == tijdelijk1)
            {
                Gveld.FillEllipse(blauwBrush, tijdelijk1 * breedteVakjes, tijdelijk1 * breedteVakjes, breedteVakjes, breedteVakjes);
                steenList[j * aantalVakjes + i].Kleur = 0;
                steenList[j * aantalVakjes + i].Bezet = true;
            }
        }
    }
}
reset();


//tekenReversi (AKA de bolletjes tekenen)
void tekenReversi(int gekliktVakjeX, int gekliktVakjeY)
{
    for (int i = 0; i < breedteVakjes; i++)
    {
        for (int j = 0; j < breedteVakjes; j++)
        {
            
            /*
            if (steenList[gekliktVakjeY * aantalVakjes + i].Kleur == 0)
            {
                roodList.Add(obj);
            }
            else if (steenList[gekliktVakjeY * aantalVakjes + i].Kleur == 1)
            {
                blauwList.Add(obj);
            }
            */
            if (gekliktVakjeX == i && gekliktVakjeY == j)
            {
                //steen opzoeken in de steenlist
                Steen obj = steenList[gekliktVakjeY * aantalVakjes + i];

                //waardes van het object van deze steen aanpassen
                obj.Bezet = true;
                obj.Kleur = aanZet;

                //deze steen inkleuren
                if (obj.Kleur == 0)
                {
                    Gveld.FillEllipse(roodBrush, obj.Locatie.X * breedteVakjes, obj.Locatie.Y * breedteVakjes, breedteVakjes, breedteVakjes);
                }
                else if (obj.Kleur == 1)
                {
                    Gveld.FillEllipse(blauwBrush, obj.Locatie.X * breedteVakjes, obj.Locatie.Y * breedteVakjes, breedteVakjes, breedteVakjes);
                }
                else
                {
                    break;
                }
            }
        }
    }
    wieZet.Invalidate();
}

// muisposities aangeven
void mousePosition(object sender, MouseEventArgs muis)
{
    //locatie van muisclick updaten
    hier = muis.Location;

    int gekliktVakjeX = muis.X / breedteVakjes;
    int gekliktVakjeY = muis.Y / breedteVakjes;    

    tekenReversi(gekliktVakjeX, gekliktVakjeY);

    scherm.Invalidate();

    //wie aanzet is updaten (dus of blauw of rood aan zet is)
    if (aanZet == 0)
    {
        aanZet++;
    }
    else if (aanZet == 1)
    {
        aanZet--;
    }
}
    
wieZet.MouseClick += mousePosition;




Application.Run(scherm);