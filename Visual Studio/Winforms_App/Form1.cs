using Enums;
using Logic_Layer;
using Logic_Layer.Interface.LL;
using Shared_Classes;

namespace Winforms_App
{
    public partial class Form1 : Form
    {
        private readonly IUserAccountService _userAccountService;
        private readonly IIDService _IdService;
        private readonly IPassportService _PassportService;
        private readonly IFlightService _flightService;
        private readonly IAirportService _airportService;
        private readonly IPlaneService _planeService;

        public Form1(IUserAccountService userAccountService, IIDService id, IPassportService passport,
            IFlightService flightService, IAirportService airportService, IPlaneService planeService)
        {
            _userAccountService = userAccountService;
            _IdService = id;
            _PassportService = passport;
            _flightService = flightService;
            _airportService = airportService;
            _planeService = planeService;

            InitializeComponent();

            FillBoxes();
        }



        private void FillBoxes()
        {
            // create User
            PopulateGenderComboBox(cbxCreateUserGender);
            PopulateNationalityComboBox(cbxCreateUserNationality);
            PopulateUserTypeComboBox(cbxCreateUserType);

            FillUserListDataGridView(dgvCreateUserUsersList);

            // edit User
            PopulateGenderComboBox(cbxEditUserGender);
            PopulateNationalityComboBox(cbxEditUserNationality);
            PopulateUserTypeComboBox(cbxEditUserType);

            FillUserListDataGridView(dgvEditUserUsersList);

            // create document
            cbxCreateDocumentType.Items.Add("Passport");
            cbxCreateDocumentType.Items.Add("ID");

            FillComboBoxWithUsers(cbxCreateDocumentUsers);
            FillDocumentsListDataGridView(dgvCreateDocumentList);

            // edit document
            cbxEditDocumentType.Items.Add("Passport");
            cbxEditDocumentType.Items.Add("ID");

            FillComboBoxWithUsers(cbxEditDocumentUsers);
            FillDocumentsListDataGridView(dgvEditDocumentList);

            // create flights
            PopulateAirportcbx(cbxCreateFlightOrigin);
            PopulateAirportcbx(cbxCreateFlightDestination);
            PopulatePlanecbx(cbxCreateFlightPlane);
            PopulateFlightStatuscbxComboBox(cbxCreateFlightStatus);

            FillFlightsListDGV(dgvCreateFlightList);

            // edit flights
            PopulateAirportcbx(cbxEditFlightOrigin);
            PopulateAirportcbx(cbxEditFlightDestination);
            PopulatePlanecbx(cbxEditFlightPlane);
            PopulateFlightStatuscbxComboBox(cbxEditFlightStatus);

            FillFlightsListDGV(dgvEditFlightList);


        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // create user
            cbxCreateUserGender.SelectedIndex = 0;
            cbxCreateUserNationality.SelectedIndex = 0;
            cbxCreateUserType.SelectedIndex = 0;

            // edit user
            cbxEditUserGender.SelectedIndex = 0;
            cbxEditUserNationality.SelectedIndex = 0;
            cbxEditUserType.SelectedIndex = 0;

            dgvEditUserUsersList.SelectionChanged += dgvEditUserUsersList_SelectionChanged;
            dgvEditDocumentList.SelectionChanged += dgvEditDocumentList_SelectionChanged;
            dgvEditFlightList.SelectionChanged += dgvEditFlightList_SelectionChanged;

            // create document



        }

        private void FillComboBoxWithUsers(ComboBox comboBox)
        {
            comboBox.Items.Clear();

            List<User> users = _userAccountService.GetAllUsers();

            foreach (User user in users)
            {
                comboBox.Items.Add(user.GetInfo());
            }
        }


        private void PopulateGenderComboBox(ComboBox comboBox)
        {
            foreach (Gender gender in Enum.GetValues(typeof(Gender)))
            {
                comboBox.Items.Add(gender);
            }
        }
        private void PopulateNationalityComboBox(ComboBox comboBox)
        {
            foreach (Nationality nationality in Enum.GetValues(typeof(Nationality)))
            {
                comboBox.Items.Add(nationality);
            }
        }

        private void PopulateUserTypeComboBox(ComboBox comboBox)
        {
            foreach (UserType userType in Enum.GetValues(typeof(UserType)))
            {
                comboBox.Items.Add(userType);
            }
        }


