using StudentAchievement.Data.Domain.Interfaces;
using StudentAchievement.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace StudentAchievement.Service.Services
{
    public abstract class BaseService<T> : IBaseService<T> where T : class
    {
        protected readonly IUnitOfWork _uow;

        public object EmailSettings { get; private set; }

        protected BaseService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public T GetById(Guid id)
        {
            return _uow.GetRepository<T>().FindById(id);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _uow.GetRepository<T>().FindByIdAsync(id);
        }

        //public void SendMail(string recipients, string subject, string body)
        //{
        //    var emailSettings = EmailSettings;


        //    var fromAddress = new MailAddress(emailSettings.Username, "VT safe");
        //    var toAddress = new MailAddress(emailSettings.Username, "Vasy");

        //    var smtp = new SmtpClient
        //    {
        //        Host = emailSettings.Host,
        //        Port = emailSettings.Port,
        //        EnableSsl = true,
        //        DeliveryMethod = SmtpDeliveryMethod.Network,
        //        UseDefaultCredentials = false,
        //        Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password)
        //    };
        //    using (var message = new MailMessage(fromAddress, toAddress)
        //    {
        //        Subject = subject,
        //        Body = body
        //    })
        //    {
        //        smtp.Send(message);
        //    }



        //}

        public virtual void Remove(Guid id)
        {
            _uow.GetRepository<T>().Remove(id);
            _uow.Commit();
        }

        public virtual void Remove(T entity)
        {
            _uow.GetRepository<T>().Remove(entity);
            _uow.Commit();
        }

        public T Add(T entity)
        {
            _uow.GetRepository<T>().Add(entity);
            _uow.Commit();
            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            _uow.GetRepository<T>().Add(entity);
            await _uow.CommitAsync();
            return entity;
        }

        public T Update(T entity)
        {
            _uow.GetRepository<T>().Update(entity);
            _uow.Commit();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _uow.GetRepository<T>().Update(entity);
            await _uow.CommitAsync();
            return entity;
        }
    }
}
