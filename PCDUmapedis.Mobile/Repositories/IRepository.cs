using PCDUmapedis.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCDUmapedis.Mobile.Repositories
{
    public interface IRepository
    {
        Task<HttpResponseWrapper<T>> GetPerso<T>(string urlBase, string url, T model);
        Task<HttpResponseWrapper<T>> GetPersoN<T>(string urlBase, string url, LoginDTO modeld);
    }
}
