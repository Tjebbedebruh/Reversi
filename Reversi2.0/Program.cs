using Reversi;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
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

//Knoppen en labels aangeven
Button nieuwKnop = new Button();
Button helpKnop = new Button();
Label roodStenen = new Label();
Label blauwStenen = new Label();
Label aanZetLabel = new Label();
Label wieZet = new Label();
Label helpGrid = new Label();
scherm.Controls.Add(nieuwKnop);
scherm.Controls.Add(helpKnop);
scherm.Controls.Add(roodStenen);
scherm.Controls.Add(blauwStenen);
scherm.Controls.Add(aanZetLabel);
scherm.Controls.Add(wieZet);
scherm.Controls.Add(helpGrid);

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

//counter variablen
int aantalBlauweStenen = 2;
int aantalRodeStenen = 2;
int aanZet = 1; //0 = rood, 1 = blauw 2 = grijs
bool help = false;

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

//Aaanzet Label
aanZetLabel.Text = "Blauw is aan zet";
aanZetLabel.Location = new Point(500, 130);
aanZetLabel.Size = new Size(160, 60);
aanZetLabel.Font = font;

//Placeholder
G.FillRectangle(grijsBrush, 100, 300, 600, 600);

//wieZet
wieZet.Location = new Point(100, 300);
wieZet.Size = new Size(600, 600);

//GhelpGrid
helpGrid.Location = new Point(100, 300);
helpGrid.Size = new Size(600, 600);

//-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

//grid waardes
int aantalVakjes = 6;
int breedteVakjes = wieZet.Width / aantalVakjes;

//publieke waardes
Point hier = new Point(0, 0);

//lijsten
List<Steen> steenList = new List<Steen>();
List<Rectangle> rectangles = new List<Rectangle>();

//aparte graphics voor het speelveld
Bitmap veld = new Bitmap(600, 600);
Graphics Gveld = Graphics.FromImage(veld);
wieZet.Image = veld;

bool Ikweetweer = true;


void TekenGoatFunctieEverythingMarkRutteKabineValleLoveNaarEmirForPresidentIktrakteernogweleenkeeropMOLateNightVibes(Object JeroenFokkerMoetFokprogrammaMakenOmStudentenAanDeCokeTeHelpen, PaintEventArgs pea)
{
    Graphics g = pea.Graphics;
    if (Ikweetweer == true)
    {
        //speelveld resetten
        Gveld.Clear(Color.White);
        wieZet.Invalidate();
        steenList.Clear();
        aanZet = 1;

        //score resetten    
        aantalRodeStenen = 2;
        aantalBlauweStenen = 2;
        blauwStenen.Text = $"{aantalBlauweStenen} stenen";
        roodStenen.Text = $"{aantalRodeStenen} stenen";
        aanZetLabel.Text = "Blauw is aan zet";

        //grid tekenen en objecten (opnieuw) aanmaken
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
                Steen tijdelijkObj = steenList[i * aantalVakjes + j];
                //tekenen van de 4 middelste cirkels en hun objecten updaten
                int tijdelijk1 = aantalVakjes / 2;
                int tijdelijk2 = aantalVakjes / 2 - 1;
                if (i == tijdelijk2 && j == tijdelijk2)
                {
                    tijdelijkObj.Kleur = 1;
                    tijdelijkObj.Bezet = true;
                    tijdelijkObj.Plaatsbaar = false;
                    Gveld.FillEllipse(blauwBrush, tijdelijkObj.Locatie.X * breedteVakjes, tijdelijkObj.Locatie.Y * breedteVakjes, breedteVakjes, breedteVakjes);
                }
                else if (i == tijdelijk2 && j == tijdelijk1)
                {
                    tijdelijkObj.Kleur = 0;
                    tijdelijkObj.Bezet = true;
                    tijdelijkObj.Plaatsbaar = false;
                    Gveld.FillEllipse(roodBrush, tijdelijkObj.Locatie.X * breedteVakjes, tijdelijkObj.Locatie.Y * breedteVakjes, breedteVakjes, breedteVakjes);
                }
                else if (i == tijdelijk1 && j == tijdelijk2)
                {
                    tijdelijkObj.Kleur = 0;
                    tijdelijkObj.Bezet = true;
                    tijdelijkObj.Plaatsbaar = false;
                    Gveld.FillEllipse(roodBrush, tijdelijkObj.Locatie.X * breedteVakjes, tijdelijkObj.Locatie.Y * breedteVakjes, breedteVakjes, breedteVakjes);
                }
                else if (i == tijdelijk1 && j == tijdelijk1)
                {
                    tijdelijkObj.Kleur = 1;
                    tijdelijkObj.Bezet = true;
                    tijdelijkObj.Plaatsbaar = false;
                    Gveld.FillEllipse(blauwBrush, tijdelijkObj.Locatie.X * breedteVakjes, tijdelijkObj.Locatie.Y * breedteVakjes, breedteVakjes, breedteVakjes);
                }
            }
        }
    }
    Ikweetweer = false;

    if (help == true)
    {
        foreach (Rectangle r in rectangles)
        {
            g.DrawEllipse(gridPen, r);
        }
    }
    //checkt of die cirkels placeable zijn je weet
    placeable();
    
}


