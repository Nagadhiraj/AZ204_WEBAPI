using HFWEBAPI.Common;
using HFWEBAPI.DataAccess;
using HFWEBAPI.Models;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFWEBAPI.Business
{
    public class ChartService<T> where T : class
    {

        public ChartService()
        {
        }

        public async Task<List<ChartEntity>> CreateItemAsync(ChartQuery chart, IConfiguration _config)
        {
            try
            {
                BlobStorageService objBlobService = new BlobStorageService(_config);
                EmailService objEmailService = new EmailService(_config);

                List<Attachment> attachments = new List<Attachment>();
                IChartRepository<ChartEntity> Respository = new ChartRepository<ChartEntity>(_config);


                ChartEntity newchart = new ChartEntity();
                newchart.id = null;
                newchart.name = chart.name;
                newchart.remarks = chart.remarks;
                newchart.mailcontent = chart.mailcontent;
                newchart.mailsubject = chart.mailsubject;
                newchart.chartAttachments = chart.chartAttachments;
                newchart.userId = chart.userId;
                newchart.email = chart.email;
                newchart.phone = chart.phone;
                newchart.username = chart.username;
                newchart.isActive = chart.isActive;
                newchart.createdBy = chart.createdBy;
                newchart.createdDate = chart.createdDate;
                newchart.modifiedBy = chart.modifiedBy;
                newchart.modifiedDate = chart.modifiedDate;

                // add base64 image to blob
                if (newchart.chartAttachments.Count() > 0)
                {
                    newchart.chartAttachments.ForEach(x =>
                    {
                    string[] strName = x.Name.Split('.');
                    //x.Path = newchart.userId + "/" + x.uId + "." + strName[strName.Length - 1];
                    //byte[] bytes = Encoding.ASCII.GetBytes(x.Base64String);

                    // upload to blob
                    //objBlobService.UploadFileToBlob(x.ContainerName, newchart.userId + "/" + x.uId + "." + strName[strName.Length - 1], Encoding.ASCII.GetBytes(x.Base64String), x.ContentType);

                    // add attachments 
                    attachments.Add(new Attachment{ Content = x.Base64String, Type = x.ContentType, Filename = x.Name, Disposition = "attachment", ContentId = "chart" });
                    });
                }


                var cht = await Respository.CreateItemAsync(newchart, "Chart");

                if (cht != null)
                {
                    // send Mail
                    var response = objEmailService.SendEmailAsync(newchart.mailsubject, newchart.email, "New Chart from Holistic Fitness", newchart.mailcontent, attachments);

                }


                List<ChartEntity> chtList = new List<ChartEntity>();
                return chtList;
            }

            catch(Exception ex)
            {
                List<ChartEntity> chtList = null;
                return chtList;
            }
        }
    }
}
    

