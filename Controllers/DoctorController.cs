using DentalClinic.DataTransferObjects;
using DentalClinic.Interfaces;
using DentalClinic.Models;
using Mapster;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Controllers
{
    [Route("api/doctors")]
    [Produces("application/json")]
    [ApiController]
    public class DoctorController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]

        public IActionResult GetAllDoctorsAsync()
        {
            var deffredResults = _unitOfWork.Doctors.GetAllDeferred();

            var results = deffredResults.ToList();

            var resultsDto = results.Adapt<DoctorDto>();

            return Ok(resultsDto);
        }

        // Get Doctors by id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:int}")]

        public async Task<IActionResult> GetDoctorByIdAsync(int id)
        {
            var result = await _unitOfWork.Doctors.GetByIdAsync(id);

            if (result is null)
            {
                return NotFound($"No record found with this id {id}");
            }

            var resultDto = result.Adapt<DoctorDto>();

            return Ok(resultDto);
        }

        // Add Doctors
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]

        public async Task<IActionResult> AddDoctorsAsync(Doctor doctor)
        {
            var result = await _unitOfWork.Doctors.AddAsync(doctor);

            var resultDto = result.Adapt<DoctorDto>();

            _unitOfWork.Complete();

            return Ok(resultDto);

        }

        // update doctor by id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id:int}")]

        public async Task<IActionResult> UpdateDoctorAsync(int id, DoctorDto doctorDto)
        {
            var entity = await _unitOfWork.Doctors.GetByIdAsync(id);

            if (!ModelState.IsValid || doctorDto is null)
            {
                return BadRequest("Something went wrong");
            }

            if (entity is null)
            {
                return NotFound($"No record with this id: {id}");
            }

            doctorDto.Adapt(entity);

            _unitOfWork.Complete();

            return Ok(entity);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("{id:int}")]

        public IActionResult UpdatePartialDoctor(int id, JsonPatchDocument<Doctor> doctor)
        {
            var entity = _unitOfWork.Doctors.GetById(id);

            if (!ModelState.IsValid || doctor is null)
            {
                return BadRequest("Something went wrong");
            }
            if (entity is null)
            {
                return NotFound($"No record found with this id: {id}");
            }

            doctor.ApplyTo(entity);

            _unitOfWork.Complete();

            return Ok(entity);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}")]

        public IActionResult DeleteDoctor(int id)
        {
            var entity = _unitOfWork.Doctors.GetById(id);

            if (!ModelState.IsValid)
            {
                return BadRequest("Something went wrong");
            }

            if (entity is null)
            {
                return NotFound($"No record found with this id: {id}");
            }

            _unitOfWork.Doctors.Delete(entity);

            _unitOfWork.Complete();

            return Ok(entity);



        }

    }
}
