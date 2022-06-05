using SendGrid;
using SendGrid.Helpers.Mail;
using ToDoCore;
using ToDoInfrastructure.Interfaces;
using Serilog;
using ILogger = Serilog.ILogger;

namespace ToDoAPI
{
    public class ReminderHostedService : IHostedService
    {

        private Timer _timer;
        private readonly ILogger _logger;
        private readonly IServiceProvider _service;
        private IConfiguration config;
        public ReminderHostedService(IServiceProvider serviceprov,ILogger loger) 
        {
            _logger= loger; 
            _service= serviceprov;
            //inject
           var configuration = new ConfigurationBuilder()
                                         .SetBasePath(Directory.GetCurrentDirectory())
                                         .AddJsonFile($"appsettings.json");
            config = configuration.Build();
            _logger.Information("Background service started");
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            int interval = Int32.Parse(config["Interval"]);
            _timer = new Timer(Remind,null,0,interval);
           return Task.CompletedTask;
        }

        public void Remind(object state) 
        {
            using (var scope=_service.CreateScope()) 
            {
                var toDoListOps = scope.ServiceProvider.GetRequiredService<IToDoListOperations>();
                List<ToDoList> toDoList = toDoListOps.Get(null);
                // List<ToDoList> toRemind = toDoList.Where(p=>(p.Remind)&&(p.RemindAfter<DateTime.Now)).ToList();
                List<ToDoList> toRemind = toDoList.Where(p => (p.Remind)).ToList();
                 _logger.Information(String.Format("Reminder service found {0}",toRemind.Count().ToString()));

                
                foreach (ToDoList toDo in toRemind) 
                {
                    SendEmail(toDo);
                   // toDoListOps.UpdateRemindCriteria(toDo.Id,false);
                   // toDoListOps.UpdateOpened(toDo, false);
                } 
               
            }
        }

        private async void SendEmail(ToDoList toDoList) 
        {
            var apiKey = config["SendGridKey"];
            var sender = config["SenderMail"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(sender, "Slavke");
            var subject = "Reminder";
            //todolist.email
            var to = new EmailAddress("milakovic.slavisa01@gmail.com", "Slavke2");
            //local:port/api
            var plainTextContent = "http://localhost:4200/to-do-list/"+toDoList.Id;
            var htmlContent = "";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
             _timer?.Change(Timeout.Infinite,0);
             return Task.CompletedTask;
        }

    }
}
