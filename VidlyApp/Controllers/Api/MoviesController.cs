using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Net.Http;
using System.Web.Http;
using VidlyApp.Models;

namespace VidlyApp.Controllers.Api
{
    [RoutePrefix("api/movies")]
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IHttpActionResult GetAll(string query = null)
        {
            var moviesQuery = _context.Movies.Include(m => m.Genre).Where(m => m.AvailableNumber > 0);
            if (!String.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));
            var result = moviesQuery.ToList().Select(Mapper.Map<Movie, MovieDto>);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetMovieByID(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult EditMovie(MovieDto movie)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDb = _context.Movies.FirstOrDefault(m => m.Id == movie.Id);
            if (movieInDb == null)
                return NotFound();

            Mapper.Map(movie, movieInDb);
            _context.SaveChanges();
            return Ok("Updated Successfully");
        }

        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult AddMovie(MovieDto movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movieTobeAdded = Mapper.Map<MovieDto, Movie>(movie);
            _context.Movies.Add(movieTobeAdded);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + movieTobeAdded.Id), movieTobeAdded);
        }

        [Route("{id}")]
        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.FirstOrDefault(m => m.Id == id);
            if (movieInDb == null)
            {
                return NotFound();
            }
            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
            return Ok();
        }
    }
}
