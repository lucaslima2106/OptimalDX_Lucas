using OptimalDX.Data.Repositories;
using OptimalDX.Models;
using System;
using System.Linq;
using System.Web.UI;

namespace OptimalDX
{
	public partial class Update : Page
	{
		private PatientRepository _patientRepository;
		private Patient _patient;

		protected void Page_Load(object sender, EventArgs e)
		{
			_patientRepository = new PatientRepository();

			Guid patientId = Guid.Parse(Request.QueryString["Id"]);
			_patient = _patientRepository.GetAllPatients().SingleOrDefault(x => x.Id == patientId);

			if (!IsPostBack)
			{
				LoadPatientData(patientId);
			}
		}

		private void LoadPatientData(Guid patientId)
		{
			txtFirstName.Text = _patient.FirstName;
			txtLastName.Text = _patient.LastName;
			txtPhone.Text = _patient.Phone;
			txtEmail.Text = _patient.Email;
			ddlGender.SelectedValue = _patient.Gender;
			txtNotes.Text = _patient.Notes;
		}

		protected void btnUpdatePatient_Click(object sender, EventArgs e)
		{
			_patient.FirstName = txtFirstName.Text;
			_patient.LastName = txtLastName.Text;
			_patient.Phone = txtPhone.Text;
			_patient.Email = txtEmail.Text;
			_patient.Gender = ddlGender.SelectedValue;
			_patient.Notes = txtNotes.Text;

			_patientRepository.UpdatePatient(_patient);

			Response.Redirect("List.aspx");
		}

	}
}