using OptimalDX.Data.Repositories;
using OptimalDX.Models;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace OptimalDX
{
	public partial class List : Page
	{
		private PatientRepository _patientRepository;

		protected void Page_Load(object sender, EventArgs e)
		{
			_patientRepository = new PatientRepository();

			var allPatients = _patientRepository
				.GetAllPatients()
				.Where(x => x.IsDeleted == false)
				.OrderBy(x => x.FirstName)
				.ToList();

			foreach (Patient patient in allPatients)
			{
				TableRow row = new TableRow();

				TableCell cellFirstName = new TableCell();
				cellFirstName.Text = patient.FirstName;
				row.Cells.Add(cellFirstName);

				TableCell cellLastName = new TableCell();
				cellLastName.Text = patient.LastName;
				row.Cells.Add(cellLastName);

				TableCell cellPhone = new TableCell();
				cellPhone.Text = patient.Phone;
				row.Cells.Add(cellPhone);

				TableCell cellEmail = new TableCell();
				cellEmail.Text = patient.Email;
				row.Cells.Add(cellEmail);

				TableCell cellGender = new TableCell();
				cellGender.Text = patient.Gender;
				row.Cells.Add(cellGender);

				TableCell cellNotes = new TableCell();
				cellNotes.Text = patient.Notes;
				row.Cells.Add(cellNotes);

				TableCell cellCreatedDate = new TableCell();
				cellCreatedDate.Text = patient.CreatedDate.ToString("yyyy-MM-dd");
				row.Cells.Add(cellCreatedDate);

				TableCell cellActions = new TableCell();
				Button btnEdit = new Button();
				btnEdit.CssClass = "btn btn-primary btn-sm";
				btnEdit.Text = "Edit";
				// Adicione um manipulador de eventos para o botão Edit
				btnEdit.Click += (s, args) => EditPatient(patient.Id); // Supondo que você tenha um método EditPatient para lidar com a edição do paciente
				cellActions.Controls.Add(btnEdit);

				Button btnDelete = new Button();
				btnDelete.CssClass = "btn btn-danger btn-sm";
				btnDelete.Text = "Delete";
				// Adicione um manipulador de eventos para o botão Delete
				btnDelete.Click += (s, args) => DeletePatient(patient.Id); // Supondo que você tenha um método DeletePatient para lidar com a exclusão do paciente
				cellActions.Controls.Add(btnDelete);

				row.Cells.Add(cellActions);

				tbPatients.Rows.Add(row);
			}
		}

		private void EditPatient(Guid patientId)
		{
			Response.Redirect($"Update.aspx?Id={patientId}");
		}

		private void DeletePatient(Guid patientId)
		{
			_patientRepository.DeletePatient(patientId);
			Response.Redirect("List.aspx");
		}
	}
}