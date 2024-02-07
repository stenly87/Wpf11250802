using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf11250802
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            // Маршрутизированные события
            // в windows forms используются обычные события
            // в wpf есть улучшенная версия - RoutedEvent
            // События бывают трех видов:
            // 1. Туннельные - распостраняются от дочерних к родительским
            // 2. Пузырьковые - наоборот от родительских
            // элементов к дочерним
            // 3. Прямые - событие не распостраняется

            // События генерируются - мышью, клавиатурой, стилусом, тачпадом
            // изменениями свойств элементов

            // Событий прямого вида меньше всего - это события
            // MouseEnter и MouseLeave 

            // Если название события начинается с Preview, то это
            // скорее всего Пузырьковое событие

            // Как и в случае с DP, создание собственных RoutedEvents
            // пригодится скорее всего только при создании кастомных
            // компонентов
            // пример маршрутизированного события для кастомной
            // кнопки с пузырьковой стратегией, название Tap
            /*   public static readonly RoutedEvent TapEvent =
               EventManager.RegisterRoutedEvent(
               "Tap", RoutingStrategy.Bubble, 
               typeof(RoutedEventHandler), 
               typeof(MyButtonSimple));

           // Объявление обычного события для организации
           // подписки и отписки
           public event RoutedEventHandler Tap
           {
               add { AddHandler(TapEvent, value); }
               remove { RemoveHandler(TapEvent, value); }
           }*/
        }


        public ObservableCollection<string> EventsData { get; set; }
            = new ObservableCollection<string>();

        private void ClearData(object sender, RoutedEventArgs e)
        {
            EventsData.Clear();
        }

        private void TestLeftMouse(object sender, MouseButtonEventArgs e)
        {
            EventsData.Add($"sender: {sender.ToString()}, e.Source: {e.Source.ToString()}");
            // если назначить Handled в true, то событие 
            // прекращает распространение
            //e.Handled = true;
        }

        private void TestKeyDown(object sender, KeyEventArgs e)
        {
            EventsData.Add($"sender: {sender.ToString()}, e.Source: {e.Source.ToString()}");
            // в случае с preview можно заблокировать ввод
            // с помощью свойства e.Handled = true;
            //e.Handled = true;
        }

        // прямое событие (не распространяется)
        private void TestLeave(object sender, MouseEventArgs e)
        {
            EventsData.Add($"sender: {sender.ToString()}, e.Source: {e.Source.ToString()}");
        }

        private void TextBox_DragLeave(object sender, DragEventArgs e)
        {
            var text = sender as TextBox;
            e.Data.SetData(text.Text);
            text.Text = null;
        }

        private void Button_Drop(object sender, DragEventArgs e)
        {
            string text = (string)e.Data.GetData("Text");
            var b = sender as Button;
            b.Content = text;
        }
    }
}
