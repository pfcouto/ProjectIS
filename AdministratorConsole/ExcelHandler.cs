using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//create an alias to the namespace/type
using Excel = Microsoft.Office.Interop.Excel;


namespace AdministratorConsole
{
    class ExcelHandler
    {
        public static void OutputToExcel(string filename,List<Transaction> transactions)
        {
            string filePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\"+ filename;

            //Creates and Excel Application instance
            var excelAplication = new Excel.Application();
            excelAplication.Visible = false;

            //create an excel workbook with a default number of sheets;
            var excelWorkbook = excelAplication.Workbooks.Add();
            excelWorkbook.SaveAs(filePath, AccessMode: Excel.XlSaveAsAccessMode.xlNoChange);

            Excel.Workbook execlWorkbook = excelAplication.Workbooks.Open(filePath);
            Excel.Worksheet excelWorksheet = execlWorkbook.ActiveSheet;

            excelWorksheet.Cells[1, 1].Value = "Id";
            excelWorksheet.Cells[1, 2].Value = "Type";
            excelWorksheet.Cells[1, 3].Value = "Vcard Origin";
            excelWorksheet.Cells[1, 4].Value = "Vcard Destiny";
            excelWorksheet.Cells[1, 5].Value = "Date";
            excelWorksheet.Cells[1, 6].Value = "Value"; 
            excelWorksheet.Cells[1, 6].EntireRow.Font.Bold = true;

            int counter = 2;
            foreach (var transaction in transactions)
            {
                excelWorksheet.Cells[counter, 1].Value = transaction.Id.ToString();
                excelWorksheet.Cells[counter, 2].Value = transaction.Type.ToString();
                excelWorksheet.Cells[counter, 3].Value = transaction.VCard;
                excelWorksheet.Cells[counter, 4].Value = transaction.Payment_reference;
                excelWorksheet.Cells[counter, 5].Value = transaction.Date.ToString("dd-MM-yyyy HH:mm:ss");
                excelWorksheet.Cells[counter, 6].Value = transaction.Value.ToString() + "€";
                counter++;
            }
            excelWorksheet.Columns.AutoFit();


            execlWorkbook.Save();
            execlWorkbook.Close();
            excelAplication.Quit();

            excelAplication = null;
            excelWorksheet = null;
            execlWorkbook = null;
            GC.Collect();


        }

    }
}
