using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteSampleCode
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeSorter time_quick_sorter = new TimeSorter(new QuickSorter());
            TimeSorter time_bubble_sorter = new TimeSorter(new BubbleSorter());
            object[] target = null;
            time_quick_sorter.timeSort(target);
            time_bubble_sorter.timeSort(target);
            Console.ReadKey();
        }
    }

    interface Sorter
    {
        void sort(object[] sort_target);
    }
    class QuickSorter : Sorter
    {
        public void sort(object[] sort_target)
        {
            // クイックソートの実装
            System.Threading.Thread.Sleep(100);
        }
    }
    class BubbleSorter : Sorter
    {
        public void sort(object[] sort_target)
        {
            // バブルソートの実装
            System.Threading.Thread.Sleep(200);
        }
    }

    class TimeSorter : Sorter
    {
        Sorter sorter;
        public TimeSorter(Sorter _sorter)
        {
            sorter = _sorter;
        }
        public void timeSort(object[] sort_target)
        {
            DateTime start_time = DateTime.Now;
            sorter.sort(sort_target);
            DateTime end_time = DateTime.Now;
            Console.WriteLine("time:{0}", (start_time - end_time).ToString());
        }
        public void sort(object[] sort_target)
        {
            sorter.sort(sort_target);
        }
        class SampleClass
        {
            public String Name { get; set; }
        }
    }
}
