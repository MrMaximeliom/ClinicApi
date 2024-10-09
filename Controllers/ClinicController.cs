using DentalClinic.DataTransferObjects;
using DentalClinic.Interfaces;
using DentalClinic.Models;
using Mapster;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Controllers
{
    [Route("api/clinics")]
    [Produces("application/json")]
    [ApiController]
    public class ClinicController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]

        public async Task<IActionResult> GetAllClinicsAsync()
        {
            var deffredResults = _unitOfWork.Clinics.GetAllDeferred();

            var results = deffredResults.ToList();

            var resultsDto = results.Adapt<ClinicDto>();
            return Ok(resultsDto);
        }

        // Get Clinics by id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id:int}")]

        public async Task<IActionResult> GetClinicsByIdAsync(int id)
        {
            var result = await _unitOfWork.Clinics.GetByIdAsync(id);

            var resultDto = result.Adapt<ClinicDto>();

            return Ok(resultDto);
        }

        // Add Clinics
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]

        public async Task<IActionResult> AddClinicsAsync(Clinic clinic)
        {
            var result = await _unitOfWork.Clinics.AddAsync(clinic);

            var resultsDto = result.Adapt<ClinicDto>();

            _unitOfWork.Complete();

            return Ok(resultsDto);
        }

        // update clinic by id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id:int}")]

        public IActionResult UpdateClinicAsync(int id, ClinicDto clinicDto)
        {
            var entity = _unitOfWork.Clinics.GetByIdAsync(id);

            if (!ModelState.IsValid || clinicDto is null)
            {
                return BadRequest("Something went wrong");
            }
            if (entity is null)
            {
                return NotFound($"No record with this id: {id}");
            }
            clinicDto.Adapt(entity);
            _unitOfWork.Complete();
            return Ok(entity);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("{id:int}")]

        public IActionResult UpdatePartialClinic(int id, JsonPatchDocument<Clinic> clinic)
        {
            var entity = _unitOfWork.Clinics.GetById(id);

            if (!ModelState.IsValid || clinic is null)
            {
                return BadRequest("Something went wrong");
            }

            if (entity is null)
            {
                return NotFound($"No record with this id: {id}");
            }

            clinic.ApplyTo(entity);

            _unitOfWork.Complete();

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}")]

        public IActionResult DeleteClinic(int id)
        {
            var entity = _unitOfWork.Clinics.GetById(id);

            if (!ModelState.IsValid)
            {
                return BadRequest("Somthing went wrong");
            }
            if (entity is null)
            {
                return NotFound($"No record with this id: {id}");
            }

            _unitOfWork.Clinics.Delete(entity);

            _unitOfWork.Complete();

            return Ok(entity);
        }
    }
}
