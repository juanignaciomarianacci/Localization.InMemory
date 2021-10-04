using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Localizatio.InMemory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocalizationController : Controller
    {
        private readonly LocalizationContext buildingContext;

        public LocalizationController(LocalizationContext buildingContext)
        {
            this.buildingContext = buildingContext;
        }

        [HttpGet]
        [Route("")]
        [EnableCors("CorsApi")]
        public ActionResult Get()
        {
            return Ok(buildingContext.Localizations.ToList());
        }

        [HttpGet]
        [Route("{id}")]
        [EnableCors("CorsApi")]
        public ActionResult Get(int id)
        {
            return Ok(buildingContext.Localizations.Find(id));
        }

        [HttpGet("localizations/{localization}")]
        [EnableCors("CorsApi")]
        public ActionResult GetByLoc([FromRoute] string localization)
        {
            if (!string.IsNullOrEmpty(localization))
                return Ok(buildingContext.Localizations.Where(loc => loc.Language.Equals(localization)).ToList());
            else
                return NotFound();
        }

        [HttpPut]
        [Route("")]
        [EnableCors("CorsApi")]
        public ActionResult Put(int id, LocalizationRequest localizationRequest)
        {
            var localization = buildingContext.Localizations.Find(id);
            localization.Key = localizationRequest.Key;
            localization.Language = localizationRequest.Language;
            localization.Value = localizationRequest.Value;

            buildingContext.Localizations.Update(localization);
            buildingContext.SaveChanges();

            return Ok(localization);
        }

        [HttpPost]
        [Route("")]
        [EnableCors("CorsApi")]
        public ActionResult Post(LocalizationRequest localizationRequest)
        {
            var localization = new Localizations
            {
                Key = localizationRequest.Key,
                Language = localizationRequest.Language,
                Value = localizationRequest.Value
            };

            buildingContext.Localizations.Add(localization);
            buildingContext.SaveChanges();

            return Ok(localization);
        }

        [HttpDelete("{id}")]
        [EnableCors("CorsApi")]
        public ActionResult Delete(int id)
        {
            var localization = buildingContext.Localizations.Find(id);

            if (localization != null)
            {
                buildingContext.Localizations.Remove(localization);
                buildingContext.SaveChanges();
            }
            else
                return NotFound();

            return Ok();
        }
    }
}
