using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobuv.Models;
using Bobuv.Models.Repository;
using System.Web.ModelBinding;
using System.IO;

namespace Bobuv.Pages.Admin
{
    public partial class Tovary: System.Web.UI.Page
    {
        private Repository repository = new Repository();

        protected void Page_Load(object sender, EventArgs e)
        {
           /* if (!IsPostBack)
            {
                string[] filePaths = Directory.GetFiles(Server.MapPath("~/img/"));
                List<ListItem> files = new List<ListItem>();
                foreach (string filePath in filePaths)
                {
                    string fileName = Path.GetFileName(filePath);
                    files.Add(new ListItem(fileName, "~/img/" + fileName));
                }
                GridView1.DataSource = files;
                GridView1.DataBind();
            }*/
        }

        public IEnumerable<Shoe> GetShoes()
        {
            return repository.Shoes;
        }

        public void UpdateShoe(int ShoeID)
        {
            Shoe myShoe = repository.Shoes
                .Where(p => p.ShoeId == ShoeID).FirstOrDefault();
            if (myShoe != null && TryUpdateModel(myShoe,
                new FormValueProvider(ModelBindingExecutionContext)))
            {
                repository.SaveShoe(myShoe);
            }
        }

        public void DeleteShoe(int ShoeID)
        {
            Shoe myShoe = repository.Shoes
                .Where(p => p.ShoeId == ShoeID).FirstOrDefault();
            if (myShoe != null)
            {
                repository.DeleteShoe(myShoe);
            }
        }

        public void InsertShoe()
        {
            Shoe myShoe = new Shoe();
            if (TryUpdateModel(myShoe,
                new FormValueProvider(ModelBindingExecutionContext)))
            {
                repository.SaveShoe(myShoe);
            }
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            // Specify the path on the server to
            // save the uploaded file to.


            // Before attempting to perform operations
            // on the file, verify that the FileUpload 
            // control contains a file.
            if (FileUpload1.HasFile)
            {
                string saveDir = @"\img\";


                string appPath = Request.PhysicalApplicationPath;
                String fileName = FileUpload1.FileName;
                string extension = System.IO.Path.GetExtension(fileName);
                if ((extension == ".jpg") || (extension == ".png"))
                {
                    // Append the name of the file to upload to the path.
                    string savePath = appPath + saveDir +
                          Server.HtmlEncode(FileUpload1.FileName);

                    // Call the SaveAs method to save the 
                    // uploaded file to the specified path.
                    // This example does not perform all
                    // the necessary error checking.               
                    // If a file with the same name
                    // already exists in the specified path,  
                    // the uploaded file overwrites it.
                    FileUpload1.SaveAs(savePath);

                    // Notify the user that their file was successfully uploaded.
                    UploadStatusLabel.Text = "Название файла: " + fileName;
                }
            }
            else
            {
                // Notify the user that a file was not uploaded.
                UploadStatusLabel.Text = "You did not specify a file to upload.";
            }

        }

    }
}