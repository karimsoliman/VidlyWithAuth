using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VidlyApp.Models;

namespace VidlyApp.Controllers.Api
{
    [RoutePrefix("api/rentals")]
    public class MovieRentalController : ApiController
    {
        private ApplicationDbContext _context;

        public MovieRentalController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        [Route("rent")]
        public IHttpActionResult RentMovies(MovieRentalDto rentalVm)
        {
            try
            {
                var customer = _context.Customers.FirstOrDefault(c => c.Id == rentalVm.CustomerID);

                var movies = _context.Movies.Where(m => (rentalVm.MovieIDs.Contains(m.Id)) && (m.AvailableNumber > 0)).ToList();

                var rentals = new List<Rental>();

                foreach (Movie movie in movies)
                {
                    if (movie.AvailableNumber == 0)
                        return BadRequest("Movie (" + movie.Name + ") is not available.");

                    movie.AvailableNumber -= 1;
                    var newRental = new Rental { Movie = movie, Customer = customer, RentalDate = DateTime.Now};
                    rentals.Add(newRental);
                }

                _context.Rentals.AddRange(rentals);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}