        private void PopulateAirportcbx(ComboBox comboBox)
        {
            List<Airport> airports = _airportService.GetAllAirports();
            foreach (Airport airport in airports)
            {
                comboBox.Items.Add(airport);
            }
            comboBox.DisplayMember = "AirportName";
        }

        private void PopulatePlanecbx(ComboBox comboBox)
        {
            List<Plane> planes = _planeService.GetAllPlanes();
            foreach (Plane plane in planes)
            {
                comboBox.Items.Add(plane);
            }
            comboBox.DisplayMember = "RegistrationNumber";
        }

        private void PopulateFlightStatuscbxComboBox(ComboBox comboBox)
        {
            foreach (FlightStatus flightStatus in Enum.GetValues(typeof(FlightStatus)))
            {
                comboBox.Items.Add(flightStatus);
            }
        }

        private void FillUserListDataGridView(DataGridView dataGridView)
        {
            dataGridView.Columns.Add("ID", "ID");
            dataGridView.Columns.Add("Name", "Name");
            dataGridView.Columns.Add("MiddleName", "Middle Name");
            dataGridView.Columns.Add("Surname", "Surname");
            dataGridView.Columns.Add("Gender", "Gender");
            dataGridView.Columns.Add("BirthDate", "Birth Date");
            dataGridView.Columns.Add("BirthPlace", "Birth Place");
            dataGridView.Columns.Add("Username", "Username");
            dataGridView.Columns.Add("Password", "Password");
            dataGridView.Columns.Add("Email", "Email");
            dataGridView.Columns.Add("Nationality", "Nationality");
            dataGridView.Columns.Add("UserType", "User Type");
        }

        private void FillDocumentsListDataGridView(DataGridView dataGridView)
        {
            dataGridView.Columns.Add("Type", "Type");
            dataGridView.Columns.Add("DocumentID", "Document ID");
            dataGridView.Columns.Add("UserID", "User ID");
            dataGridView.Columns.Add("DocumentNumber", "Document Number");
            dataGridView.Columns.Add("DateOfIssue", "Date Of Issue");
            dataGridView.Columns.Add("DateOfExpire", "Date Of Expire");
        }

        private void FillFlightsListDGV(DataGridView dgv)
        {
            dgv.Columns.Add("FlightID", "Flight ID");
            dgv.Columns.Add("DepartureAirport", "Departure Airport");
            dgv.Columns.Add("ArrivalAirport", "Arrival Airport");
            dgv.Columns.Add("DepartureTime", "Departure Time");
            dgv.Columns.Add("ArrivalTime", "Arrival Time");
            dgv.Columns.Add("Price", "Price");
            dgv.Columns.Add("Plane", "Plane");
            dgv.Columns.Add("FlightStatus", "Flight Status");
        }





        // buttons:

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            string username = string.IsNullOrWhiteSpace(tbxCreateUserUsername.Text) ? null : tbxCreateUserUsername.Text;
            string pass = string.IsNullOrWhiteSpace(tbxCreateUserPassword.Text) ? null : tbxCreateUserPassword.Text;
            string email = string.IsNullOrWhiteSpace(tbxCreateUserEmail.Text) ? null : tbxCreateUserEmail.Text;

            string name = string.IsNullOrWhiteSpace(tbxCreateUserName.Text) ? null : tbxCreateUserName.Text;
            string middleName = string.IsNullOrWhiteSpace(tbxCreateUserMiddlename.Text) ? null : tbxCreateUserMiddlename.Text;
            string surname = string.IsNullOrWhiteSpace(tbxCreateUserSurname.Text) ? null : tbxCreateUserSurname.Text;
            Gender gender = (Gender)cbxCreateUserGender.SelectedIndex;

            DateOnly? birthDateValue = dtpCreateUserBirthdate.Value == null ? null : DateOnly.FromDateTime(dtpCreateUserBirthdate.Value.Date);
            DateOnly birthDate = birthDateValue ?? default;

            string birthPlace = string.IsNullOrWhiteSpace(tbxCreateUserBirthPlace.Text) ? null : tbxCreateUserBirthPlace.Text;
            Nationality nationality = (Nationality)cbxCreateUserNationality.SelectedIndex;
            UserType userType = (UserType)cbxCreateUserType.SelectedIndex;

