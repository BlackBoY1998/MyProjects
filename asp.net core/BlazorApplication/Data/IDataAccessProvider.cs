using BlazorApplication.Models;
using System.Collections.Generic;

namespace BlazorApplication.Data
{
    public interface IDataAccessProvider
    {
        void AddPatientRecord(Patient patient);
        void UpdatePatientRecord(Patient patient);
        void DeletePatientRecord(int id);
        Patient GetPatientSingleRecord(int id);
        List<Patient> GetPatientRecords();
    }
}
