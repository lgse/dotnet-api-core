using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Core.Http.Response;

namespace API.Core.Http.Controllers
{
    public abstract class Controller
    {
        protected static ActionResult<ObjectCollectionResponse<TObjectType>> ObjectCollection<TObjectType>(List<TObjectType> collection)
        {
            var res = new ObjectCollectionResponse<TObjectType>(collection);
            return new JsonObjectResult(res);
        }

        protected static ActionResult<ObjectCreatedResponse<TObjectType>> ObjectCreated<TObjectType>(TObjectType obj)
        {
            var res = new ObjectCreatedResponse<TObjectType>(obj);

            return new JsonObjectResult(res) {
                StatusCode = StatusCodes.Status201Created,
            };
        }

        protected static ActionResult<ObjectResponse<TObjectType>> Object<TObjectType>(TObjectType obj)
        {
            var res = new ObjectResponse<TObjectType>(obj);
            return new JsonObjectResult(res);
        }

        protected static ActionResult<ObjectCreatedEmptyResponse> ObjectCreated()
        {
            var res = new ObjectCreatedEmptyResponse();
            return new JsonObjectResult(res);
        }

        protected static ActionResult<OkResponse> Success()
        {
            var res = new OkResponse();
            return new JsonObjectResult(res);
        }
    }
}