            User user = new(name, middleName, surname, gender, birthDate, birthPlace,
                            username, pass, email, nationality, userType);

            if (_userAccountService.CreateUser(user))
            {
                MessageBox.Show("User created");
            }
        }


        private void btnCreateUserRefreshList_Click(object sender, EventArgs e)
        {
            dgvCreateUserUsersList.Rows.Clear();
            List<User> users = _userAccountService.GetAllUsers();

            foreach (User user in users)
            {
                dgvCreateUserUsersList.Rows.Add(
                    user.ID,
                    user.Name,
                    user.MiddleName,
                    user.Surname,
                    user.Gender,
                    user.BirthDate,
                    user.BirthPlace,
                    user.Username,
                    user.Password,
                    user.Email,
                    user.Nationality,
                    user.UserType
                );
            }
        }

        private void btnEditUserRefreshList_Click(object sender, EventArgs e)
        {
            dgvEditUserUsersList.Rows.Clear();
            List<User> users = _userAccountService.GetAllUsers();

            foreach (User user in users)
            {
                dgvEditUserUsersList.Rows.Add(
                    user.ID,
                    user.Name,
                    user.MiddleName,
                    user.Surname,
                    user.Gender,
                    user.BirthDate,
                    user.BirthPlace,
                    user.Username,
                    user.Password,
                    user.Email,
                    user.Nationality,
                    user.UserType
                );
            }
        }


        private void dgvEditUserUsersList_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEditUserUsersList.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvEditUserUsersList.SelectedRows[0];

                tbxEditUserID.Text = selectedRow.Cells["ID"].Value.ToString();
                tbxEditUserUsername.Text = selectedRow.Cells["Username"].Value?.ToString() ?? string.Empty;
                tbxEditUserPassword.Text = selectedRow.Cells["Password"].Value?.ToString() ?? string.Empty;
                tbxEditUserEmail.Text = selectedRow.Cells["Email"].Value?.ToString() ?? string.Empty;

                tbxEditUserName.Text = selectedRow.Cells["Name"].Value?.ToString() ?? string.Empty;
                tbxEditUserMiddlename.Text = selectedRow.Cells["MiddleName"].Value?.ToString() ?? string.Empty;
                tbxEditUserSurname.Text = selectedRow.Cells["Surname"].Value?.ToString() ?? string.Empty;
                cbxEditUserGender.SelectedItem = selectedRow.Cells["Gender"].Value;

                DateOnly? birthDateValue = selectedRow.Cells["BirthDate"].Value as DateOnly?;
                if (birthDateValue != null)
                {
                    // Check if birth date is valid
                    if (birthDateValue.Value.Year > 1)
                    {
                        dtpEditUserBirthdate.Value = new DateTime(birthDateValue.Value.Year, birthDateValue.Value.Month, birthDateValue.Value.Day);
                    }
                    else
                    {
                        // Set to MinDate or another default value
                        dtpEditUserBirthdate.Value = dtpEditUserBirthdate.MinDate;
                    }
                }
                else
                {
                    // Set to MinDate or another default value
                    dtpEditUserBirthdate.Value = dtpEditUserBirthdate.MinDate;
                }

