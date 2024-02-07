using OptimalDX.Data.Contracts;
using OptimalDX.Helpers;
using OptimalDX.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace OptimalDX.Data.Repositories
{
	public class PatientRepository : IPatientRepository
	{
		private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "database.xml");
		private readonly string _dateFormat = "yyyy-MM-dd";

		XDocument _databaseFile;
		Security _securityHelper;

		public PatientRepository()
		{
			_securityHelper = new Security();

			if (File.Exists(_filePath))
			{
				using (StreamReader reader = new StreamReader(_filePath))
				{
					string xmlContent = reader.ReadToEnd();
					_databaseFile = XDocument.Parse(xmlContent);
				}
			}
			else
				throw new Exception("Database error!");
		}

		public void AddPatient(Patient patient)
		{
			string encryptedFirstName = _securityHelper.EncryptString(patient.FirstName);
			string encryptedLastName = _securityHelper.EncryptString(patient.LastName);
			string encryptedPhone = _securityHelper.EncryptString(patient.Phone);
			string encryptedEmail = _securityHelper.EncryptString(patient.Email);
			string encryptedGender = _securityHelper.EncryptString(patient.Gender);
			string encryptedNotes = _securityHelper.EncryptString(patient.Notes);

			XElement newPatient = new XElement("Patient",
				new XElement("Id", patient.Id),
				new XElement("FirstName", encryptedFirstName),
				new XElement("LastName", encryptedLastName),
				new XElement("Phone", encryptedPhone),
				new XElement("Email", encryptedEmail),
				new XElement("Gender", encryptedGender),
				new XElement("Notes", encryptedNotes),
				new XElement("CreatedDate", patient.CreatedDate.ToString(_dateFormat)),
				new XElement("LastUpdatedDate", patient.LastUpdatedDate.ToString(_dateFormat)),
				new XElement("IsDeleted", patient.IsDeleted)
			); ;

			_databaseFile.Root.Add(newPatient);
			_databaseFile.Save(_filePath);
		}

		public List<Patient> GetAllPatients()
		{
			return _databaseFile
				.Root
				.Elements("Patient")
				.Select(p =>
					new Patient(
						(Guid)p.Element("Id"),
						_securityHelper.DecryptString((string)p.Element("FirstName")),
						_securityHelper.DecryptString((string)p.Element("LastName")),
						_securityHelper.DecryptString((string)p.Element("Phone")),
						_securityHelper.DecryptString((string)p.Element("Email")),
						_securityHelper.DecryptString((string)p.Element("Gender")),
						_securityHelper.DecryptString((string)p.Element("Notes")),
						ParseDateTime((string)p.Element("CreatedDate")),
						ParseDateTime((string)p.Element("LastUpdatedDate")),
						(bool)p.Element("IsDeleted"))
				).ToList();
		}

		public List<Patient> SearchPatients(string firstName, string lastName, string phone, string email, string gender)
		{
			List<Patient> allPatients = GetAllPatients();

			List<Patient> filteredPatients = allPatients
				.Where(p =>
					(string.IsNullOrEmpty(firstName) || p.FirstName.Contains(firstName)) &&
					(string.IsNullOrEmpty(lastName) || p.LastName.Contains(lastName)) &&
					(string.IsNullOrEmpty(phone) || p.Phone.Contains(phone)) &&
					(string.IsNullOrEmpty(email) || p.Email.Contains(email)) &&
					(string.IsNullOrEmpty(gender) || p.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase)))
				.ToList();

			return filteredPatients;
		}

		public void UpdatePatient(Patient patient)
		{
			XElement patientElement = _databaseFile.Root.Elements("Patient").FirstOrDefault(p => (Guid)p.Element("Id") == patient.Id);

			if (patientElement != null)
			{
				patientElement.SetElementValue("FirstName", patient.FirstName);
				patientElement.SetElementValue("LastName", patient.LastName);
				patientElement.SetElementValue("Phone", patient.Phone);
				patientElement.SetElementValue("Email", patient.Email);
				patientElement.SetElementValue("Gender", patient.Gender);
				patientElement.SetElementValue("Notes", patient.Notes);
				patientElement.SetElementValue("LastUpdatedDate", DateTime.Now);

				_databaseFile.Save(_filePath);
			}
		}

		public void DeletePatient(Guid patientId)
		{
			XElement patientElement = _databaseFile.Root.Elements("Patient").FirstOrDefault(p => (Guid)p.Element("Id") == patientId);

			if (patientElement != null)
			{
				patientElement.SetElementValue("IsDeleted", true);
				_databaseFile.Save(_filePath);
			}
		}

		private DateTime ParseDateTime(string dateTimeString)
		{
			DateTime dateTime;
			if (DateTime.TryParseExact(dateTimeString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
			{
				return dateTime;
			}
			else
			{
				return DateTime.Now;
			}
		}
	}
}