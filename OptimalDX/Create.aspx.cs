using OptimalDX.Data.Repositories;
using OptimalDX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OptimalDX
{
	public partial class Create : Page
	{
		private PatientRepository _patientRepository;

		protected void Page_Load(object sender, EventArgs e)
		{
			_patientRepository = new PatientRepository();
		}

		protected void btnCreatePatient_Click(object sender, EventArgs e)
		{
			string firstName = txtFirstName.Text;
			string lastName = txtLastName.Text;
			string phone = txtPhone.Text;
			string email = txtEmail.Text;
			string gender = ddlGender.SelectedValue;
			string notes = txtNotes.Text;

			Patient patient = new Patient(
				firstName,
				lastName,
				phone,
				email,
				gender,
				notes);

			_patientRepository.AddPatient(patient);

			Response.Redirect("List.aspx");
		}

	}
}