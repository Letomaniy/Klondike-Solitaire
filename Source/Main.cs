using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
namespace kosinka
{
    public partial class Main : Form
    {
        public Stopwatch clock = new Stopwatch();
        public void Init()
        {  
            for (int i = 10; i < kolods.Length; i++)
            {
                int countcard = i - 9;
                MoveCard(0, i, countcard, true, true);
            } 
        }

        public void DrawCard(Graphics g, Card card, Point point)//отрисовка карты
        {
            g.DrawImage(card.GetImgCard(), point);
        } 

        public void MoveCard(int start, int finish, int countCard, bool isHide)//перемещение карты
        {
            for (int i = 0; i < countCard; i++)
            {
                if (kolods[start].koloda.Count != 0)
                { 
                    kolods[finish].PutCard(kolods[start].GetUpCard(isHide));
                    kolods[start].TakeCard(); 
                }
            }

        }

        public void MoveCard(int start, int finish, int countCard, bool isHide, bool lastvisible)   //перемещение карты
        {
            for (int i = 0; i < countCard; i++)
            {
                if (kolods[start].koloda.Count != 0)
                {
                    if (lastvisible && i == countCard - 1)
                    {
                        kolods[finish].PutCard(kolods[start].GetUpCard(!isHide));
                        kolods[start].TakeCard();
                    }
                    else
                    {
                        kolods[finish].PutCard(kolods[start].GetUpCard(isHide));
                        kolods[start].TakeCard();
                    }

                }
            }

        }
        /**/
        private void IsFinish()//проверка на победу
        {
            if (kolods[0].koloda.Count == 0 && kolods[1].koloda.Count == 0 && kolods[10].koloda.Count == 0 && kolods[11].koloda.Count == 0 && kolods[12].koloda.Count == 0 && kolods[13].koloda.Count == 0 && kolods[14].koloda.Count == 0 && kolods[15].koloda.Count == 0
               && kolods[16].koloda.Count == 0 && kolods[17].koloda.Count == 0 && kolods[18].koloda.Count == 0 && kolods[19].koloda.Count == 0 && kolods[2].koloda.Count == 13 && kolods[3].koloda.Count == 13 && kolods[4].koloda.Count == 13
               && kolods[5].koloda.Count == 13 && kolods[6].koloda.Count == 13 && kolods[7].koloda.Count == 13 && kolods[8].koloda.Count == 13 && kolods[9].koloda.Count == 13)
            {
                timer1.Stop();
                MessageBox.Show("You win!"); 
                newGameToolStripMenuItem_Click(this, null);
            }
            
        } 


        public void DrawKolods(Graphics g)
        {
            for (int i = 0; i < kolods.Length; i++)
            {
                if (kolods[i].koloda.Count != 0)
                {
                    if (kolods[i].koloda == kolods[0].koloda)
                    {
                        g.DrawImage(kolods[i].GetFace(true), kolodaP[i]);
                    }
                    else if (kolods[i].koloda != kolods[0].koloda)
                    {

                        if (i >= 10)
                        {
                            int ii = 0;
                            for (int j = 0; j < kolods[i].koloda.Count; j++)
                            {
                                if ((kolods[i].koloda.Count - 1 != j))
                                {
                                    rectlist[i - 10][j] = new Rectangle(kolodaP[i].X, kolodaP[i].Y + ii, 144, 40);
                                }
                                else
                                {
                                    rectlist[i - 10][j] = new Rectangle(kolodaP[i].X, kolodaP[i].Y + ii, 144, 192);
                                } 
                                    g.DrawImage(kolods[i].koloda[j].GetImgCard(), new Point(kolodaP[i].X, kolodaP[i].Y + ii));
                                 
                                    ii += 30;
                                 
                                 

                            }
                            if ((kolods[i].koloda[kolods[i].koloda.Count - 1].hide == true))
                            {
                                if (tempCard == null)
                                {
                                    if (tempCard_array.Count == 0)
                                    {
                                        kolods[i].koloda[kolods[i].koloda.Count - 1].hide = false;
                                    }
                                     
                                }
                                 
                            } 
                        }
                        else
                        {
                            g.DrawImage(kolods[i].GetFace(), kolodaP[i]);
                        }


                    }

                }
            }
        }

