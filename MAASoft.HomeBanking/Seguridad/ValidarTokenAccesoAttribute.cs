using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MAASoft.HomeBanking.Seguridad
{
    public class ValidarTokenAccesoAttribute : ActionFilterAttribute
    {
        private const string HEADER_TOKEN_ACCESO = "TokenAcceso";

        private Dictionary<string, string> _tokens;

        public ValidarTokenAccesoAttribute()
        {
            _tokens = ConfigurationManager.AppSettings["tokensAcceso"]
                .Split(',')
                .ToDictionary(el => el);
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            IEnumerable<string> tokenAccesoValores = null;
            if (!actionContext.Request.Headers.TryGetValues(HEADER_TOKEN_ACCESO, out tokenAccesoValores)
                || !tokenAccesoValores.Any()
                || !EsTokenValido(tokenAccesoValores.FirstOrDefault()))
            {
                var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Por favor, especifique el token de acceso correcto." };
                throw new HttpResponseException(msg);
            }
        }

        private bool EsTokenValido(string token)
        {
            return !(
                String.IsNullOrWhiteSpace(token)
                || !_tokens.ContainsKey(token)
            );
        }
    }
}