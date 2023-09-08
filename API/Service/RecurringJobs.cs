using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using API.Models;
using Hangfire;
using Hangfire.Common;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;

namespace API.Service
{
    public class RecurringJobs
    {
        
        private readonly IServiceProvider _serviceProvider;

        public RecurringJobs(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task SendReminderEmailsJob()
        {
            using var scope = _serviceProvider.CreateScope();
            var emailService = scope.ServiceProvider.GetRequiredService<EmailService>();
            var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            try
            {
                var emailList = await applicationDbContext.EmailDatas.Select(e => e.EMail).ToListAsync();

                foreach (var email in emailList)
                {
                    var emailSendModel = new EMailSendModel
                    {
                        ToEmail = email,
                        Subject = "Hatırlatma",
                        Body = "Merhaba, unutmayın!"
                    };

                    await emailService.SendEmailAsync(emailSendModel);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ConfigureRecurringJobs(IServiceProvider serviceProvider)
        {
            var recurringJobManager = serviceProvider.GetRequiredService<IRecurringJobManager>();

            recurringJobManager.AddOrUpdate("Hatırlatma Epostalarını Gönder", Job.FromExpression(() => new RecurringJobs(null).SendReminderEmailsJob()), "21 9 * * *");
        }
    }
}
