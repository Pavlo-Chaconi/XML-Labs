using System;
using System.Xml;
using System.Xml.XPath;

namespace LabaratoryWork2
{
    class Program
    {
        static void XPathProcessor(XPathNavigator navigator, string exprs, string title)
        {
            double result = 0.0;
            XmlNamespaceManager nsManager = new XmlNamespaceManager(navigator.NameTable);
            nsManager.AddNamespace("t", "https://ralf.ru");

            bool isNumber = double.TryParse(navigator.Evaluate(exprs, nsManager).ToString(), out result);
            if (!isNumber)
            {
                Console.WriteLine(title);
                XPathNodeIterator xpnIter = navigator.Select(exprs, nsManager);
                while (xpnIter.MoveNext())
                {
                    Console.WriteLine("- {0}", xpnIter.Current.Value);
                }
            }
            else
            {
                Console.WriteLine(title + " {0}", result);
            }
        }

        static void FirstQuery(XPathNavigator navigator)
        {
            string title = "1. Названия обуви с размером больше 37:";
            string exprs = "//t:обувь[t:Размер > 45]/t:название";
            XPathProcessor(navigator, exprs, title);
        }
        static void SecondQuery(XPathNavigator navigator)
        {
            string title = "2. Отделы, где количество аксессуаров больше двух:";
            string exprs = "//t:Отдел[count(t:Аксессуары) > 2]/t:Название";
            XPathProcessor(navigator, exprs, title);
        }
        static void ThirdQuery(XPathNavigator navigator)
        {
            string title = "3. Количество обуви после обуви с кодом 454:";
            string exprs = "count(//t:обувь[@код > 454])";
            XPathProcessor(navigator, exprs, title);
        }






        static void Main(string[] args)
        {
            Console.Title = "XPATH-запросы к XML-документу";
            string url = @"NewShoesShop.xml";
            XPathDocument document = new XPathDocument(url);
            XPathNavigator navigator = document.CreateNavigator();
            FirstQuery(navigator);
            SecondQuery(navigator);
            ThirdQuery(navigator);

            Console.Read();

        }
    }
}
