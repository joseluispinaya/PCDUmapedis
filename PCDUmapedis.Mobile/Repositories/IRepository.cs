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
        Task<HttpResponseWrapper<T>> Get<T>(string urlBase, string url);
        Task<HttpResponseWrapper<T>> GetPagosN<T>(string urlBase, string url, ConsultaDTO modelo);
    }
}
