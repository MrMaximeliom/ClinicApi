using DentalClinic.DataTransferObjects;
using DentalClinic.Interfaces;
using DentalClinic.Models;
using Mapster;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Controllers
{
    [Route("api/workingHours")]
    [Produces("application/json")]
    [ApiController]
    public class WorkingHoursController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]

        public IActionResult GetAllHoursAsync()
        {
            var deffredResults = _unitOfWork.WorkingHours.GetAllDeferred();

            var results = deffredResults.ToList();

            var resultsDto = results.Adapt<WorkingHoursDto>();

            return Ok(resultsDto);
        }

        // Get Working Hours by id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:int}")]

        public async Task<IActionResult> GetHoursByIdAsync(int id)
        {
            var result = await _unitOfWork.WorkingHours.GetByIdAsync(id);

            if (result is null)
            {
                return NotFound($"No record found with this id {id}");
            }

            var resultDto = result.Adapt<WorkingHoursDto>();

            return Ok(resultDto);
        }

        // Add working hours
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]

        public async Task<IActionResult> AddWorkingHours(WorkingHours workingHours)
        {
            var result = await _unitOfWork.WorkingHours.AddAsync(workingHours);

            var resultDto = result.Adapt<WorkingHoursDto>();

            _unitOfWork.Complete();

            return Ok(resultDto);
        }

        // update working hours
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id:int}")]

        public async Task<IActionResult> UpdateHoursASync(int id, WorkingHoursDto workingHoursDto)
        {
            var entity = await _unitOfWork.WorkingHours.GetByIdAsync(id);

            if (!ModelState.IsValid || workingHoursDto is null)
            {
                return BadRequest("Something went wrong");
            }
            if (entity is null)
            {
                return NotFound($"No record with this id: {id}");

            }
            workingHoursDto.Adapt(entity);
            _unitOfWork.Complete();
            return Ok(entity);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("{id:int}")]

        public IActionResult UpdatePartialHours(int id, JsonPatchDocument<WorkingHours> workingHours)
        {
            var entity = _unitOfWork.WorkingHours.GetById(id);

            if (!ModelState.IsValid || workingHours is null)
            {
                return BadRequest("Something went wrong");
            }

            if (entity is null)
            {
                return NotFound($"No record with this id: {id}");
            }
            workingHours.ApplyTo(entity);

            _unitOfWork.Complete();

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}")]

        public IActionResult DeleteHours(int id)
        {
            var entity = _unitOfWork.WorkingHours.GetById(id);

            if (!ModelState.IsValid)
            {
                return BadRequest("Something went wrong");
            }
            if (entity is null)
            {
                return NotFound($"No record found with this id: {id}");
            }

            _unitOfWork.WorkingHours.Delete(entity);

            _unitOfWork.Complete();

            return Ok(entity);
        }
    }
}
