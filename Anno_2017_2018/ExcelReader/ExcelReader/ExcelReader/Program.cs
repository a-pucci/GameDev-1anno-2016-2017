using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

namespace ExcelReader
{
    class Program
    {
        #region EXAMPLE
        static void GenerateExcel()
        {
            //file excel
            IWorkbook workbook = new XSSFWorkbook();
            using (FileStream stream = new FileStream("nome_file.xlsx", FileMode.OpenOrCreate))
            {
                ISheet sheet = workbook.CreateSheet("SFXs");
                IRow row = sheet.CreateRow(0);
                ICell cell = row.CreateCell(0);
                cell.SetCellValue("ciao");
                //accedere a una cella
                string cellString = sheet.GetRow(0).GetCell(0).StringCellValue;
                //accedere a tutte le righe
                for (int i = 0; i <= sheet.LastRowNum; i++)
                {

                }
                //scrittura file excel
                workbook.Write(stream);
            }
        }
        #endregion

        static void GenerateXML()
        {
            IWorkbook workbook = new XSSFWorkbook();
            using (FileStream stream = new FileStream("localization-eng-fra.ods", FileMode.OpenOrCreate))
            {
                ISheet sheet = new XSSFSheet();
                string cellString = sheet.GetRow(0).GetCell(0).StringCellValue;
                //accedere a tutte le righe
                for (int i = 0; i <= sheet.LastRowNum; i++)
                {

                }
                //scrittura file excel
                workbook.Write(stream);
            }
        }
    }
}
