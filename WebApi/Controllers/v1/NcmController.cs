using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fingers10.ExcelExport.ActionResults;
using System.Linq;
using Application.Features.ncm.Queries;
using System.IO;
using System.Data;
using Microsoft.AspNetCore.Http;
using ExcelDataReader;
using Application.Features.ncm.Commands.UpdatePartnerBankRates;
using Application.Features.ncm.Commands.UpdateCustomerRates;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class NcmController : BaseApiController
    {
        [HttpGet("getddlistreport")]
        public async Task<IActionResult>  getddlistreport(string region,string location,string customer)
        {
            var ddl = await Mediator.Send(new getNcmReportddlQuery { region = region,location=location,customer=customer } );

            return Ok(ddl);
        }

        [HttpGet("GetPartnerBank")]
        public async Task<IActionResult> GetPartnerBank()
        {
            var objcustomer = await Mediator.Send(new GetPartnerBankRatesQuery { });

            var t = objcustomer.Data;

            var m = t.AsEnumerable();

            return new ExcelResult<PartnerBankRates>(m, "demo", "test");

        }
        [HttpGet("GetCustomer")]
        public async Task<IActionResult> GetCustomer()
        {

            var objcustomer = await Mediator.Send(new getCustomerQuery { });

            var t = objcustomer.Data;

            var m = t.AsEnumerable();

            return new ExcelResult<ncmbillingrate>(m, "demo", "test");

        }

        [HttpGet("getNcmReport")]
        public async Task<IActionResult> getNcmReport(string fromdate, string todate, string customer, string region, string location,string crn,string reporttype)
        {
            var objcustomer = await Mediator.Send(new getNcmReportQuery { fromdate= fromdate, todate= todate, customer= customer, region= region , location = location,crn=crn,reporttype=reporttype });

            var t = objcustomer.Data;

            var m = t.AsEnumerable();

            return new ExcelResult<ncmreportoutput>(m, "demo", "test");
        }

        [HttpGet("getNcmReportSum")]
        public async Task<IActionResult> getNcmReportSum(string fromdate, string todate, string customer, string region, string location, string crn, string reporttype)
        {
            var objcustomer = await Mediator.Send(new getNcmReportSumQuery { fromdate = fromdate, todate = todate, customer = customer, region = region, location = location, crn = crn, reporttype = reporttype });

            var t = objcustomer.Data;

            var m = t.AsEnumerable();

            return new ExcelResult<ncmreportoutputsum>(m, "demo", "test");
        }

        [HttpPost("UploadPartnerExcel")]
        public async Task<IActionResult> UploadPartnerExcel()
        {
            Stream fs = new System.IO.MemoryStream();
            DataSet dsexcelRecords = new DataSet();
            IExcelDataReader reader = null;
            string message = "";
            List<CustomerBranchMaster> CustomerBranchMasterlst = new List<CustomerBranchMaster>();

            IFormFile file = Request.Form.Files[0];

            if (file != null && file.Length != 0)
            {
                file.CopyTo(fs);

                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];

                if (extension.EndsWith(".xls") || extension.EndsWith(".xlsx"))
                {
                    reader = ExcelReaderFactory.CreateReader(fs);
                    dsexcelRecords = reader.AsDataSet();
                    reader.Close();
                }
                else
                {
                    message = "The file format is not supported.";
                }

                if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
                {
                    DataTable dtStudentRecords = dsexcelRecords.Tables[0];

                    for (int i = 1; i < dtStudentRecords.Rows.Count; i++)
                    {
                        if (dtStudentRecords.Rows[i][7].ToString() != "")
                        {
                            CustomerBranchMaster objCustomerBranchMaster = new CustomerBranchMaster();
                            objCustomerBranchMaster.customerbranchcode = Convert.ToString(dtStudentRecords.Rows[i][5]);
                            objCustomerBranchMaster.customercode = Convert.ToString(dtStudentRecords.Rows[i][3]);
                            objCustomerBranchMaster.PartnerBankRate = Decimal.Parse(dtStudentRecords.Rows[i][7].ToString());
                            CustomerBranchMasterlst.Add(objCustomerBranchMaster);
                        }
                    }
                    var result = await Mediator.Send(new UpdatePartnerBankRatesCommand { CustomerBranchMasterlst = CustomerBranchMasterlst });

                    return Ok(result);
                }
            }
            return Ok("");
        }

        [HttpPost("UploadExcel")]
        public async Task<IActionResult> UploadExcel()
        {
            Stream fs = new System.IO.MemoryStream();
            DataSet dsexcelRecords = new DataSet();
            IExcelDataReader reader = null;
            string message = "";
            List<customermaster> lstcustomer = new List<customermaster>();

            IFormFile file = Request.Form.Files[0];

            if (file != null && file.Length != 0)
            {

                file.CopyTo(fs);

                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
              
                if (extension.EndsWith(".xls") || extension.EndsWith(".xlsx"))
                {
                    reader = ExcelReaderFactory.CreateReader(fs);
                    dsexcelRecords = reader.AsDataSet();
                    reader.Close();
                }
                else
                {
                    message = "The file format is not supported.";
                }

                if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
                {
                    DataTable dtStudentRecords = dsexcelRecords.Tables[0];
                    for (int i = 1; i < dtStudentRecords.Rows.Count; i++)
                    {
                        if (dtStudentRecords.Rows[i][5].ToString() !="")
                        {
                            customermaster objcust = new customermaster();

                            objcust.customercode = Convert.ToString(dtStudentRecords.Rows[i][0]);
                            //objcust.customername = Convert.ToString(dtStudentRecords.Rows[i][2]);
                            objcust.ncmbillingrate = Decimal.Parse(dtStudentRecords.Rows[i][5].ToString());

                            lstcustomer.Add(objcust);
                        }
                    }

                    var result=await Mediator.Send(new UpdateCustomerRatesCommand { Customerlst = lstcustomer });

                    return Ok(result);
                }



            }
            return Ok("");
        }
    }
}
