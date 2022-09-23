
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.36.0.  DO NOT EDIT!
//*************************************************************************************

using System.Net;
using Grpc.Core;
using System.Threading.Tasks;
using XTC.FMP.MOD.Assloud.LIB.Proto;

namespace XTC.FMP.MOD.Assloud.App.Service
{
    /// <summary>
    /// Healthy基类
    /// </summary>
    public class HealthyServiceBase : LIB.Proto.Healthy.HealthyBase
    {
    

        public override async Task<HealthyEchoResponse> Echo(HealthyEchoRequest _request, ServerCallContext _context)
        {
            try
            {
                return await safeEcho(_request, _context);
            }
            catch (ArgumentRequiredException ex)
            {
                return await Task.Run(() => new HealthyEchoResponse
                {
                    Status = new LIB.Proto.Status() { Code = -HttpStatusCode.BadRequest.GetHashCode(), Message = ex.Message },
                });
            }
            catch (Exception ex)
            {
                return await Task.Run(() => new HealthyEchoResponse
                {
                    Status = new LIB.Proto.Status() { Code = -HttpStatusCode.InternalServerError.GetHashCode(), Message = ex.Message },
                });
            }
        }



        protected virtual async Task<HealthyEchoResponse> safeEcho(HealthyEchoRequest _request, ServerCallContext _context)
        {
            return await Task.Run(() => new HealthyEchoResponse {
                    Status = new LIB.Proto.Status() { Code = -1, Message = "Not Implemented" },
            });
        }

    }
}

