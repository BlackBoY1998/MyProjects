using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorCrud.Server.Models;
using BlazorCrud.Shared.Models;
using BlazorCrud.Server.Interface;

namespace BlazorCrud.Server.DataAccess
{
    public class DataAccessProvider: IDataAccessProvider
    {
        private readonly PatientDBcontext _context;
        public DataAccessProvider(PatientDBcontext context)
        {
            _context = context;
        }
        public void AddPatientRecord(Patient patient)
        {
            _context.patients.Add(patient);
            _context.SaveChanges();
        }

        public void UpdatePatientRecord(Patient patient)
        {
            _context.patients.Update(patient);
            _context.SaveChanges();
        }

        public void DeletePatientRecord(string id)
        {
            var entity = _context.patients.FirstOrDefault(t => t.id == id);
            _context.patients.Remove(entity);
            _context.SaveChanges();
        }

        public Patient GetPatientSingleRecord(string id)
        {
            return _context.patients.FirstOrDefault(t => t.id == id);
        }

        public List<Patient> GetPatientRecords()
        {
            return _context.patients.ToList();
        }
    }
}
