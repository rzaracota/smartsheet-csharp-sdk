using Microsoft.VisualStudio.TestTools.UnitTesting;
using Smartsheet.Api;
using Smartsheet.Api.Models;

namespace mock_api_test_sdk_net60
{
    [TestClass]
    public class WorkspaceTests
    {
        [TestMethod]
        public void CreateWorkspace_Success()
        {
            SmartsheetClient client = HelperFunctions.SetupClient("Create Workspace - Valid");

            Workspace newWorkspace = new Workspace();

            newWorkspace.Name = "New workspace";
            
            Workspace workspace = client.WorkspaceResources.CreateWorkspace(newWorkspace);
            
            Assert.IsNotNull(workspace);
            Assert.IsNotNull(workspace.Id);
            Assert.IsNotNull(workspace.Name);
            Assert.IsNotNull(workspace.AccessLevel);
            Assert.IsNotNull(workspace.Permalink);
        }

        [TestMethod]
        public void ListSheets_IncludeOwnerInfo()
        {
            SmartsheetClient ss = HelperFunctions.SetupClient("List Sheets - Include Owner Info");

            PaginatedResult<Sheet> sheets = ss.SheetResources.ListSheets(new List<SheetInclusion> { SheetInclusion.OWNER_INFO }, null);

            Assert.IsNotNull(sheets.Data.Where(s => s.Owner.Equals("john.doe@smartsheet.com")).FirstOrDefault());
        }

        [TestMethod]
        public void CreateSheet_NoColumns()
        {
            SmartsheetClient ss = HelperFunctions.SetupClient("Create Sheet - Invalid - No Columns");

            Sheet sheetA = new Sheet
            {
                Name = "New Sheet",
                Columns = new List<Column>()
            };

            HelperFunctions.AssertRaisesException<SmartsheetException>(() =>
                ss.SheetResources.CreateSheet(sheetA),
                "The new sheet requires either a fromId or columns.");
        }
    }
}
