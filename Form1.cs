using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Xsl;

namespace PathfindingApp
{
    public partial class Form1 : Form
    {
        //panel related
        int lines = 0;
        float xSpace = 0f;
        float ySpace = 0f;

        //Single field related
        int fieldWidth = 0;
        int fieldHeight = 0;

        int xStart, yStart, xEnd, yEnd;

        //Tools
        Graphics graphics;
        Font font;
        Pen pen;

        GridElement[,] allElements;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            panel.Refresh();

            graphics = panel.CreateGraphics();
            pen = new Pen(Brushes.Black, 1);

            lines = Convert.ToInt32(tblblGridSize.Text);
            xSpace = (panel.Width / lines) - pen.Width;
            ySpace = (panel.Height / lines) - pen.Width;           

            font = new Font("Arial", (panel.Width <= panel.Height) ? xSpace / 4 : ySpace / 4);

            drawGrid();
            setLimitation(lines);
        }

        public void drawGrid()
        {
            int xFieldPosition = 0;
            int yFieldPosition = 0;

            int panelWidth = panel.Width; //876
            int panelHeight = panel.Height;  //352
            fieldWidth = panelWidth / lines; //87
            fieldHeight = panelHeight / lines; //35

            float xFont = 0f;
            float yFont = 0f;
            int counter = 1; 

            allElements = new GridElement[lines, lines];

            for (int i = 0; i < lines; i++)
            {
                for (int j = 0; j < lines; j++)
                {
                    GridElement element = new GridElement();
                    element.x = i;
                    element.y = j;
                    element.isObsticle = false;
                    allElements[i, j] = element;

                    Rectangle rect = new Rectangle(xFieldPosition, yFieldPosition, fieldWidth, fieldHeight);
                    graphics.DrawRectangle(pen, rect);

                    graphics.DrawString(Convert.ToString(counter), font, Brushes.Black, xFont + font.Size, yFont + font.Size);

                    xFieldPosition += fieldWidth;
                    xFont += xSpace;
                    counter++;
                }
                xFieldPosition = 0;
                xFont = 0;

                yFieldPosition += fieldHeight;
                yFont += ySpace;
            }
        }

        public void setLimitation(int lines)
        {
            //Obstacle limit
            numericObsticleX.Maximum = lines;
            numericObsticleY.Maximum = lines;
            numericObsticleX.Minimum = 0;
            numericObsticleY.Minimum = 0;

            //Staring point limit
            numericStartingX.Maximum = lines;
            numericStartingY.Maximum = lines;
            numericStartingX.Minimum = 0;
            numericStartingY.Minimum = 0;

            //Ending point limit
            numericEndingX.Maximum = lines;
            numericEndingY.Maximum = lines;
            numericEndingX.Minimum = 0;
            numericEndingY.Minimum = 0;
        }

        private void btnAddObsticle_Click(object sender, EventArgs e)
        {
            drawSingleRectangular(sender, e);
        }

        private void btnFindRoute_Click(object sender, EventArgs e)
        {
            drawSingleRectangular(sender, e);
        }

        private void btnAddStartPoint_Click(object sender, EventArgs e)
        {
            drawSingleRectangular(sender, e);
        }

        private void btnAddEndPoint_Click(object sender, EventArgs e)
        {
            drawSingleRectangular(sender, e);
        }

