using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalMovie
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class Movie
    {
        public const int CHILDRENS = 2;
        public const int REGULAR = 0;
        public const int NEW_RELEASE = 1;

        private String movie_title;
        private int price_code;

        public Movie(String title, int priceCode)
        {
            movie_title = title;
            price_code = priceCode;
        }

        public int PriceCode
        {
            get { return price_code; }
            set { price_code = value; }
        }

        public string Title
        {
            get { return movie_title; }
        }
    }

    public class Rental
    {
        private Movie rental_movie;
        private int days_rented;

        private const int GetBounauPoint = 1;
        private const int NotGetBounauPoint = 0;

        public Rental(Movie movie, int daysRented)
        {
            rental_movie = movie;
            days_rented = daysRented;
        }

        public int DaysRented
        {
            get { return days_rented; }
        }

        public Movie Movie
        {
            get { return rental_movie; }
        }

        public double getCharge()
        {
            double result = 0;
            // 一行ごとに金額を計算
            switch (rental_movie.PriceCode)
            {
                case Movie.REGULAR:
                    result += 2;
                    if (days_rented > 2)
                        result += (days_rented - 2) * 1.5;
                    break;
                case Movie.NEW_RELEASE:
                    result += days_rented * 3;
                    break;
                case Movie.CHILDRENS:
                    result += 1.5;
                    if (days_rented > 3)
                        result += (days_rented - 3) * 1.5;
                    break;
            }
            return result;
        }

        public int getFrequentRenterPoints()
        {
            if ((rental_movie.PriceCode == Movie.NEW_RELEASE) && (days_rented > 1))
            {
                return GetBounauPoint;
            }
            return NotGetBounauPoint;
        }
    }

    public class Customer
    {
        private string name;
        private System.Collections.ArrayList rentals;

        public Customer(string set_name)
        {
            rentals = new System.Collections.ArrayList( );
            name = set_name;
        }

        public void addRental(Rental arg)
        {
            rentals.Add(arg);
        }

        public string Name
        {
            get { return name; }
        }

        public string statement()
        {
            double totalAmount = 0;
            int frequentRenterPoints = 0;
            string result = "Rental Record for " + Name + "\n";
            foreach (Rental each in rentals)
            {
                // レンタルポイントを加算
                frequentRenterPoints++;
                // 新作を二日以上借りた場合はボーナスポイント
                frequentRenterPoints += each.getFrequentRenterPoints();
                // この貸し出しに対する数値の表示
                result += "\t" + each.Movie.Title + "\t" + each.getCharge( ).ToString( ) + "\n";
                totalAmount += each.getCharge( );
            }
            // フッタ部分の追加
            result += "Amount owed is " + totalAmount.ToString( ) + "\n";
            result += "You earned " + frequentRenterPoints.ToString( ) + " frequent renter points";
            return result;
        }

        private static int get_bounus_point(int frequentRenterPoints, Rental each)
        {
            if ((each.Movie.PriceCode == Movie.NEW_RELEASE) &&
                  each.DaysRented > 1)
                frequentRenterPoints++;
            return frequentRenterPoints;
        }
    }
}
