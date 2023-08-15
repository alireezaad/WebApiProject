using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WebApi.Model;
using WebApi.Model.DBContext;
using WebApi.Model.ViewModel;

namespace WebApi.Services
{
    public class TwittRepository : IDisposable, ITwittRepsitory
    {
        private readonly MyDBContext _service;

        public TwittRepository(MyDBContext db)
        {
            this._service = db;
        }

        public IEnumerable<Twitt> GetAll()
        {
            try
            {
                return _service.twitts.ToList();
            }
            catch (Exception) { throw; }
        }
        public Twitt Get(int id)
        {
            try
            {
                return _service.twitts.FirstOrDefault(t => t.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Add(TwittViewModel twittModel)
        {
            try
            {
                var twitt = new Twitt() { Body = twittModel.Body };
                _service.twitts.Add(twitt);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(TwittViewModel twittModel)
        {
            try
            {
                var twitt = new Twitt() { Id = twittModel.Id, Body = twittModel.Body };
                _service.twitts.Update(twitt);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void Delete(int id)
        {
            try
            {
                var twitt = Get(id);
                _service.twitts.Remove(twitt);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(TwittViewModel twittModel)
        {
            try
            {
                var twitt = new Twitt() { Id = twittModel.Id, Body = twittModel.Body };
                _service.twitts.Remove(twitt);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void SaveChanges()
        {
            _service.SaveChanges();
        }

        public void Dispose()
        {
            if (_service != null)
            {
                GC.SuppressFinalize(_service);
            }
        }
    }
}
