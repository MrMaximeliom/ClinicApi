using CashierApi.DataTransferObjects;
using DentalClinic.Interfaces;
using DentalClinic.Models;
using Mapster;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace DentalClinic.Controllers
{
    [Route("api/bookings")]
    [Produces("application/json")]
    [ApiController]
    public class BookingController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]

        public async Task<IActionResult> GetAllBookingsAsync()
        {
            var deferredResults = _unitOfWork.Bookings.GetAllDeferred();

            var results = deferredResults.ToArrayAsync();

            var resultDto = results.Adapt<BookingDto>();

            return Ok(resultDto);
        }

        // Get Bookings by id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id:int}")]

        public async Task<IActionResult> GetBookingsByIdAsync(int id)
        {
            var result = await _unitOfWork.Bookings.GetByIdAsync(id);

            var resultDto = result.Adapt<BookingDto>();

            return Ok(resultDto);
        }

        // Add Bookings 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]

        public async Task<IActionResult> AddBookingAsync(Booking bookingDto)
        {
            var result = await _unitOfWork.Bookings.AddAsync(bookingDto);

            var resultDto = result.Adapt<BookingDto>();

            _unitOfWork.Complete();
            return Ok(resultDto);
        }

        // Update booking by id

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id:int}")]

        public IActionResult UpdateBookingAsync(int id, BookingDto booking)
        {
            var entity = _unitOfWork.Bookings.GetById(id);

            if (!ModelState.IsValid || booking is null)
            {
                return BadRequest("Something went wrong");
            }
            if (entity is null)
            {
                return NotFound($"No record with this id: {id}");

            }
            booking.Adapt(entity);

            _unitOfWork.Complete();
            return Ok(entity);

        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("{id:int}")]


        public IActionResult UpdatePartialBooking(int id, JsonPatchDocument<Booking> booking)
        {
            var entity = _unitOfWork.Bookings.GetById(id);

            if (!ModelState.IsValid || booking is null)
            {
                return BadRequest("Something went wrong");
            }
            if (entity is null)
            {
                return NotFound($"Bo record with this Id:{id}");
            }
            booking.ApplyTo(entity);
            _unitOfWork.Complete();
            return NoContent();
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}")]

        public IActionResult DeleteBooking(int id)
        {
            var entity = _unitOfWork.Bookings.GetById(id);

            if (!ModelState.IsValid)
            {
                return BadRequest("Something went wrong");
            }

            if (entity is null)
            {
                return NotFound($"No record with this id: {id}");
            }
            _unitOfWork.Bookings.Delete(entity);
            _unitOfWork.Complete();

            return Ok(entity);
        }

    }


}
