using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ChirpyR.Domain.Model;
using SignalR;
using ChirpyR.Mvc4.SignalR;
using System.Web.Security;
using ChirpyR.Domain.Repository;

namespace ChirpyR.Mvc4.Controllers
{
    using System.Security.Cryptography;
    using System.Text;

    public class ChirpyRController : ApiController
    {
        //IChirpyRRepository _repository;
        public ChirpyRController()
        {
        }

        // GET api/chirpyr
        public HttpResponseMessage Get()
        {
            //IList<Chirp> chirps =_repository.GetLatestChirpsFor(CurrentUserName());
            return Request.CreateResponse<IList<Chirp>>(HttpStatusCode.OK, null);
        }

        private string CurrentUserName()
        {
            return this.User != null && this.User.Identity != null
                                            ? this.User.Identity.Name
                                            : "(none)";
        }

        // GET api/chirpyr/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/chirpyr
        public void Post(Chirp model)
        {
            if (ModelState.IsValid)
            {
                //_repository.AddChirp(model);
                var hubContext =
                    GlobalHost
                        .ConnectionManager
                            .GetHubContext<ChirpyRHub>();

                var member = Membership.GetUser();

                model.ChirpBy = new ChirpyRUser
                {
                    UserId = this.User != null && this.User.Identity != null
                                ? this.User.Identity.Name
                                : "(none)",
                    Gravataar = string.Format("http://www.gravatar.com/avatar/{0}?d=identicon", this.CreateMD5Hash(member.Email))
                };
                hubContext.Clients.NewChirp(model);
            }
        }

        private string CreateMD5Hash(string input)
        {
            // Use input string to calculate MD5 hash
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
                // To force the hex string to lower-case letters instead of
                // upper-case, use he following line instead:
                // sb.Append(hashBytes[i].ToString("x2")); 
            }
            return sb.ToString().ToLower();
        }

        // PUT api/chirpyr/5
        public void Put(int id, string value)
        {
        }

        // DELETE api/chirpyr/5
        public void Delete(int id)
        {
        }
    }
}
