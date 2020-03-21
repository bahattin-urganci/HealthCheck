using Hangfire;
using HealthCheck.Infrastructure.Attributes;
using HealthCheck.Infrastructure.MailTemplates;
using HealthCheck.Messaging;
using HealthCheck.Model;
using HealthCheck.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace HealthCheck.Service
{
    [Scoped]
    public class AppService : BaseService, IAppService
    {
        private readonly IUnitOfWork _db;
        private readonly IMessagingService _messagingService;
        public AppService(IMessagingService messagingService, IHttpContextAccessor httpContextAccessor, IUnitOfWork db) : base(httpContextAccessor)
        {
            _db = db;
            _messagingService = messagingService;
        }

        public AppDTO App(int id)
        {
            return Map<AppDTO>(_db.AppRepository.GetApp(id));
        }

        public async Task<List<AppDTO>> Apps()
        {
            return Map<List<AppDTO>>(await _db.AppRepository.Apps(UserId));
        }

        public Task<int> RemoveApp(int id)
        {
            _db.AppRepository.Remove(_db.AppRepository.FindOne(x => x.Id == id));
            return _db.SaveChangesAsync();
        }

        public async Task<AppDTO> SaveApp(AppDTO app)
        {
            var entity = _db.AppRepository.FindOne(x => x.Id == app.Id);
            if (!Uri.IsWellFormedUriString(app.URL, UriKind.RelativeOrAbsolute))
            {
                throw new InvalidOperationException("URL is not well formatted");
            }
            if (entity != null)
            {
                _db.AppRepository.Update(app, entity);
            }
            else
            {
                app.UserId = UserId;
                entity = Map<Domain.App>(app);
                _db.AppRepository.Add(entity);
            }

            await _db.SaveChangesAsync();
            RecurringJob.AddOrUpdate(entity.Id.ToString(), () => CheckApp(entity.Id), entity.Interval);


            return app;
        }

        public async Task CheckApp(int jobId)
        {
            var app = _db.AppRepository.GetApp(jobId);
            if (app != null && !string.IsNullOrEmpty(app.URL))
            {
                var entity = new Domain.AppCheckHistory
                {
                    AppId = jobId,
                    CheckTime = DateTime.Now,

                };
                try
                {
                    HttpClient client = new HttpClient();

                    var checkingResponse = await client.GetAsync(app.URL);
                    if (!checkingResponse.IsSuccessStatusCode)
                    {
                        //notification
                        _messagingService.TemplateName = MailTemplates.DownNotification;
                        _messagingService.Props = new { AppName = app.Name, URL = app.URL, CheckTime = DateTime.Now };
                        await _messagingService.Send(app.User.Email, "Application Check Is Not LIVE");
                    }
                    entity.Live = checkingResponse.IsSuccessStatusCode;
                }
                catch (Exception ex)
                {
                    entity.Live = false;
                }
                finally
                {
                    _db.AppCheckHistoryRepository.Add(entity);
                    await _db.SaveChangesAsync();
                }
            }
        }
    }


}