        public void drawSingleRectangular(object sender, EventArgs e)
        {
            string btn = (sender as Button).Text;
                   
            float xFont = 0f;
            float yFont = 0f;
            int counter = 1;

            GridElement gridElement = new GridElement();
            GridElement[,] allPositions = new GridElement[lines, lines];

            switch (btn)
            {
                case "Add obsticle":
                    SolidBrush obsticleBrush = new SolidBrush(Color.Red);

                    int xObsticle = Convert.ToInt32(numericObsticleX.Value);
                    int yObsticle = Convert.ToInt32(numericObsticleY.Value);

                    int xObsticlePosition = 0;
                    int yObsticlePosition = 0;

                    for (int i = 0; i < lines; i++)
                    {
                        if (i == xObsticle)
                        {
                            for (int j = 0; j < lines; j++)
                            {
                                if (j == yObsticle)
                                {
                                    GridElement obsticle = allElements[i, j];
                                    obsticle.isObsticle = true;
                                    obsticle.TypeOfFields = typeOfFields.Obsticle;
                                    obsticle.x = i;
                                    obsticle.y = j;
                                    allElements[i, j] = obsticle;

                                    Rectangle rect = new Rectangle(xObsticlePosition, yObsticlePosition, fieldWidth, fieldHeight);
                                    graphics.FillRectangle(obsticleBrush, rect);

                                    graphics.DrawString("[OBSTICLE]", font, Brushes.White, xFont + font.Size, yFont + font.Size);
                                }
                                xObsticlePosition += fieldWidth;
                                xFont += xSpace;
                                counter++;
                            }
                        }
                        yObsticlePosition += fieldHeight;
                        xFont = 0;
                        yFont += ySpace;
                    }
                    break;

                case "Add start point":
                    SolidBrush startBrush = new SolidBrush(Color.Yellow);

                    xStart = Convert.ToInt32(numericStartingX.Value);
                    yStart = Convert.ToInt32(numericStartingY.Value);

                    int xStartPosition = 0;
                    int yStartPosition = 0;

                    for (int i = 0; i < lines; i++)
                    {
                        if (i == xStart)
                        {
                            for (int j = 0; j < lines; j++)
                            {
                                if (j == yStart)
                                {
                                    GridElement obsticle = allElements[i, j];
                                    obsticle.isObsticle = false;
                                    obsticle.TypeOfFields = typeOfFields.Start;
                                    obsticle.x = i;
                                    obsticle.y = j;
                                    allElements[i, j] = obsticle;

                                    Rectangle rect = new Rectangle(xStartPosition, yStartPosition, fieldWidth, fieldHeight);
                                    graphics.FillRectangle(startBrush, rect);

                                    graphics.DrawString("[START]", font, Brushes.Black, xFont + font.Size, yFont + font.Size);
                                }
                                xStartPosition += fieldWidth;
                                xFont += xSpace;
                                counter++;
                            }
                        }
                        yStartPosition += fieldHeight;
                        xFont = 0;
                        yFont += ySpace;
                    }
                    break;

                case "Add end point":
                    SolidBrush endBrush = new SolidBrush(Color.Green);

                    xEnd = Convert.ToInt32(numericEndingX.Value);
                    yEnd = Convert.ToInt32(numericEndingY.Value);

                    int xEndPosition = 0;
                    int yEndPosition = 0;

                    for (int i = 0; i < lines; i++)
                    {
                        if (i == xEnd)
                        {
                            for (int j = 0; j < lines; j++)
                            {
                                if (j == yEnd)
                                {
                                    GridElement obsticle = allElements[i, j];
                                    obsticle.isObsticle = false;
                                    obsticle.TypeOfFields = typeOfFields.End;
                                    obsticle.x = i;
                                    obsticle.y = j;
                                    allElements[i, j] = obsticle;

                                    Rectangle rect = new Rectangle(xEndPosition, yEndPosition, fieldWidth, fieldHeight);
                                    graphics.FillRectangle(endBrush, rect);

                                    graphics.DrawString("[END]", font, Brushes.Black, xFont + font.Size, yFont + font.Size);
                                }
                                xEndPosition += fieldWidth;
                                xFont += xSpace;
                                counter++;
                            }
                        }
                        yEndPosition += fieldHeight;
                        xFont = 0;
                        yFont += ySpace;
                    }
                    break;

                case "Find route":
                    int x = xStart;
                    if (xStart <= xEnd)
                    {
                        while (x < xEnd)
                        {
                            allElements[x, yStart].x = x;
                            allElements[x, yStart].isPath = true;
                            x++;
                        }
                    }
                    else
                    {
                        while (x > xEnd)
                        {
                            allElements[x, yStart].x = x;
                            allElements[x, yStart].isPath = true;
                            x--;
                        }
                    }

                    int y = yStart;
                    if (yStart <= yEnd)
                    {
                        while (y <= yEnd)
                        {
                            allElements[x, y].y = y;
                            allElements[x, y].isPath = true;
                            y++;
                        }
                    }
                    else
                    {
                        while (y > yEnd)
                        {
                            allElements[x, y].y = y;
                            allElements[x, y].isPath = true;
                            y--;
                        }
                    }

                    SolidBrush pathBrush = new SolidBrush(Color.Blue);

                    int yPathPosition = 0;

                    for (int i = 0; i < lines; i++)
                    {
                        int xPathPosition = 0;
                        for (int j = 0; j < lines; j++)
                        {
                            if (allElements[i, j].isPath)
                            {
                                Rectangle rect = new Rectangle(xPathPosition, yPathPosition, fieldWidth, fieldHeight);
                                graphics.FillRectangle(pathBrush, rect);

                                graphics.DrawString("[ROUTE]", font, Brushes.Black, xFont + font.Size, yFont + font.Size);
                            }
                            xPathPosition += fieldWidth;
                            xFont += xSpace;
                            counter++;
                        }
                        yPathPosition += fieldHeight;
                        xFont = 0;
                        yFont += ySpace;
                    }
                    break;
            }
        }
    }
}