                tbxEditUserBirthplace.Text = selectedRow.Cells["BirthPlace"].Value?.ToString() ?? string.Empty;
                cbxEditUserNationality.SelectedItem = selectedRow.Cells["Nationality"].Value;
                cbxEditUserType.SelectedItem = selectedRow.Cells["UserType"].Value;
            }
        }



        private void btnEditUser_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(tbxEditUserID.Text);
            string username = string.IsNullOrWhiteSpace(tbxEditUserUsername.Text) ? null : tbxEditUserUsername.Text;
            string pass = string.IsNullOrWhiteSpace(tbxEditUserPassword.Text) ? null : tbxEditUserPassword.Text;
            string email = string.IsNullOrWhiteSpace(tbxEditUserEmail.Text) ? null : tbxEditUserEmail.Text;

            string name = string.IsNullOrWhiteSpace(tbxEditUserName.Text) ? null : tbxEditUserName.Text;
            string middleName = string.IsNullOrWhiteSpace(tbxEditUserMiddlename.Text) ? null : tbxEditUserMiddlename.Text;
            string surname = string.IsNullOrWhiteSpace(tbxEditUserSurname.Text) ? null : tbxEditUserSurname.Text;
            Gender gender = (Gender)cbxEditUserGender.SelectedIndex;

            DateOnly? birthDateValue = dtpEditUserBirthdate.Value == null ? null : DateOnly.FromDateTime(dtpEditUserBirthdate.Value.Date);
            DateOnly birthDate = birthDateValue ?? default;

            string birthPlace = string.IsNullOrWhiteSpace(tbxEditUserBirthplace.Text) ? null : tbxEditUserBirthplace.Text;
            Nationality nationality = (Nationality)cbxEditUserNationality.SelectedIndex;
            UserType userType = (UserType)cbxEditUserType.SelectedIndex;

            User user = new(id, name, middleName, surname, gender, birthDate, birthPlace,
                            username, pass, email, nationality, userType);

            if(_userAccountService.UpdateUser(user))
            {
                MessageBox.Show("User data updated");
            }
            else
            {
                MessageBox.Show("Failed to edit User data");
            }
        }




        // Create Document

        private void btnCreateDocument_Click(object sender, EventArgs e)
        {
            string docnumber = tbxCreateDocumentNumber.Text;
            string selectedUser = cbxCreateDocumentUsers.SelectedItem.ToString();

            if (cbxCreateDocumentType.SelectedItem != null)
            {
                string documentType = cbxCreateDocumentType.SelectedItem.ToString();

                DateOnly? dateOfIssue = dtpCreateDocumentDateofissue.Value == null ? null : DateOnly.FromDateTime(dtpCreateDocumentDateofissue.Value.Date);
                DateOnly? dateOfExpire = dtpCreateDocumentDateofexpire.Value == null ? null : DateOnly.FromDateTime(dtpCreateDocumentDateofexpire.Value.Date);

                if (documentType == "ID")
                {
                    int userId = ExtractUserId(selectedUser);
                    _IdService.CreateID(userId, docnumber, dateOfIssue ?? default(DateOnly), dateOfExpire ?? default(DateOnly));
                    MessageBox.Show("ID document created successfully!");
                }
                else if (documentType == "Passport")
                {
                    int userId = ExtractUserId(selectedUser);
                    _PassportService.CreatePassport(userId, docnumber, dateOfIssue ?? default(DateOnly), dateOfExpire ?? default(DateOnly));
                    MessageBox.Show("Passport document created successfully!");
                }
                else
                {
                    MessageBox.Show("Invalid document type selected!");
                }
            }
            else
            {
                MessageBox.Show("Please select a document type!");
            }
        }

        private int ExtractUserId(string userString)
        {
            // Extracting user ID from the user string
            int startIndex = userString.IndexOf("ID:") + 4;
            int endIndex = userString.IndexOf(",");
            string userIdString = userString.Substring(startIndex, endIndex - startIndex);
            return int.Parse(userIdString.Trim());
        }

        private void btnCreateDocumentRefreshList_Click(object sender, EventArgs e)
        {
            List<DocumentPassport> passports = _PassportService.GetAllPassports();
            List<DocumentID> ids = _IdService.GetAllIDs();

            dgvCreateDocumentList.Rows.Clear();

            foreach (var passport in passports)
            {
                dgvCreateDocumentList.Rows.Add("Passport", passport.Id, passport.UserId, passport.PassportNumber, passport.DateOfIssue, passport.DateOfExpire);
            }

            foreach (var id in ids)
            {
                dgvCreateDocumentList.Rows.Add("ID", id.Id, id.UserId, id.IDNumber, id.DateOfIssue, id.DateOfExpire);
            }
        }


        // edit document

        private void dgvEditDocumentList_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEditDocumentList.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvEditDocumentList.SelectedRows[0];

                // Set the text of tbxEditDocumentId to the value of the "DocumentID" cell
                tbxEditDocumentId.Text = selectedRow.Cells["DocumentID"].Value?.ToString() ?? string.Empty;

                // Populate the ComboBox with users
                FillComboBoxWithUsers(cbxEditDocumentUsers);

                // Retrieve selected user ID
                string userId = selectedRow.Cells["UserID"].Value?.ToString() ?? string.Empty;

                // Select the corresponding user in the ComboBox
                for (int i = 0; i < cbxEditDocumentUsers.Items.Count; i++)
                {
                    string userInfo = cbxEditDocumentUsers.GetItemText(cbxEditDocumentUsers.Items[i]);
                    if (userInfo.Contains("ID: " + userId))
                    {
                        cbxEditDocumentUsers.SelectedIndex = i;
                        break;
                    }
                }

                // Update other document information
                cbxEditDocumentType.Text = selectedRow.Cells["Type"].Value?.ToString() ?? string.Empty;
                tbxEditDocumentNumber.Text = selectedRow.Cells["DocumentNumber"].Value?.ToString() ?? string.Empty;

                DateOnly? dateOfIssueValue = selectedRow.Cells["DateOfIssue"].Value as DateOnly?;

                if (dateOfIssueValue != null)
                {
                    // Validate the year to ensure it's within a reasonable range
                    if (dateOfIssueValue.Value.Year >= 1900 && dateOfIssueValue.Value.Year <= DateTime.Now.Year)
                    {
                        dtpEditDocumentDateofissue.Value = new DateTime(dateOfIssueValue.Value.Year, dateOfIssueValue.Value.Month, dateOfIssueValue.Value.Day);
                    }
                    else
                    {
                        // Handle invalid date, maybe set to a default or show an error
                        dtpEditDocumentDateofissue.Value = dtpEditDocumentDateofissue.MinDate;
                    }
                }
                else
                {
                    // Handle case where dateOfIssueValue is null
                    dtpEditDocumentDateofissue.Value = dtpEditDocumentDateofissue.MinDate;
                }


                DateOnly? dateOfExpireValue = selectedRow.Cells["DateOfExpire"].Value as DateOnly?;
                if (dateOfExpireValue != null)
                {
                    dtpEditDocumentDateofexpire.Value = new DateTime(dateOfExpireValue.Value.Year, dateOfExpireValue.Value.Month, dateOfExpireValue.Value.Day);
                }
                else
                {
                    dtpEditDocumentDateofexpire.Value = dtpEditDocumentDateofexpire.MinDate;
                }
            }
        }


        private void btnEditDocumentRefreshList_Click(object sender, EventArgs e)
        {
            List<DocumentPassport> passports = _PassportService.GetAllPassports();
            List<DocumentID> ids = _IdService.GetAllIDs();

            dgvEditDocumentList.Rows.Clear();

            foreach (var passport in passports)
            {
                dgvEditDocumentList.Rows.Add("Passport", passport.Id, passport.UserId, passport.PassportNumber, passport.DateOfIssue, passport.DateOfExpire);
            }

            foreach (var id in ids)
            {
                dgvEditDocumentList.Rows.Add("ID", id.Id, id.UserId, id.IDNumber, id.DateOfIssue, id.DateOfExpire);
            }
        }

        private void btnEditDocument_Click(object sender, EventArgs e)
        {
            // Get document details from the form
            string documentType = cbxEditDocumentType.Text;
            string selectedUser = cbxEditDocumentUsers.SelectedItem?.ToString();
            int userId = ExtractUserId(selectedUser);
            string docnumber = tbxEditDocumentNumber.Text;
            int.TryParse(tbxEditDocumentId.Text, out int documentId); // Parse the document ID from tbxEditDocumentId
            DateOnly issueDate = DateOnly.FromDateTime(dtpEditDocumentDateofissue.Value.Date);
            DateOnly expiryDate = DateOnly.FromDateTime(dtpEditDocumentDateofexpire.Value.Date);

            // Update the document based on its type
            if (documentType == "ID")
            {
                DocumentID documentID = new DocumentID(documentId, userId, docnumber, issueDate, expiryDate); // Pass the documentId to the constructor
                if (_IdService.UpdateID(documentID))
                {
                    MessageBox.Show("ID document updated successfully!");
                }
                else
                {
                    MessageBox.Show("ID document NOT UPDATED!");

                }
            }
            else if (documentType == "Passport")
            {
                DocumentPassport documentPassport = new DocumentPassport(documentId, userId, docnumber, issueDate, expiryDate); // Pass the documentId to the constructor
                _PassportService.UpdatePassport(documentPassport);
                MessageBox.Show("Passport document updated successfully!");
            }
            else
            {
                MessageBox.Show("Invalid document type!");
            }
        }

        private void btnCreateFlightCreate_Click(object sender, EventArgs e)
        {
            Airport origin = (Airport)cbxCreateFlightOrigin.SelectedItem;
            Airport destination = (Airport)cbxCreateFlightDestination.SelectedItem;

            Plane plane = (Plane)cbxCreateFlightPlane.SelectedItem;

            DateTime takeoff = dtpCreateFlightTakeoff.Value;
            DateTime landing = dtpCreateFlightLanding.Value;

            double price;
            if (!double.TryParse(tbxCreateFlightPrice.Text, out price))
            {
                MessageBox.Show("Please enter a valid price.");
                return;
            }

            FlightStatus flightStatus = (FlightStatus)cbxCreateFlightStatus.SelectedIndex;

            bool result = _flightService.CreateFlight(origin, destination, takeoff, landing, price, plane, flightStatus);

            if (result)
            {
                MessageBox.Show("Flight created successfully.");
            }
            else
            {
                MessageBox.Show("Failed to create the flight.");
            }
        }

        private void btnCreateFlightRefresh_Click(object sender, EventArgs e)
        {
            dgvCreateFlightList.Rows.Clear();

            List<Flight> flights = _flightService.GetAllFlights();

            foreach (var flight in flights)
            {
                dgvCreateFlightList.Rows.Add(
                    flight.FlightID,
                    flight.DepartureAirport.AirportName,
                    flight.ArrivalAirport.AirportName,
                    flight.DepartureTime,
                    flight.ArrivalTime,
                    flight.Price,
                    flight.Plane.PlaneModel,
                    flight.FlightStatus
                );

            }
        }





        private void btnEditFlightRefreshList_Click(object sender, EventArgs e)
        {
            dgvEditFlightList.Rows.Clear(); // Clear existing rows

            List<Flight> flights = _flightService.GetAllFlights();

            foreach (var flight in flights)
            {
                dgvEditFlightList.Rows.Add(
                    flight.FlightID,
                    flight.DepartureAirport.AirportName,
                    flight.ArrivalAirport.AirportName,
                    flight.DepartureTime,
                    flight.ArrivalTime,
                    flight.Price,
                    flight.Plane.PlaneModel,
                    flight.FlightStatus
                );
            }
        }


        private void dgvEditFlightList_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEditFlightList.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvEditFlightList.SelectedRows[0];

                // Corrected to match the actual column name as per the provided context
                decimal price = Convert.ToDecimal(selectedRow.Cells["Price"].Value);
                tbxEditFlightPrice.Text = price.ToString();

                cbxEditFlightOrigin.SelectedItem = selectedRow.Cells["DepartureAirport"].Value as Airport;
                cbxEditFlightDestination.SelectedItem = selectedRow.Cells["ArrivalAirport"].Value as Airport;
                dtpEditFlightTakeoff.Value = (DateTime)selectedRow.Cells["DepartureTime"].Value;
                dtpEditFlightLanding.Value = (DateTime)selectedRow.Cells["ArrivalTime"].Value;
                tbxEditFlightPrice.Text = price.ToString();
                cbxEditFlightPlane.SelectedItem = selectedRow.Cells["Plane"].Value as Plane;
                cbxEditFlightStatus.SelectedItem = selectedRow.Cells["FlightStatus"].Value;
            }
        }


        private void btnEditFlightEdit_Click(object sender, EventArgs e)
        {
            int flightId = Convert.ToInt32(dgvEditFlightList.SelectedRows[0].Cells["FlightID"].Value);
            Airport origin = cbxEditFlightOrigin.SelectedItem as Airport;
            Airport destination = cbxEditFlightDestination.SelectedItem as Airport;
            DateTime takeoff = dtpEditFlightTakeoff.Value;
            DateTime landing = dtpEditFlightLanding.Value;
            double price = Convert.ToDouble(tbxEditFlightPrice.Text);
            Plane plane = (Plane)cbxEditFlightPlane.SelectedItem;
            FlightStatus flightStatus = (FlightStatus)cbxEditFlightStatus.SelectedItem;

            Flight flight = new Flight(flightId, origin, destination, takeoff, landing, price, plane, flightStatus);

            bool result = _flightService.UpdateFlight(flight);

            if (result)
            {
                MessageBox.Show("Flight updated successfully.");
            }
            else
            {
                MessageBox.Show("Failed to update the flight.");
            }
        }

    }
}
