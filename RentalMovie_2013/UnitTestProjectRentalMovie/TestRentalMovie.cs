using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using RentalMovie;

namespace TestRentalMovie
{
    [TestFixture()]
    public class TestClass
    {

        [Test()]
        public void Teststatement()
        {
            string name = "Taro";
            string title = "star wars";
            int days = 5;
            int thisAmount = 15;
            int frequentRenterPoints = 2;
            Customer customer = new Customer(name);
            Movie sample_movie = new Movie(title, Movie.NEW_RELEASE);
            Rental rental = new Rental(sample_movie, days);
            customer.addRental(rental);
            string result = customer.statement();


            string expect = "Rental Record for " + name + "\n";
            expect += "\t" + sample_movie.Title + "\t" + thisAmount.ToString() + "\n";
            expect += "Amount owed is 15" + "\nYou earned " + frequentRenterPoints.ToString() + " frequent renter points";
            Assert.AreEqual(expect, result, "Wrong!! expect[{0}] result[{1}]", expect, result);
        }

        [TestCase(1, 1, 1.0)]
        public void TestamountFor(int days, int price_code, double expect)
        {
            Movie movie_input = new Movie("sample", price_code);
            Rental rental_input = new Rental(movie_input, days);

        }
    }
}
