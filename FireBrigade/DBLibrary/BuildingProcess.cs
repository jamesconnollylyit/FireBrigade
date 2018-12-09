using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary
{
    public class BuildingProcess
    {
        /// <summary>
        /// Validates inputted building information
        /// </summary>
        /// <param name="tbxBuildingName">Textbox containing the building name</param>
        /// <param name="tbxBuildingNo">Textbox containing the building number</param>
        /// <param name="tbxPostcode">Textbox containing the building post code</param>
        /// <param name="cboBuildingType">Index of the Building type combobox</param>
        /// <param name="typeList">Number of records in the Type list</param>
        /// <param name="cboBuildingArea">Index of the Building area combobox</param>
        /// <param name="areaList">Number of records in the Area list</param>
        /// <param name="tbxContactFirstName">Textbox containing the first name of the building contact</param>
        /// <param name="tbxContactSurname">Textbox containing the Surname of the building contact</param>
        /// <param name="tbxContactHouseNo">Textbox containing the house number of the building contact</param>
        /// <param name="tbxContactStreet">Textbox containing the street name of the building contact</param>
        /// <param name="tbxContactTown">Textbox containing the town of the building contact</param>
        /// <param name="tbxContactPhoneNo">Textbox containing the phone number of the building contact</param>
        /// <param name="tbxDocumentDesc">Textbox containing the document description of the associated building document</param>
        /// <param name="tbxDocumentPath">Textbox containing the document path to the associated building document</param>
        /// <returns></returns>
        public bool ValidateBuildingInput(
            string tbxBuildingName, 
            string tbxBuildingNo , 
            string tbxPostcode, 
            int cboBuildingType, 
            int typeList, 
            int cboBuildingArea, 
            int areaList, 
            string tbxContactFirstName, 
            string tbxContactSurname, 
            string tbxContactHouseNo, 
            string tbxContactStreet, 
            string tbxContactTown, 
            string tbxContactPhoneNo, 
            string tbxDocumentDesc, 
            string tbxDocumentPath)
        {
            bool validated = true;

            if (tbxBuildingName.Length == 0 || tbxBuildingName.Length > 30)
            {
                validated = false;
            }

            if (tbxBuildingNo.Length == 0 || tbxBuildingNo.Length > 10)
            {
                validated = false;
            }

            if (tbxPostcode.Length == 0 || tbxPostcode.Length > 30)
            {
                validated = false;
            }

            if (cboBuildingType < 0 || cboBuildingType > typeList - 1)
            {
                validated = false;
            }

            if (cboBuildingArea < 0 || cboBuildingArea > areaList - 1)
            {
                validated = false;
            }

            if (tbxContactFirstName.Length < 0 || tbxContactFirstName.Length > 30)
            {
                validated = false;
            }

            if (tbxContactSurname.Length < 0 || tbxContactSurname.Length > 30)
            {
                validated = false;
            }

            if (tbxContactHouseNo.Length < 0 || tbxContactHouseNo.Length > 30)
            {
                validated = false;
            }

            if (tbxContactStreet.Length < 0 || tbxContactStreet.Length > 30)
            {
                validated = false;
            }

            if (tbxContactTown.Length < 0 || tbxContactTown.Length > 30)
            {
                validated = false;
            }

            if (tbxContactPhoneNo.Length < 0 || tbxContactPhoneNo.Length > 20)
            {
                validated = false;
            }

            if (tbxDocumentDesc.Length < 0 || tbxDocumentDesc.Length > 30)
            {
                validated = false;
            }

            if (tbxDocumentPath.Length < 0 || tbxDocumentPath.Length > 260)
            {
                validated = false;
            }
            return validated;
        }

        public void AddBuildingToBuildingList(List<Building> buildingList, Building building)
        {
            bool validated = true;
            if (building.BuildingID <1)
            {
                validated = false;
            }
            if (string.IsNullOrWhiteSpace(building.BuildingNo))
            {
                validated = false;
            }
            if (string.IsNullOrWhiteSpace(building.BuildingName))
            {
                validated = false;
            }
            if (validated)
            {
                buildingList.Add(building);
            }          
        }
    }
}
