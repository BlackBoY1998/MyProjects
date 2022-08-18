using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlpgsqlAPI.Models;
namespace PlpgsqlAPI.DataAcesss
{
    public class DataAccessProvider:IDataAccessProvider
    {
        private readonly PostgreSqlContext _context;

        public DataAccessProvider(PostgreSqlContext context)
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

        public void DeletePatientRecord(int id)
        {
            var entity = _context.patients.FirstOrDefault(t => t.id == id);
            _context.patients.Remove(entity);
            _context.SaveChanges();
        }

        public Patient GetPatientSingleRecord(int id)
        {
            return _context.patients.FirstOrDefault(t => t.id == id);
        }

        public List<Patient> GetPatientRecords()
        {
            return _context.patients.ToList();
        }
    }
}