        private readonly Point[] kolodaP = new Point[]//координаты всех колод
        {
            new Point(62, 74),           //основная колода                                          index=0
            new Point(230, 74),          //место куда скидываются карты из основной колоды          index=1
            new Point(518, 74),          //1                                                        index=2
            new Point(690, 74),          //2                                                        index=3
            new Point(865, 74),          //3                                                        index=4
            new Point(1038, 74),         //4                                                        index=5
            new Point(1210, 74),         //5                                                        index=6
            new Point(1382, 74),         //6                                                        index=7
            new Point(1558, 74),         //7                                                        index=8
            new Point(1730, 74),         //8                                                        index=9
            //                                                                                       
            new Point(118, 300),         //case 1                                                   index=10
            new Point(290, 300),         //case 2                                                   index=11
            new Point(466, 300),         //case 3                                                   index=12
            new Point(638, 300),         //case 4                                                   index=13
            new Point(813, 300),         //case 5                                                   index=14
            new Point(986, 300),         //case 6                                                   index=15
            new Point(1158, 300),        //case 7                                                   index=16
            new Point(1330, 299),        //case 8                                                   index=17
            new Point(1506, 299),        //case 9                                                   index=18   
            new Point(1678, 299),        //case 10                                                  index=19
        }; 
        public Main()
        {
            InitializeComponent();
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            clock.Start();//запуск таймера
            timer1.Start();
            Init();//раскидка карт
        }
        private Koloda[] kolods = new Koloda[]//список колод
        {
            new Koloda(104),        //основная колода                                        index=0
            new Koloda(),           //место куда скидываются карты из основной колоды        index=1
            new Koloda(),           //1                                                      index=2
            new Koloda(),           //2                                                      index=3
            new Koloda(),           //3                                                      index=4
            new Koloda(),           //4                                                      index=5
            new Koloda(),           //5                                                      index=6
            new Koloda(),           //6                                                      index=7
            new Koloda(),           //7                                                      index=8
            new Koloda(),           //8                                                      index=9
            //                                                                               
            new Koloda(),           //case1                                                  index=10
            new Koloda(),           //case2                                                  index=11
            new Koloda(),           //case3                                                  index=12
            new Koloda(),           //case4                                                  index=13
            new Koloda(),           //case5                                                  index=14
            new Koloda(),           //case6                                                  index=15
            new Koloda(),           //case7                                                  index=16
            new Koloda(),           //case8                                                  index=17
            new Koloda(),           //case9                                                  index=18
            new Koloda(),           //case10                                                 index=19
        };
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)//новая игра обновляет список колод и обнуляет все счётчики
        {
            timer1.Stop();
            timer1.Start();
            clock.Restart();
            counterscope = 0;
            toolStripStatusLabel1.Text = "Your scope: 0";
            kolods = new Koloda[]
        {
            new Koloda(104),        //основная колода                                        index=0
            new Koloda(),           //место куда скидываются карты из основной колоды        index=1
            new Koloda(),           //1                                                      index=2
            new Koloda(),           //2                                                      index=3
            new Koloda(),           //3                                                      index=4
            new Koloda(),           //4                                                      index=5
            new Koloda(),           //5                                                      index=6
            new Koloda(),           //6                                                      index=7
            new Koloda(),           //7                                                      index=8
            new Koloda(),           //8                                                      index=9
            //                                                                               
            new Koloda(),           //case1                                                  index=10
            new Koloda(),           //case2                                                  index=11
            new Koloda(),           //case3                                                  index=12
            new Koloda(),           //case4                                                  index=13
            new Koloda(),           //case5                                                  index=14
            new Koloda(),           //case6                                                  index=15
            new Koloda(),           //case7                                                  index=16
            new Koloda(),           //case8                                                  index=17
            new Koloda(),           //case9                                                  index=18
            new Koloda(),           //case10                                                 index=19
                                    
        };
            Init();
            update();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();//выход
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        } 

