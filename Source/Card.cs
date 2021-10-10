using System.Drawing;

namespace kosinka
{
    public class Card
    {
        public Titles titles; //название карты
        public Suit suit;//ее масть
        public bool hide { get; set; }//скрыта карта или нет
        private readonly Bitmap img;//изображение карты


        public Card(Suit _suit, Titles _title, bool _hide)                                                                
        {                                                                                                                   
            suit = _suit;                                                                                                  //установка значения масти карты
            hide = _hide;                                                                                                  //установка значения видимости карты
            titles = _title;                                                                                               //установка значения названия карты
            img = (Bitmap)Properties.Resources.ResourceManager.GetObject($"{_title.ToString()}_{_suit.ToString()}", null); //установка значения изображения карты путём поиска объекта в ресурсах проекта
        }                                                                                                                  
                                                                                                                           
        public Bitmap GetImgCard()//метод для получения изображения карты
        {
            if (hide)//скрыта ли карта
            {
                return (Bitmap)Properties.Resources.ResourceManager.GetObject("hide", null);//возвращение значение изображения карты путём поиска объекта в ресурсах проекта
            }
            return img;//возвращение изображения карты заданное в 18й строке
        }
        public enum Titles    //список названия карт
        {
            ace,
            two,
            three,
            four,
            five,
            six,
            seven,
            eight,
            nine,
            ten,
            jack,
            queen,
            king
        }

        public enum Suit//список названия мастей
        {
            cherva,
            buba,
            pika,
            trefa
        }
    }
}
