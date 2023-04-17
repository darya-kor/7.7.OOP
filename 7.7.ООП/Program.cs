using System.Globalization;

namespace _7._7.ООП
{
    abstract class Delivery
    {
        public string Address;
    }
    public static class StringExtentions
    {
        public static string s = "Доставить по указанному адресу";
        public static string n = "Доставка возможно только по Москве";
        public static string DeliveryMoscow (this string adress)   // создаем метод расширения
        {
            if (adress.Contains("Москва"))
            { return s; }
            else
            { return n; }
        }
    }
    class HomeDelivery : Delivery
    {
        public string NameKurier { get; set; }  // создаем автосвойство для переменной с именем курьера
        public string PhoneKurier { get; set; }  // создаем автоссвойтсво для переменной с телефоном курьера

        HomeDelivery kurier = new HomeDelivery();    // создаем объект Курьер - экземпляр класса ХоумДеливери

        public void DisplayKurier ()
        {
            Console.WriteLine("Имя курьера {0}, телефон курьера {1} Требуется доставить заказ по адресу {2}", kurier.NameKurier, kurier.PhoneKurier, Address);  // пример использования свойств
        }
        
    }

    class PickPointDelivery<T> : Delivery
    {
        public T NumberPoint;  // создаем поле, содержащее условный номер пункта выдачи 

        public PickPointDelivery(T numberPoint)   // инициализируем переменную через конструктор
        {
            NumberPoint = numberPoint;
        }

        public void DisplayNumberPoint<T> (T number)   // создаем обобщенный метод, который показывает номер заказа в виде строки
        {
            Console.WriteLine("В пункт выдачи номер {0} необходимо доставить заказ", number.ToString);
        }

    }

    class PointNew : PickPointDelivery<int>     // наследование обобщений

    {
        public PointNew(int numberPoint) : base(numberPoint) { }

    }

   class ShopDelivery : Delivery
    {
      public void DisplayDateTime(DateTime date) 
        {
            Console.WriteLine("Необходимо отвезти заказ в розничный магазин к {0}", DateTime.TimeOfOrder); 
        }
        //  обращаемся к статическому свойству TimeOfOrder за пределами класса DateTime
    }

    class DateTime
    {
        
        public static DateTime TimeOfOrder { get; set; }  // создаем статическое свойство
        
    }

    class Order<TDelivery,
    TStruct> where TDelivery : Delivery
    {
        public TDelivery Delivery;

        public int Number;

        public string Description;
        public Order(TDelivery delivery, int number, string description)  // определяем конструктор с тремя параметрами
        {
            Delivery = delivery;
            Number = number;
            Description = description;
        }

        public void DisplayAddress()
        {
            Console.WriteLine(Delivery.Address);
        }

        ShopDelivery shopDelivery;   // используем композицию классов: класс Заказ содержит объект класса ШопДеливери
        public Order()
        {
            shopDelivery = new ShopDelivery();
        }

        Product[] tovar;             // создаем индексатор, который позволит работать с объектами Product как с массивом
        public Product this[int index]
        {
            get
            {
                return tovar[index];
            }
            set
            {
                tovar[index] = value;
            }
        }
    }

    abstract class Product        // создаем абстрактный класс Продукт
    {
        private string articul;
        public string Articul     // создаем свойство Артикул, чтобы обращаться к приватному полю Артикул
        {
            get
            {
                return articul;
            }
            set
            {
                articul = value;
            }
        }

        public void DisplayArticul<T> (T articulProduct)     // создаем обобщенный метод, который сможет показывать артикул напитка, заданного в строковом или числовом варианте
        {
            Console.WriteLine("Артикул напитка {0}", articulProduct);
        }
    }

    class Drink : Product       // создаем класс-наследник Напиток
    {
        public virtual void Display()     // создаем виртуальный метод Дисплей
        {
            Console.WriteLine("Выбран напиток");
        }

        protected string nameDrink;   // создаем поле с модификатором protected

    }

    class Alcogol : Drink       // создаем класс-наследник Алкоголь
    { 
        public override void Display()    // переопределяем метод Дисплей в классе-наследнике Алкоголь
        {
            Console.WriteLine("Выбран алкоголь");
        }

        Alcogol alcogol = new Alcogol();
        public int ageKlient;
        private string nameAlcogol;

        public string NameAlcogol      // создаем свойство с логикой
        {
            set
            {
                if (ageKlient >= 18)
                { nameAlcogol = value; }
                
                else
                { Console.WriteLine("Возраст клиента должен быть не меньше 18"); }
            }
            get { return nameAlcogol; }
        }
    }

    class FreeAlcogol : Drink    // Создаем класс-наследник Безалкогольный
    {
        public override void Display()    // переопределяем метод Дисплей в классе-наследнике Безалкогольный напиток
        {
            Console.WriteLine("Выбран безалкогольный напиток {0}", nameDrink);   // использование переменной с модификатором protected в классе-наследнике
        }


    }
    class Food : Product       // создаем класс-наследник Еда
    {
        public string NameOfFood { get; set; }

        public static Food operator + (Food nameFood, Food articulFood)    // перегружаем оператор +, который сможет складывать объекты Food (название товара и его артикул) 
        {
            return new Food { NameOfFood = nameFood.NameOfFood + articulFood.NameOfFood };
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}