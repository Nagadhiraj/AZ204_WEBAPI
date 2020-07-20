using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HFWEBAPI.DataAccess;
using HFWEBAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HFWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private IConfiguration config;

        public AppointmentController(IConfiguration Configuration)
        {
            config = Configuration;
        }

        //[Route("api/[controller]")]
        [HttpPost]
        public async Task<List<AppointmentEntity>> Post([FromBody] AppointmentQuery appointment)
        {
            try
            {
                IAppointmentRepository<AppointmentEntity> Respository = new AppointmentRepository<AppointmentEntity>(config);
                if (appointment.queryParameter == "findbyuserid")
                {
                    var appointments = await Respository.GetItemsAsync(d => d.userId == appointment.userId && d.userId != null && d.isActive == true, "AppointmentMaster");
                    return appointments.ToList();
                }
                else if(appointment.queryParameter == "findbydate") 
                {
                    var appointments = await Respository.GetItemsAsync(d => d.date == appointment.date && d.isActive == true && d.isAvailable == true, "AppointmentMaster");
                    return appointments.ToList();
                }
                else
                {
                    AppointmentEntity newAppointment = new AppointmentEntity();
                    newAppointment.id = null;
                    newAppointment.date = appointment.date;
                    newAppointment.from = appointment.from;
                    newAppointment.to = appointment.to;
                    newAppointment.userId = appointment.userId;
                    newAppointment.email = appointment.email;
                    newAppointment.phone = appointment.phone;
                    newAppointment.username = appointment.username;
                    newAppointment.isActive = appointment.isActive;
                    newAppointment.isAvailable = appointment.isAvailable;
                    newAppointment.createdBy = appointment.createdBy;
                    newAppointment.createdDate = appointment.createdDate;
                    newAppointment.modifiedBy = appointment.modifiedBy;
                    newAppointment.modifiedDate = appointment.modifiedDate;

                    var appt = await Respository.CreateItemAsync(newAppointment, "AppointmentMaster");
                    List<AppointmentEntity> apptList = new List<AppointmentEntity>();
                    return apptList;
                }
            }
            catch
            {
                List<AppointmentEntity> apptList = new List<AppointmentEntity>();
                return apptList;
            }
        }

        //[Route("api/[controller]/{id:int}")]
        [HttpPut]
        public async Task<bool> Put([FromBody] AppointmentEntity appointment)
        {
            try
            {
                IAppointmentRepository<AppointmentEntity> Respository = new AppointmentRepository<AppointmentEntity>(config);
                await Respository.UpdateItemAsync(appointment.id, appointment, "AppointmentMaster");

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
