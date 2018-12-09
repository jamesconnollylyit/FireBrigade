using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using DBLibrary;

namespace DBLibrary.Tests
{
    public class BuildingProcessTests
    {
        [Theory]
        [InlineData ("name", "no", "postcode", int.MinValue, int.MinValue, int.MinValue, int.MinValue, "firstname", "surname", "houseno", "street", "town", "phoneno", "docdesc", "docpath", false)]
        [InlineData("name", "no", "postcode", int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, "firstname", "surname", "houseno", "street", "town", "phoneno", "docdesc", "docpath", false)]
        [InlineData("-name", "no", "postcode", 0, 1, 0, 1, "firstname", "surname", "houseno", "street", "town", "phoneno", "docdesc", "docpath", true)]
        [InlineData("", "no", "postcode", 0, 0, 0, 0, "firstname", "surname", "houseno", "street", "town", "phoneno", "docdesc", "docpath", false)]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "no", "postcode", 0, 0, 0, 0, "firstname", "surname", "houseno", "street", "town", "phoneno", "docdesc", "docpath", false)]


        public void ValidateBuildingInput_ValuesShouldVerify(
            string tbxBuildingName,
            string tbxBuildingNo,
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
            string tbxDocumentPath, bool expected)
        {
            // Arrange
            BuildingProcess buildingProcess = new BuildingProcess();

            // Act
            bool actual = buildingProcess.ValidateBuildingInput(
                tbxBuildingName,
             tbxBuildingNo,
             tbxPostcode,
             cboBuildingType,
             typeList,
             cboBuildingArea,
             areaList,
             tbxContactFirstName,
             tbxContactSurname,
             tbxContactHouseNo,
             tbxContactStreet,
             tbxContactTown,
             tbxContactPhoneNo,
             tbxDocumentDesc,
             tbxDocumentPath);

            // Assert
            Assert.Equal(expected, actual);         
        }

        [Fact]

        public void AddBuildingToBuildingList_ShouldWork()
        {
            // Arrange
            BuildingProcess buildingProcess = new BuildingProcess();
            Building building = new Building { BuildingID = 1, BuildingNo = "No1", BuildingName = "LYIT", BuildingTown = "Letterkenny", BuildingCity = "Letterkenny", BuildingPostCode = "HH45 6GG", TypeID = 1, AreaID = 1, ContactID = 1, DocumentID = 1 };
            List<Building> buildings = new List<Building>();

            // Act
            buildingProcess.AddBuildingToBuildingList(buildings, building);

            // Assert
            Assert.True(buildings.Count == 1);
            Assert.Contains<Building>(building, buildings);
        }

        [Theory]
        [InlineData(0, "No1", "LYIT", "Letterkenny", "Letterkenny", "HH45 6GG", 1, 1, 1, 1, "BuildingNo")]
        [InlineData (1, "", "LYIT", "Letterkenny", "Letterkenny", "HH45 6GG", 1, 1, 1, 1, "BuildingNo")]
        [InlineData(1, "No1", "", "Letterkenny", "Letterkenny", "HH45 6GG", 1, 1, 1, 1, "BuildingNo")]

        public void AddBuildingToBuildingList_ShouldFail(int buildingID, string buildingNo, string buildingName, string town, string city, string postcode, int typeID, int areaID, int contactID, int documentID, string param)
        {
            // Arrange
            BuildingProcess buildingProcess = new BuildingProcess();
            Building building = new Building {
                BuildingID = buildingID,
                BuildingNo = buildingNo,
                BuildingName = buildingName,
                BuildingTown = town,
                BuildingCity = city,
                BuildingPostCode = postcode,
                TypeID = typeID,
                AreaID = areaID,
                ContactID = contactID,
                DocumentID = documentID
            };
            List<Building> buildings = new List<Building>();

            // Act
            buildingProcess.AddBuildingToBuildingList(buildings, building);

            // Assert
            Assert.True(buildings.Count == 0);
            Assert.DoesNotContain<Building>(building, buildings);
        }

    }
}
