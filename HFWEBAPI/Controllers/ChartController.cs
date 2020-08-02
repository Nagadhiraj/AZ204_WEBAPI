using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HFWEBAPI.Business;
using HFWEBAPI.DataAccess;
using HFWEBAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HFWEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChartController : ControllerBase
    {
        private IConfiguration config;

        public ChartController(IConfiguration Configuration)
        {
            config = Configuration;
        }

        //[Route("api/[controller]")]
        [HttpPost]
        public async Task<List<ChartEntity>> Post([FromBody] ChartQuery chart)
        {
            try
            {
                IChartRepository<ChartEntity> Respository = new ChartRepository<ChartEntity>(config);
                ChartService<ChartEntity> service = new ChartService<ChartEntity>();
                if (chart.queryParameter == "findbyuserid")
                {
                    var charts = await Respository.GetItemsAsync(d => d.userId == chart.userId && d.userId != null && d.isActive == true, "Chart");
                    return charts.ToList();
                }
                //else if (chart.queryParameter == "findbydate")
                //{
                //    var charts = await Respository.GetItemsAsync(d => d.date == chart.date && d.isActive == true && d.isAvailable == true, "chartMaster");
                //    return charts.ToList();
                //}
                else
                {
                    return await service.CreateItemAsync(chart, config);
                    //ChartEntity newchart = new ChartEntity();
                    //newchart.id = null;
                    //newchart.name = chart.name;
                    //newchart.remarks = chart.remarks;
                    //newchart.mailcontent = chart.mailcontent;
                    //newchart.mailsubject = chart.mailsubject;
                    //newchart.userId = chart.userId;
                    //newchart.email = chart.email;
                    //newchart.phone = chart.phone;
                    //newchart.username = chart.username;
                    //newchart.isActive = chart.isActive;
                    //newchart.createdBy = chart.createdBy;
                    //newchart.createdDate = chart.createdDate;
                    //newchart.modifiedBy = chart.modifiedBy;
                    //newchart.modifiedDate = chart.modifiedDate;
                    //newchart.chartAttachments = chart.chartAttachments;

                    //var cht = await Respository.CreateItemAsync(newchart, "Chart");
                    //List<ChartEntity> chtList = new List<ChartEntity>();
                    //return chtList;
                }
            }
            catch
            {
                List<ChartEntity> chtList = new List<ChartEntity>();
                return chtList;
            }
        }

        //[Route("api/[controller]/{id:int}")]
        [HttpPut]
        public async Task<bool> Put([FromBody] ChartEntity chart)
        {
            try
            {
                IChartRepository<ChartEntity> Respository = new ChartRepository<ChartEntity>(config);
                await Respository.UpdateItemAsync(chart.id, chart, "Chart");

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
