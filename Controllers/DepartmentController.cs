using DentalClinic.DataTransferObjects;
using DentalClinic.Interfaces;
using DentalClinic.Models;
using Mapster;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Controllers
{
    [Route("api/departments")]
    [Produces("application/json")]
    [ApiController]
    public class DepartmentController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]

        public IActionResult GetAllDepartmentsAsync()
        {
            var defferedResults = _unitOfWork.Departments.GetAllDeferred();

            var results = defferedResults.ToList();

            var resultDto = results.Adapt<DepartmentDto>();

            return Ok(resultDto);
        }

        // Get Department by id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:int}")]

        public async Task<IActionResult> GetDepartmentByIdAsync(int id)
        {
            var result = await _unitOfWork.Departments.GetByIdAsync(id);

            if (result is null)
            {
                return NotFound($"No record found with this id {id}");
            }

            var resultDto = result.Adapt<DepartmentDto>();

            return Ok(resultDto);
        }

        // Add Department 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]

        public async Task<IActionResult> AddDepartmentAsync(Department department)
        {
            var result = await _unitOfWork.Departments.AddAsync(department);

            var resultDto = result.Adapt<DepartmentDto>();

            _unitOfWork.Complete();

            return Ok(resultDto);

        }

        // update department by id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id:int}")]

        public async Task<IActionResult> UpdateDepartmentAsync(int id, DepartmentDto departmentDto)
        {
            var entity = await _unitOfWork.Departments.GetByIdAsync(id);

            if (!ModelState.IsValid || departmentDto is null)
            {
                return BadRequest("Something went wrong");
            }
            if (entity is null)
            {
                return NotFound($"No record with this id: {id}");
            }
            departmentDto.Adapt(entity);
            _unitOfWork.Complete();
            return Ok(entity);

        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("{id:int}")]

        public IActionResult UpdatePartialClinic(int id, JsonPatchDocument<Department> department)
        {
            var entity = _unitOfWork.Departments.GetById(id);

            if (!ModelState.IsValid || department is null)
            {
                return BadRequest("Something went wrong");
            }
            if (entity is null)
            {
                return NotFound($"No record with this id: {id}");
            }

            department.ApplyTo(entity);

            _unitOfWork.Complete();

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}")]

        public IActionResult DeleteDepartment(int id)
        {
            var entity = _unitOfWork.Departments.GetById(id);

            if (!ModelState.IsValid)
            {
                return BadRequest("Something went wrong");
            }

            if (entity is null)
            {
                return NotFound($"No record with this id: {id}");
            }
            _unitOfWork.Departments.Delete(entity);

            _unitOfWork.Complete();

            return Ok(entity);
        }
    }
}
