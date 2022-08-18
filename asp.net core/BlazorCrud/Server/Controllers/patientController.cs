using BlazorCrud.Server.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorCrud.Shared.Models;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class patientController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public patientController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Patient> Get()
        {
            return _dataAccessProvider.GetPatientRecords();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Patient patient)
        {
            if (ModelState.IsValid)
            {
                Guid obj = Guid.NewGuid();
                patient.id = obj.ToString();
                _dataAccessProvider.AddPatientRecord(patient);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public Patient Details(int id)
        {
            return _dataAccessProvider.GetPatientSingleRecord(id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Patient patient)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdatePatientRecord(patient);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _dataAccessProvider.GetPatientSingleRecord(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeletePatientRecord(id);
            return Ok();
        }
    }
}
