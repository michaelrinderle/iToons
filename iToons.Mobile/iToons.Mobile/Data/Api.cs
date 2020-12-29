using Flurl;
using Flurl.Http;
using iToons.Library.Entity;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace iToons.Mobile.Data
{
    public class Api
    {
        public async Task<MetaData> GetMetaData(int id)
        {
            try
            {
                var uri = Constants.GetBaseMetaUrl();
                var connection = await uri
                    .SetQueryParams(new { id  = id })
                    .WithTimeout(45)
                    .GetAsync();

                return JsonConvert.DeserializeObject<MetaData>
                    (await connection.Content.ReadAsStringAsync());
            }
            catch (FlurlHttpTimeoutException ex)
            {
                return null;
            }
            catch (FlurlHttpException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
               return null;
            }
        }
    }
}
