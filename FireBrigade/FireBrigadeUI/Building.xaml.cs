using DBLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FireBrigadeUI
{
    /// <summary>
    /// Interaction logic for Building.xaml
    /// </summary>
    public partial class Building : UserControl
    {
        FireDBEntities db = new FireDBEntities("metadata=res://*/FireBrigadeModel.csdl|res://*/FireBrigadeModel.ssdl|res://*/FireBrigadeModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.8.120;initial catalog=FireDB;persist security info=True;user id=fireuser;password=password;pooling=False;MultipleActiveResultSets=True;App=EntityFramework'");

        List<Area> areaList = new List<Area>();
        List<DBLibrary.Type> typeList = new List<DBLibrary.Type>();
        List<DBLibrary.Building> buildingList = new List<DBLibrary.Building>();

        enum DBOperation
        {
            Add,
            Modify         
        }

        DBOperation dbOperation = new DBOperation();

        public Building()
        {
            InitializeComponent();
        }

        private void cboBuildingType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cboBuildingArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {


            bool dataValidated = ValidateBuildingInput();
            if (!dataValidated)
            {
                MessageBox.Show("Error with your data. Please check and try again.", "Save to database", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (dbOperation == DBOperation.Add && dataValidated)
            {
                DBLibrary.Building building = new DBLibrary.Building();
                building.BuildingName = tbxBuildingName.Text;
                building.BuildingNo = tbxBuildingNo.Text;
                building.BuildingTown = tbxTown.Text;
                building.BuildingCity = tbxCity.Text;
                building.BuildingPostCode = tbxPostcode.Text;
                // Added 1 to the slectedindex to match the correct record ID in the database
                building.TypeID = cboBuildingType.SelectedIndex + 1;
                building.AreaID = cboBuildingArea.SelectedIndex + 1;

                Contact contact = new Contact();
                contact.FName = tbxContactFirstName.Text;
                contact.SName = tbxContactSurname.Text;
                contact.HouseNo = tbxContactHouseNo.Text;
                contact.Street = tbxContactStreet.Text;
                contact.Town = tbxContactTown.Text;
                contact.PhoneNo = tbxContactPhoneNo.Text;

                Document document = new Document();
                document.DocDesc = tbxDocumentDesc.Text;
                document.Path = tbxDocumentPath.Text;

                int contactSaved = SaveContactRecord(contact);
                if (contactSaved == 1)
                {
                    building.ContactID = contact.ContactID;
                    int documentSaved = SaveDocumentRecord(document);
                    if (documentSaved == 1)
                    {
                        building.DocumentID = document.DocumentID;
                        int buildingSaved = SaveBuildingRecord(building);
                        if (buildingSaved == 1)
                        {
                            ClearAllControls();
                            RefreshBuildingsListView();
                            stkBuildingPanel.Visibility = Visibility.Collapsed;
                            tabBuildingContact.IsEnabled = false;
                            tabBuildingDocument.IsEnabled = false;
                            MessageBox.Show("Building record has been saved successfully.", "Save to database", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }

                }

            }
        }

       private int SaveContactRecord(Contact contact)
        {
           
            int saveSuccess = 0;
            try
            {
                db.Entry(contact).State = System.Data.Entity.EntityState.Added;
                saveSuccess = db.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Error saving contact record.", "Save to database", MessageBoxButton.OK, MessageBoxImage.Error);            
            }       
            return saveSuccess;
        }

        private int SaveDocumentRecord(Document document)
        {
            int saveSuccess = 0;
            try
            {
                db.Entry(document).State = System.Data.Entity.EntityState.Added;
                saveSuccess = db.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Error saving document record.", "Save to database", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return saveSuccess;
        }

        private int SaveBuildingRecord(DBLibrary.Building building)
        {
            int saveSuccess = 0;
            try
            {
                db.Entry(building).State = System.Data.Entity.EntityState.Added;
                saveSuccess = db.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Error saving building record.", "Save to database", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return saveSuccess;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddNewContact_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ModifySelectedContact_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteSelectedContact_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RefreshAreaList()
        {
            areaList.Clear();
            foreach (var areaRecord in db.Areas)
            {
                areaList.Add(areaRecord);
            }
            cboBuildingArea.ItemsSource = areaList;

            cboBuildingArea.Items.Refresh();
        }

        private void RefreshBuildingList()
        {
            typeList.Clear();
            foreach (var typeRecord in db.Types)
            {
                typeList.Add(typeRecord);
            }
            cboBuildingType.ItemsSource = typeList;
            cboBuildingType.Items.Refresh();
        }

        private void RefreshBuildingsListView()
        {
            buildingList.Clear();
            foreach (var buildingRecord in db.Buildings)
            {
                buildingList.Add(buildingRecord);
            }
            lstBuildingsList.ItemsSource = buildingList;
            lstBuildingsList.Items.Refresh();
        }

       

       

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshAreaList();
            RefreshBuildingList();
            RefreshBuildingsListView();
            
        }

        private bool ValidateBuildingInput()
        {
            bool validated = true;

            if (tbxBuildingName.Text.Length == 0 || tbxBuildingName.Text.Length > 30)
            {
                validated = false;
            }

            if (tbxBuildingNo.Text.Length == 0 || tbxBuildingNo.Text.Length > 10)
            {
                validated = false;
            }

            if (tbxPostcode.Text.Length == 0 || tbxPostcode.Text.Length > 30)
            {
                validated = false;
            }

            if (cboBuildingType.SelectedIndex <0 || cboBuildingType.SelectedIndex > typeList.Count -1)
            {
                validated = false;
            }

            if (cboBuildingArea.SelectedIndex <0 || cboBuildingArea.SelectedIndex > areaList.Count - 1)
            {
                validated = false;
            }

            if (tbxContactFirstName.Text.Length < 0 || tbxContactFirstName.Text.Length > 30)
            {
                validated = false;
            }

            if (tbxContactSurname.Text.Length <0 || tbxContactSurname.Text.Length > 30)
            {
                validated = false;
            }

            if (tbxContactHouseNo.Text.Length <0 || tbxContactHouseNo.Text.Length >30)
            {
                validated = false;
            }

            if (tbxContactStreet.Text.Length <0 || tbxContactStreet.Text.Length > 30)
            {
                validated = false;
            }

            if (tbxContactTown.Text.Length <0 || tbxContactTown.Text.Length > 30)
            {
                validated = false;
            }

            if (tbxContactPhoneNo.Text.Length < 0 || tbxContactPhoneNo.Text.Length > 20)
            {
                validated = false;
            }

            if (tbxDocumentDesc.Text.Length < 0 || tbxDocumentDesc.Text.Length > 30)
            {
                validated = false;
            }

            if (tbxDocumentPath.Text.Length < 0 || tbxDocumentPath.Text.Length > 260)
            {
                validated = false;
            }
            return validated;
        }

        private void submnuAddNewBuilding_Click(object sender, RoutedEventArgs e)
        {
            stkBuildingPanel.Visibility = Visibility.Visible;
            tabBuildingContact.IsEnabled = true;
            tabBuildingDocument.IsEnabled = true;
            dbOperation = DBOperation.Add;
            ClearAllControls();
        }

        private void ClearAllControls()
        {
            // Building tab
            tbxBuildingName.Text = "";
            tbxBuildingNo.Text = "";
            tbxTown.Text = "";
            tbxCity.Text = "";
            tbxPostcode.Text = "";
            cboBuildingType.SelectedIndex = -1;
            cboBuildingArea.SelectedIndex = -1;

            // Contacts tab
            tbxContactFirstName.Text = "";
            tbxContactSurname.Text = "";
            tbxContactHouseNo.Text = "";
            tbxContactStreet.Text = "";
            tbxContactTown.Text = "";
            tbxContactPhoneNo.Text = "";

            // Document tab        
            tbxDocumentPath.Text = "";
            tbxDocumentDesc.Text = "";
        }

        private void submnuModifySelectedBuilding_Click(object sender, RoutedEventArgs e)
        {
            stkBuildingPanel.Visibility = Visibility.Visible;
            dbOperation = DBOperation.Modify;
            var selectedBuilding = buildingList.ElementAt(lstBuildingsList.SelectedIndex);
            PopulateBuildingTabs(selectedBuilding.BuildingID);
        }

        private void submnuDeleteSelectedBuilding_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lstBuildingsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstBuildingsList.SelectedIndex > -1)
            {
                submnuModifySelectedBuilding.IsEnabled = true;
                submnuDeleteSelectedBuilding.IsEnabled = true;
            }         
        }

        private void btnContactOK_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnContactUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDocDescChange_Click(object sender, RoutedEventArgs e)
        {
            stkChangeDocDescription.Visibility = Visibility.Visible;
        }

        private void btnDocDescUpdateText_Click(object sender, RoutedEventArgs e)
        {
            tbxDocumentDesc.Text = tbxDocDescChangedText.Text;
            stkChangeDocDescription.Visibility = Visibility.Collapsed;
        }

        private void PopulateBuildingTabs(int buildingID)
        {
            var allBuildingInformation = from _building in db.Buildings.Where(t => t.BuildingID == buildingID)
                                         join _contact in db.Contacts on _building.ContactID equals _contact.ContactID
                                         join _document in db.Documents on _building.DocumentID equals _document.DocumentID
                                         join _type in db.Types on _building.TypeID equals _type.TypeID
                                         join _area in db.Areas on _building.AreaID equals _area.AreaID
                                         select new
                                         {
                                             _building.BuildingID,
                                             _building.BuildingName,
                                             _building.BuildingNo,
                                             _building.BuildingCity,
                                             _building.BuildingPostCode,
                                             _building.ContactID,
                                             _contact.FName,
                                             _contact.SName,
                                             _contact.HouseNo,
                                             _contact.Street,
                                             _contact.Town,
                                             _contact.PhoneNo,
                                             _document.DocumentID,
                                             _document.DocDesc,
                                             _document.Path,
                                             _type.TypeID,
                                             _area.AreaID
                                         };

            foreach (var record in allBuildingInformation)
            {
                if (record.ContactID > 0)
                {
                    tabBuildingContact.IsEnabled = true;
                    tbxContactFirstName.Text = record.FName;
                    tbxContactSurname.Text = record.SName;
                    tbxContactHouseNo.Text = record.HouseNo;
                    tbxContactStreet.Text = record.Street;
                    tbxContactTown.Text = record.Town;
                    tbxContactPhoneNo.Text = record.PhoneNo;
                }
                if (record.DocumentID > 0)
                {
                    tabBuildingDocument.IsEnabled = true;
                    tbxDocumentPath.Text = record.Path;
                    tbxDocumentDesc.Text = record.DocDesc;
                }

                if (record.BuildingID > 0)
                {
                    tbxBuildingName.Text = record.BuildingName;
                    tbxBuildingNo.Text = record.BuildingNo;
                    tbxTown.Text = record.Town;
                    tbxCity.Text = record.BuildingCity;
                    tbxPostcode.Text = record.BuildingPostCode;
                    cboBuildingType.SelectedIndex = record.TypeID;
                    cboBuildingArea.SelectedIndex = record.AreaID;
                }

            }
        }

        private void btnDocPathBrowse_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            // Set filter for file extension and default file extension
            dialog.DefaultExt = ".pdf";
            dialog.Filter = "pdf files (*.pdf)|*.pdf";
            // Display OpenFileDialog by calling the ShowDialog method
            bool? result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                tbxDocumentPath.Text = dialog.FileName.Trim();
            }
        }
    }
}
