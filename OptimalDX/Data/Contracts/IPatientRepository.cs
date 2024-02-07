using OptimalDX.Models;
using System;
using System.Collections.Generic;

namespace OptimalDX.Data.Contracts
{
	public interface IPatientRepository
	{
		List<Patient> GetAllPatients();
		void AddPatient(Patient patient);
		void UpdatePatient(Patient patient);
		void DeletePatient(Guid patientId);
	}
}
