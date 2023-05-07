using ExcelDataReader;
using System.Data;
using System.Text;

namespace Classify.Api.ExcelReadre;

public class ExcelReader
{
    public ExcelReader()
    {
        long x = 0;
        var filepath = "C:\\Users\\Djava\\Desktop\\SirdaryoPrezident.xlsx";
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        using (var streamval = File.Open(filepath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = ExcelReaderFactory.CreateReader(streamval))
            {
                var configuration = new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = false
                    }
                };
                var dataSet = reader.AsDataSet(configuration);

                if (dataSet.Tables.Count > 0)
                {
                    var dataTable = dataSet.Tables[0];

                    /*
                     Console.WriteLine("Rows : " + dataTable.Rows[2].ToString());
                     Console.WriteLine("Columns : " + dataTable.Columns.Count);*/
                    foreach (DataRow i in dataTable.Rows)
                    {
                        Console.WriteLine(i[0] + " " + i[2]);
                        x++;
                    }
                    Console.WriteLine("result : " + x);
                }
                else
                {
                    Console.WriteLine("Sheet doesn't exist");
                }
            }
        }


    }
}