        private void Main_MouseUp(object sender, MouseEventArgs e)
        {
            bool isRestore = true;
            if (isDown)
            {
                for (int i = 0; i < kolods.Length; i++)
                {
                    if ((i >= 2 & i <= 9) && kolodaR[i].Contains(e.Location) && selectKoloda != i && tempCard != null && tempCard_array.Count == 0)
                    {
                        if ((kolods[i].koloda.Count - 1) != 13)
                        {
                            if (kolods[i].koloda.Count == 0 && tempCard.titles == Card.Titles.ace)
                            {
                                isRestore = false;
                                kolods[i].PutCard(tempCard);

                                counterscope++;
                                update();
                                break;
                            }
                            if (kolods[i].koloda.Count > 0 && ((kolods[i].koloda[kolods[i].koloda.Count - 1].titles + 1) == tempCard.titles) && (kolods[i].koloda[kolods[i].koloda.Count - 1].suit == tempCard.suit))
                            {
                                isRestore = false;
                                kolods[i].PutCard(tempCard);

                                counterscope++;
                                update();
                                break;
                            }
                        } 
                        break;

                    }
                    else if (i >= 10)
                    {

                        if (kolods[i].koloda.Count == 0 && kolodaR[i].Contains(e.Location))
                        {
                            if (tempCard != null)
                            {
                                isRestore = false;
                                kolods[i].PutCard(tempCard);

                                counterscope++;  
                                break;
                            }else if(tempCard_array.Count != 0)
                            {
                                isRestore = false;
                                kolods[i].PutCard(tempCard_array);

                                counterscope++;  
                                break;
                            }
                             
                        }else
                        if (kolodaR[i].Contains(e.Location) && tempCard !=null &&  selectKoloda != i && (kolods[i].koloda[kolods[i].koloda.Count - 1].titles - 1 == tempCard.titles))
                        {
                            if ((kolods[i].koloda[kolods[i].koloda.Count - 1].suit == Card.Suit.buba) || (kolods[i].koloda[kolods[i].koloda.Count - 1].suit == Card.Suit.cherva))
                            {
                                if ((tempCard.suit == Card.Suit.trefa) || (tempCard.suit == Card.Suit.pika))
                                {
                                    isRestore = false;
                                    kolods[i].PutCard(tempCard);

                                    counterscope++;  
                                    break;
                                } 
                            }
                            else if ((kolods[i].koloda[kolods[i].koloda.Count - 1].suit == Card.Suit.trefa) || (kolods[i].koloda[kolods[i].koloda.Count - 1].suit == Card.Suit.pika))
                            {
                                if ((tempCard.suit == Card.Suit.cherva) || (tempCard.suit == Card.Suit.buba))
                                {
                                    isRestore = false;
                                    kolods[i].PutCard(tempCard);

                                    counterscope++;  
                                    break;
                                }

                            }
                        }
                        else if (kolodaR[i].Contains(e.Location) && tempCard_array.Count != 0 && ((kolods[i].koloda[kolods[i].koloda.Count -1].titles - 1) == tempCard_array[0].titles) &&  selectKoloda != i)
                        {
                            if ((kolods[i].koloda[kolods[i].koloda.Count - 1].suit == Card.Suit.buba) || (kolods[i].koloda[kolods[i].koloda.Count - 1].suit == Card.Suit.cherva))
                            {
                                if ((tempCard_array[0].suit == Card.Suit.trefa) || (tempCard_array[0].suit == Card.Suit.pika))
                                {
                                    isRestore = false;
                                    kolods[i].PutCard(tempCard_array);

                                    counterscope++;  
                                    break;
                                }

                            }
                            else if ((kolods[i].koloda[kolods[i].koloda.Count - 1].suit == Card.Suit.trefa) || (kolods[i].koloda[kolods[i].koloda.Count - 1].suit == Card.Suit.pika))
                            {
                                if ((tempCard_array[0].suit == Card.Suit.cherva) || (tempCard_array[0].suit == Card.Suit.buba))
                                {
                                    isRestore = false;
                                    kolods[i].PutCard(tempCard_array);

                                    counterscope++;  
                                    break;
                                }

                            }
                        } 

                    }

                }
                if (isRestore)
                {
                    if (tempCard != null)
                    {
                        kolods[selectKoloda].PutCard(tempCard);
                    }else if (tempCard_array.Count != 0)
                    {
                        kolods[selectKoloda].PutCard(tempCard_array);
                    }
                }
                isDown = false;

            }
            isUp = true;
            update();
            tempCard = null;
            tempCard_array.Clear();

        }

        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)//если зажата левая кнопка
            {
                isUp = false;
                isDown = false;
                for (int i = 0; i < kolods.Length; i++)
                {
                    if ((i == 0) && kolodaR[i].Contains(e.Location))
                    {
                        if (kolods[0].koloda.Count != 0)
                        {
                            MoveCard(0, 1, 1, false);//перемещение карты с основной колоды в второстепенную

                            update();//обновление формы
                            counterscope++;//прибавление счётчика ходов
                            break;
                        }

                        if (kolods[0].koloda.Count == 0)
                        {
                            MoveCard(1, 0, kolods[1].koloda.Count, true);//перемещение карты с второстепеной колоды в основную

                            update();//обновление формы
                            counterscope++;//прибавление счётчика ходов
                        }

                    }
                    if ((i != 0 & i < 10) && !(i >= 2 & i <= 9) && kolodaR[i].Contains(e.Location))
                    {
                        selectKoloda = i;                        //
                        isDown = true;                           //
                        tempCard = kolods[i].GetUpCard();        //установка значения временной карты
                        mousePos = e.Location;                   //устанавливаем значение координат мышки
                        kolods[i].TakeCard();                    //берём карту
                        break;                                   //
                    }
                    for (int j = 0; j < kolods[i].koloda.Count; j++)
                    {
                        if (!(i != 0 & i >= 2 & i <= 9) && (i >= 10) && rectlist[i - 10][j].Contains(e.Location))
                        {
                            if (kolods[i].koloda.Count - 1 == j && tempCard_array.Count == 0 && kolods[i].koloda[j].hide == false)
                            {
                                selectKoloda = i;                     //
                                isDown = true;                        //
                                tempCard = kolods[i].koloda[j];       //установка значения временной карты
                                mousePos = e.Location;                //устанавливаем значение координат мышки
                                kolods[i].TakeCard();                 //берём карту
                                break;                                //
                            }
                            else if (kolods[i].koloda[j].hide == false)
                            {
                                selectKoloda = i;                                        //
                                isDown = true;                                           //
                                for (int ii = j; ii < kolods[i].koloda.Count; ii++)      // 
                                {                                                        //устанавливаем значение координат мышки
                                    tempCard_array.Add(kolods[i].koloda[ii]);            //установка значения временной карты
                                }                                                        //
                                mousePos = e.Location;                                  //устанавливаем значение координат мышки
                                kolods[i].TakeCard(tempCard_array);                     //берём карту    
                                break;
                            }

                        }
                    }

                }
                update();

            }
        }

        private void Main_MouseMove(object sender, MouseEventArgs e)//действие при движении мышкой 
        {
            if (isDown && (tempCard != null))
            {
                update();//принудительное обновление формы
                mousePos = e.Location;//устанавливает координаты мышки
            }
            else if (tempCard_array.Count != 0)
            {
                update();//принудительное обновление формы
                mousePos = e.Location;//устанавливает координаты мышки
            }
        }

        private void Main_Paint(object sender, PaintEventArgs e)//действие при перерисовке формы
        {
            DrawKolods(e.Graphics);//отрисовка колод
            if (isDown && tempCard != null)
            {
                DrawCard(e.Graphics, tempCard, mousePos);//отрисовка карты при переносе мышкой
            }
            else if (tempCard_array.Count != 0)
            {
                int count = 0;
                for (int i = 0; i < tempCard_array.Count; i++)
                {
                    DrawCard(e.Graphics, tempCard_array[i], new Point(mousePos.X, mousePos.Y + count));//отрисовка списка карт при переносе больше чем одной карты
                    count += 30;
                }

            }
            toolStripStatusLabel2.Text = $"Time:  {new DateTime(clock.ElapsedTicks).ToString("HH:mm:ss")}";
        }

        public void update()//принудительное обновление формы
        {
            Invalidate();//принудительное обновление формы
        }
        private int selectKoloda;
        private bool isDown;
        private bool isUp;
        public static Card tempCard;//временная карта
        public static List<Card> tempCard_array = new List<Card>();//список временных карт
        private Point mousePos;//позиция мышки

        private  Rectangle[] kolodaR = new Rectangle[]
        {
            new Rectangle(62, 74,    144, 192),
            new Rectangle(230, 74,   144, 192),
            new Rectangle(518, 74,   144, 192),
            new Rectangle(690, 74,   144, 192),
            new Rectangle(865, 74,   144, 192),
            new Rectangle(1038, 74,  144, 192),
            new Rectangle(1210, 74,  144, 192),
            new Rectangle(1382, 74,  144, 192),
            new Rectangle(1558, 74,  144, 192),
            new Rectangle(1730, 74,  144, 192)

            ,
            new Rectangle(118, 300,  144, 666),
            new Rectangle(290, 300,  144, 666),
            new Rectangle(466, 300,  144, 666),
            new Rectangle(638, 300,  144, 666),
            new Rectangle(813, 300,  144, 666),
            new Rectangle(986, 300,  144, 666),
            new Rectangle(1158, 300, 144, 666),
            new Rectangle(1330, 299, 144, 666),
            new Rectangle(1506, 299, 144, 666),
            new Rectangle(1678, 299, 144, 666)
        };//координаты каждой колоды

        private  List<List<Rectangle>> rectlist = new List<List<Rectangle>>()
        {
        new List<Rectangle>(){
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0)
        },
        new List<Rectangle>(){new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0) },
        new List<Rectangle>(){new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0) },
        new List<Rectangle>(){new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0) },
        new List<Rectangle>(){new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0) },
        new List<Rectangle>(){new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0) },
        new List<Rectangle>(){new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0) },
        new List<Rectangle>(){new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0) },
        new List<Rectangle>(){new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0) },
        new List<Rectangle>(){new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0),
            new Rectangle(0, 0, 0, 0) }
        };//список координат каждой карты 

        public int counterscope = 0;//счётчик очков

        private void timer1_Tick(object sender, EventArgs e)//действие при истечении таймера
        {
            if (tempCard == null && tempCard_array.Count ==0)
            {
                update();//принудительное обновление формы
                IsFinish();//проверка закончена игра или нет
            }
            toolStripStatusLabel1.Text = $"Your scope: {counterscope}";
            timer1.Start();
        }
    }
}
