using System;

namespace OptimalDX.Models
{
	public class Patient
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string Gender { get; set; }
		public string Notes { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime LastUpdatedDate { get; set; }
		public bool IsDeleted { get; set; }

		public Patient(
			string firstName,
			string lastName,
			string phone,
			string email,
			string gender,
			string notes)
		{
			Id = Guid.NewGuid();
			FirstName = firstName;
			LastName = lastName;
			Phone = phone;
			Email = email;
			Gender = gender;
			Notes = notes;
			LastUpdatedDate = DateTime.Now;
			CreatedDate = DateTime.Now;
			IsDeleted = false;
		}

		public Patient(
			Guid id,
			string firstName,
			string lastName,
			string phone,
			string email,
			string gender,
			string notes,
			DateTime createdDate,
			DateTime lastUpdatedDate,
			bool isDeleted)
		{
			Id = id;
			FirstName = firstName;
			LastName = lastName;
			Phone = phone;
			Email = email;
			Gender = gender;
			Notes = notes;
			CreatedDate = createdDate;
			LastUpdatedDate = lastUpdatedDate;
			IsDeleted = isDeleted;
		}
	}
}