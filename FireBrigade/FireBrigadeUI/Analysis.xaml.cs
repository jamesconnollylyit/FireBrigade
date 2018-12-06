using DBLibrary;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace FireBrigadeUI
{
    /// <summary>
    /// Interaction logic for Analysis.xaml
    /// </summary>
    public partial class Analysis : UserControl
    {

        FireDBEntities db = new FireDBEntities("metadata=res://*/FireBrigadeModel.csdl|res://*/FireBrigadeModel.ssdl|res://*/FireBrigadeModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.8.120;initial catalog=FireDB;persist security info=True;user id=fireuser;password=password;pooling=False;MultipleActiveResultSets=True;App=EntityFramework'");


        List<DBLibrary.Building> buildingList = new List<DBLibrary.Building>();
        List<Contact> contactList = new List<Contact>();
        List<User> userList = new List<User>();
        List<Log> logList = new List<Log>();

        enum AnalysisType
        {
            Count,
            Summary,
            Statistics
        }

        private AnalysisType analysisType = new AnalysisType();

        enum TableSelected
        {
            Building,
            User,
            Log,
            Contact
        }

        private TableSelected tableSelected = new TableSelected();


        public Analysis()
        {
            InitializeComponent();
        }

        private void btnAnalyse_Click(object sender, RoutedEventArgs e)
        {
            // Clear variables. Record count used to display
            // count beside each record. CountSummary used to display
            // sum of all records in selected database table
            string output = "";
            int recordCount = 0;
            int countSummary = 0;
            //Clear text output during each button click
            tbkAnalysisOutput.Text = "";
            if (analysisType == AnalysisType.Summary && tableSelected == TableSelected.Building)
            {
                countSummary = buildingList.Count;              
                foreach (var item in buildingList)
                {
                    recordCount++;
                    output = output + Environment.NewLine + $"Record {recordCount} is for building named {item.BuildingName} , which is located in {item.BuildingTown}, in city {item.BuildingCity} " + Environment.NewLine;                                                    
                }
                output = output + Environment.NewLine + $"Total records = {recordCount}" + Environment.NewLine;
                tbkAnalysisOutput.Text = output;
            }
            if (analysisType == AnalysisType.Summary && tableSelected == TableSelected.Contact)
            {
                countSummary = contactList.Count;
                foreach (var item in contactList)
                {
                    recordCount++;
                    output = output + Environment.NewLine + $"Record {recordCount} is for contact whose name is {item.FName} {item.SName} , and who lives in {item.HouseNo}, {item.Street} in the town {item.Town}. Contact phone no is {item.PhoneNo} " + Environment.NewLine;
                }
                output = output + Environment.NewLine + $"Total records = {recordCount}";
                tbkAnalysisOutput.Text = output;
            }

            if (analysisType == AnalysisType.Summary && tableSelected == TableSelected.User)
            {
                countSummary = userList.Count;
                int level1CountSummary = 0;
                int level2CountSummary = 0;
                int level3CountSummary = 0;
                output = "Summary information for user database table." + Environment.NewLine;
                foreach (var item in userList)
                {
                    recordCount++;                  
                    output = output + Environment.NewLine + $"Record {recordCount} is for user whose name is {item.Forename} {item.Surname} , with username {item.Username}. This user has access rights at level  {item.LevelID} which is for {item.AccessLevel.JobRole} job role." + Environment.NewLine;
                    if (item.LevelID == 1)
                    {
                        level1CountSummary++;
                    }
                    if (item.LevelID == 2)
                    {
                        level2CountSummary++;
                    }
                    if (item.LevelID == 3)
                    {
                        level3CountSummary++;
                    }
                   
                }
                output = output + Environment.NewLine + $"Total users with level 1 access is {level1CountSummary}." + Environment.NewLine;
                output = output + Environment.NewLine + $"Total users with level 2 access is {level2CountSummary}." + Environment.NewLine;
                output = output + Environment.NewLine + $"Total users with level 3 access is {level3CountSummary}." + Environment.NewLine;
                output = output + Environment.NewLine + $"Total no of records = {recordCount}";
                tbkAnalysisOutput.Text = output;

            }

            if (analysisType == AnalysisType.Summary && tableSelected == TableSelected.Log)
            {
                 
                foreach (var item in logList)
                {
                    recordCount++;
                    output = output + Environment.NewLine + $"Record {recordCount} is for log created by {item.User.Forename} {item.User.Surname} " +
                        $"whose user ID is {item.UserID}. Log was created on {item.Date}. This log is registered for the {item.Category} category. " +
                        $"The description of this log is {item.Description}" + Environment.NewLine;
                }
                output = output + Environment.NewLine + $"Total records = {recordCount}" + Environment.NewLine;
                tbkAnalysisOutput.Text = output;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Load entries from the databaase tables that will be
            // potentially examined
            foreach (var buildingRecord in db.Buildings)
            {
                buildingList.Add(buildingRecord);
            }
            foreach (var contactRecord in db.Contacts)
            {
                contactList.Add(contactRecord);
            }
            foreach (var logRecord  in db.Logs)
            {
                logList.Add(logRecord);
            }
            foreach (var userRecord in db.Users)
            {
                userList.Add(userRecord);
            }          
        }
    

        private void cboAnalysisType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check that an option has been selected
            // The selectedIndex will be 0 if an option is not selected
            if (cboAnalysisType.SelectedIndex > 0)
            {
                if (cboAnalysisType.SelectedIndex == 1)
                {
                    analysisType = AnalysisType.Summary;
                }
                if (cboAnalysisType.SelectedIndex == 2)
                {
                    analysisType = AnalysisType.Count;
                }
                if (cboAnalysisType.SelectedIndex == 3)
                {
                    analysisType = AnalysisType.Statistics;
                }
            }
        }

        private void cboChooseTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboChooseTable.SelectedIndex >0)
            {
                if (cboChooseTable.SelectedIndex == 1)
                {
                    tableSelected = TableSelected.Building;
                }
                if (cboChooseTable.SelectedIndex == 2)
                {
                    tableSelected = TableSelected.User;
                }
                if (cboChooseTable.SelectedIndex == 3)
                {
                    tableSelected = TableSelected.Log;
                }
                if (cboChooseTable.SelectedIndex == 4)
                {
                    tableSelected = TableSelected.Contact;
                }
            }
        }
    }
}
