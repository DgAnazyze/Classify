using Classify.DataAccess.Interfaces;
using Classify.Domain.Entities;
using Classify.Domain.Enums;
using Classify.Service.Exceptions;
using Classify.Service.Interfaces;
using ExcelDataReader;
using System.Data;
using System.Text;

namespace Classify.Service.Services;

public class ExcelReaderService : IExcelReaderService
{
    protected readonly IRepository<Student> repository;

    public ExcelReaderService(IRepository<Student> repository)
    {
        this.repository = repository;
    }

    public async Task<bool> GetFromExcelAsync(string path)
    {
        //  var path = "C:\\Users\\Djava\\Desktop\\SirdaryoPrezident.xlsx";
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        using (var streamval = File.Open(path, FileMode.Open, FileAccess.Read))
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

                    foreach (DataRow i in dataTable.Rows)
                    {
                        Student student = new Student()
                        {
                            Id = (long)i[0],
                            Grade = (short)i[1],
                            FirstName = i[2].ToString(),
                            LastName = i[3].ToString(),
                            MidlleName = i[4].ToString(),
                            BirthCertificateSeria = i[5].ToString(),
                            BirthCertificateNumber = i[6].ToString(),
                            PassportSeria = i[7].ToString(),
                            PassportNumber = i[8].ToString(),
                            Gender = (Gender)i[9],
                            Region = i[10].ToString(),
                            School = i[11].ToString(),
                            Bearings = i[12].ToString(),
                            Language = i[13].ToString()
                        };

                        await this.repository.InserAsync(student);
                        await this.repository.SavaAsync();
                    }
                    return true;
                }
                else
                {
                    throw new CustomerException(404, "Table not found!");
                }
            }
        }

    }
}
