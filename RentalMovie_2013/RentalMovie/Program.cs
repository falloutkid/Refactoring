using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalMovie
{
    class Program
    {
        /// <summary>
        /// テスト用のランナーメソッド
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Movie movie1 = new Movie("ハリー・ポッターと賢者の石", Movie.REGULAR);
            Movie movie2 = new Movie("ピングーと愉快ななかまたち", Movie.CHILDRENS);
            Movie movie3 = new Movie("ハリー・ポッターと秘密の部屋", Movie.NEW_RELEASE);

            Customer customer = new Customer("田中真紀子");
            customer.addRental(new Rental(movie1, 3));
            customer.addRental(new Rental(movie2, 4));
            customer.addRental(new Rental(movie3, 5));

            System.Console.WriteLine(customer.statement( ));
            System.Console.WriteLine("Please hit key.");
            Console.ReadKey( );
        }
    }

    #region Priceのタイプコード
    abstract class Price
    {
        protected const int GetBounauPoint = 2;
        protected const int NotGetBounauPoint = 1;
        public abstract int getPriceCode();
        public abstract double getCharge(int days_rented);
        public abstract int getFrequentRenterPoints(int days_rented);
    }

    class ChildrenPrice : Price
    {
        public override int getPriceCode()
        {
            return Movie.CHILDRENS;
        }

        public override double getCharge(int days_rented)
        {
            double result = 1.5;
            if (days_rented > 3)
            {
                result += (days_rented - 3) * 1.5;
            }
            return result;
        }

        public override int getFrequentRenterPoints(int days_rented)
        {
            return NotGetBounauPoint;
        }
    }

    class RegularPrice : Price
    {
        public override int getPriceCode()
        {
            return Movie.REGULAR;
        }

        public override double getCharge(int days_rented)
        {
            double result = 2.0;
            if (days_rented > 2)
            {
                result += (days_rented - 2) * 1.5;
            }
            return result;
        }

        public override int getFrequentRenterPoints(int days_rented)
        {
            return NotGetBounauPoint;
        }
    }

    class NewReleasePrice : Price
    {
        public override int getPriceCode()
        {
            return Movie.NEW_RELEASE;
        }

        public override double getCharge(int days_rented)
        {
            return (days_rented * 3.0);
        }

        public override int getFrequentRenterPoints(int days_rented)
        {
            if (days_rented > 1)
            {
                return GetBounauPoint;
            }
            return NotGetBounauPoint;
        }
    }
    #endregion

    public class Movie
    {
        public const int CHILDRENS = 2;
        public const int REGULAR = 0;
        public const int NEW_RELEASE = 1;

        private String movie_title;
        private Price price;

        public Movie(String title, int priceCode)
        {
            movie_title = title;
            PriceCode = priceCode;
        }

        public int PriceCode
        {
            get { return price.getPriceCode(); }
            set {
                switch (value)
                {
                    case Movie.REGULAR:
                        price = new RegularPrice( );
                        break;
                    case Movie.CHILDRENS:
                        price = new ChildrenPrice( );
                        break;
                    case Movie.NEW_RELEASE:
                        price = new NewReleasePrice( );
                        break;
                    default:
                        throw new Exception("不正な料金コード" );
                }
            }
        }

        public string Title
        {
            get { return movie_title; }
        }

        public double getCharge(int days_rented)
        {
            return price.getCharge(days_rented );
        }

        public int getFrequentRenterPoints(int days_rented)
        {
            return price.getFrequentRenterPoints(days_rented);
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

        public double getCharge()
        {
            return rental_movie.getCharge(days_rented);
        }

        public int getFrequentRenterPoints()
        {
            return rental_movie.getFrequentRenterPoints(days_rented);
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

        double TotalCharge
        {
            get
            {
                double totalCharge = 0;
                foreach (Rental each in rentals)
                {
                    totalCharge += each.getCharge( );
                }
                return totalCharge;
            }
        }

        int TotalFrequentRenterPoints
        {
            get
            {
                int totalFrequentRenterPoints = 0;
                foreach (Rental each in rentals)
                {
                    // 新作を二日以上借りた場合はボーナスポイント
                    totalFrequentRenterPoints += each.getFrequentRenterPoints( );
                }
                return totalFrequentRenterPoints;
            }
        }

        public string statement()
        {
            string result = "Rental Record for " + Name + "\n";
            foreach (Rental each in rentals)
            {
                // この貸し出しに対する数値の表示
                result += "\t" + each.Movie.Title + "\t" + each.getCharge( ).ToString( ) + "\n";
            }
            // フッタ部分の追加
            result += "Amount owed is " + TotalCharge.ToString( ) + "\n";
            result += "You earned " + TotalFrequentRenterPoints.ToString( ) + " frequent renter points";
            return result;
        }
    }
}
