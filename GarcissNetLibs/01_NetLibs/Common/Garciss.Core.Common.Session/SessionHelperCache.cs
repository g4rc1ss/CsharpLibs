using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Garciss.Core.Common.Session {
    /// <summary>
    /// Libreria para implementar los objetos en cache, se usa el objeto ISession
    /// Por tanto hay que configurar el Startup de la siguiente manera:
    /// En la funcion de ConfigureServices
    /// services.AddSession(opts => {
    ///     opts.Cookie.IsEssential = true;
    ///     opts.Cookie.HttpOnly = true;
    ///     opts.Cookie.Name = "EguzkimendiSession";
    /// });
    /// services.AddMemoryCache();
    /// 
    /// En la funcion de Configure
    /// app.UseSession();
    /// </summary>
    public static class SessionHelperCache {
        /// <summary>
        /// Guardamos en json un objeto ligado a una `key` para almacenar en cache dicho objeto
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetObjectAsJson(this ISession session, string key, object value) {
            var str = JsonConvert.SerializeObject(value);
            session.SetString(key, str);
        }

        /// <summary>
        /// Obtenemos el json de la cache y lo convertimos al objeto correspondiente
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetObjectFromJson<T>(this ISession session, string key) {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// Eliminamos un objeto almacenado en sesion
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool RemoveObjectsOnCache(this ISession session, string key) {
            if (session != null) {
                session.Remove(key);
                return true;
            }
            return false;
        }
    }
}
