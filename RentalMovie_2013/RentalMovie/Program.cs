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

        public double AmoutFor(Rental each)
        {
            return amountFor(each);
        }

        private double amountFor(Rental rental_movie)
        {
            double result = 0;
            // 一行ごとに金額を計算
            switch (rental_movie.Movie.PriceCode)
            {
                case Movie.REGULAR:
                    result += 2;
                    if (rental_movie.DaysRented > 2)
                        result += (rental_movie.DaysRented - 2) * 1.5;
                    break;
                case Movie.NEW_RELEASE:
                    result += rental_movie.DaysRented * 3;
                    break;
                case Movie.CHILDRENS:
                    result += 1.5;
                    if (rental_movie.DaysRented > 3)
                        result += (rental_movie.DaysRented - 3) * 1.5;
                    break;
            }
            return result;
        }

        public string statement()
        {
            double totalAmount = 0;
            int frequentRenterPoints = 0;
            string result = "Rental Record for " + Name + "\n";
            foreach (Rental each in rentals)
            {
                double thisAmount = amountFor(each);
                // レンタルポイントを加算
                frequentRenterPoints++;
                // 新作を二日以上借りた場合はボーナスポイント
                if ((each.Movie.PriceCode == Movie.NEW_RELEASE) &&
                      each.DaysRented > 1)
                    frequentRenterPoints++;
                // この貸し出しに対する数値の表示
                result += "\t" + each.Movie.Title + "\t" + thisAmount.ToString( ) + "\n";
                totalAmount += thisAmount;
            }
            // フッタ部分の追加
            result += "Amount owed is " + totalAmount.ToString( ) + "\n";
            result += "You earned " + frequentRenterPoints.ToString( ) + " frequent renter points";
            return result;
        }
    }
}
