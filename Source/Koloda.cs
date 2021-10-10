using System;
using System.Collections.Generic;
using System.Drawing;

namespace kosinka
{
    internal class Koloda
    {

        public Koloda() //инициилизация класса Koloda
        {

            koloda = new List<Card>();//создание экземпляра списка колоды
            СardImg = (Bitmap)Properties.Resources.ResourceManager.GetObject("hide", null);//установка изображения закрытой карты
        }


        public Koloda(int countCard)//инициилизация класса Koloda с параметрами
        {
            koloda = new List<Card>();//создание экземпляра списка колоды
            СardImg = (Bitmap)Properties.Resources.ResourceManager.GetObject("hide", null);//установка изображения закрытой карты
            if (countCard == 104) //проверка на количество карт равное двум колодам
            {                                                                                 
                for (int ii = 0; ii < 2; ii++)                                                //запускаем цикл который выполнится два раза
                {                                                                            
                    for (int i = 0; i < 4; i++)                                               // запускам цикл который соответствует колличествам мастей
                    {                                                                        
                        for (int j = 0; j < 13; j++)                                          //запускаем цикл который сооттветствует количеству карт на масть
                        {                                                                     
                            koloda.Add(new Card((Card.Suit)i, (Card.Titles)j, true));         //добавляем карты в список
                        }                                                                      
                    }                                                                          
                }

                RandomList();//рандомизируем карты в колоде
            }
        }

        public void RandomList()//рандомизируем карты в колоде
        {
            Random rm = new Random();                       
            for (int i = 0; i < 3000; i++)                  
            {                                               
                int id = rm.Next(0, koloda.Count);          
                int id2 = rm.Next(0, koloda.Count);         
                Card temp = koloda[id];                     
                koloda[id] = koloda[id2];                   
                koloda[id2] = temp;                         
            }
        }
        public void PutCard(Card card)     //положить карту
        {                                  //
            koloda.Add(card);              //добавляем карту в конкретную колоду
            Main.tempCard = null;          //удалить информацию из временного хранилища карт
        }

        public void PutCard(List<Card> card)//положить несколько карт
        {
            foreach (Card cc in card)
            {
                koloda.Add(cc);//добавляем карту из списка в конкретную колоду
            }
            Main.tempCard_array.Clear();//удалить информацию из временного хранилища карт
        }

        public void TakeCard()//взять карту
        {
            if (koloda.Count > 0)
            {
                int index = koloda.Count - 1;
                koloda.RemoveAt(index);//удаляет из колоды карту
            }
        }

        public void TakeCard(List<Card> cc)//взять список карт
        {
            foreach (Card card in cc)
            {
                if (koloda.Count > 0)
                {
                    koloda.Remove(card); //удаляет из колоды карту(карта берётся из списка)
                }
            } 

        }

        public Card GetUpCard()//возращает карту которая последняя в списке
        {
            int count = koloda.Count;
            if (count > 0)
            {
                int index = koloda.Count - 1;
                return koloda[index];
            }
            return null;
        }

        public Card GetUpCard(bool hide)//возвращает карту которая последняя и открывает её есть параметр false
        {
            int count = koloda.Count;
            if (count > 0)
            {
                int index = koloda.Count - 1;
                koloda[index].hide = hide;
                return koloda[index];
            }
            return null;
        }

        public Bitmap GetFace()//возвращает изображение в BitMap 
        {
            int countCard = koloda.Count;
            if (countCard == 0)
            {
                return СardImg;
            }
            return koloda[countCard - 1].GetImgCard();
        }

        public Bitmap GetFace(bool hidemode)//возвращает изображение в BitMap  
        {
            int countCard = koloda.Count;
            if (countCard == 0 || hidemode)
            {
                return СardImg;
            }
            return koloda[countCard - 1].GetImgCard();
        }

        public List<Card> koloda; //список колоды
        private readonly Bitmap СardImg; //изображение карты 
    }
}