//help knop
helpKnop.Click += helpStatus;

void helpStatus(object o, EventArgs e)
{
    help = (help == true) ? false : true;
}

//reset het hele spel en scores
void reset(object o, EventArgs e)
{
    Ikweetweer = true;
    wieZet.Invalidate();    
}

void placeable()
{
    rectangles.Clear();
    
    //als help geactiveerd is dan runt die de placable loop
    if (help == true)
    {
        //alle vakjes van de grid worden nagelopen om te checken 
        for (int i = 0; i < aantalVakjes; i++)
        {
            for (int j = 0; j < aantalVakjes; j++)
            {
                
                Steen mogelijkPlaceableSteen = steenList[i * aantalVakjes + j];

                if (mogelijkPlaceableSteen.Bezet != true)
                {
                    //alle vakjes om dat ene gecheckte vakje worden gecheckt of ze de tegenovergestelde kleur bevatten
                    for (int xOffset = -1; xOffset <= 1; xOffset++)
                    {
                        for (int yOffset = -1; yOffset <= 1; yOffset++)
                        {
                            // Skip the het middelste rondje
                            if (xOffset == 0 && yOffset == 0)
                            {
                                continue;
                            }

                            int neighborX = j + xOffset;
                            int neighborY = i + yOffset;

                            // check of de neighbor coordinaten in het grid vallen
                            if (neighborX >= 0 && neighborX < aantalVakjes && neighborY >= 0 && neighborY < aantalVakjes)
                            {
                                Steen buurmanSteen = steenList[neighborY * aantalVakjes + neighborX];

                                if (buurmanSteen.Kleur == 2 || buurmanSteen.Kleur == aanZet)
                                {
                                    //mogelijkPlaceableSteen.Plaatsbaar = false;
                                }
                                else if (buurmanSteen.Kleur != aanZet)
                                {
                                    

                                    //checken of de het steentje naast die buurman niet ook een bepaalde kleur heeft
                                    for (int k = 1; k < aantalVakjes - 1; k++)
                                    {
                                        if ((yOffset * k + neighborY) >= 0 && (yOffset * k + neighborY) <= aantalVakjes && (xOffset * k + neighborX) >= 0 && (xOffset * k + neighborX) <= aantalVakjes)
                                        {
                                            try
                                            {
                                                Steen buurmanBuurmanSteen = steenList[(yOffset * k + neighborY) * aantalVakjes + (xOffset * k + neighborX)];
                                                if (buurmanBuurmanSteen.Kleur == 2)
                                                {
                                                    
                                                    continue;
                                                }
                                                else if (buurmanBuurmanSteen.Kleur == aanZet)
                                                {
                                                    
                                                    Rectangle getRekt = new Rectangle(j * breedteVakjes, i * breedteVakjes, breedteVakjes, breedteVakjes);
                                                    rectangles.Add(getRekt);
                                                    mogelijkPlaceableSteen.Plaatsbaar = true;
                                                }
                                            }
                                            catch (Exception e) { }
                                        }
                                    }

                                    continue;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    wieZet.Invalidate();
}

//tekenReversi (AKA de bolletjes tekenen)
void tekenReversi(int gekliktVakjeX, int gekliktVakjeY)
{
    for (int i = 0; i < aantalVakjes; i++)
    {
        for (int j = 0; j < aantalVakjes; j++)
        {
            //steen opzoeken in de steenlist
            Steen obj = steenList[gekliktVakjeY * aantalVakjes + j];


            if (gekliktVakjeX == j && gekliktVakjeY == i && obj.Bezet == false)
            {

                //waardes van het object van deze steen aanpassen
                obj.Bezet = true;
                obj.Kleur = aanZet;
                obj.Plaatsbaar = false;

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


    //aanZetLabel updaten naar wie er aan de beurt is\
    if (aanZet == 0)
    {
        aanZetLabel.Text = $"Blauw is aan zet";
        aanZet++;
        aantalRodeStenen++;
        roodStenen.Text = $"{aantalRodeStenen} stenen";
    }
    else if (aanZet == 1)
    {
        aanZetLabel.Text = $"Rood is aan zet";
        aanZet--;
        aantalBlauweStenen++;
        blauwStenen.Text = $"{aantalBlauweStenen} stenen";

    } 

    scherm.Invalidate();

}

wieZet.MouseClick += mousePosition;
nieuwKnop.Click += reset;
wieZet.Paint += TekenGoatFunctieEverythingMarkRutteKabineValleLoveNaarEmirForPresidentIktrakteernogweleenkeeropMOLateNightVibes;


Application.Run(scherm);