using Microsoft.AspNetCore.Authorization;
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
        public ActionResult Get()
        {
            return Ok(buildingContext.Localizations.ToList());
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(buildingContext.Localizations.Find(id));
        }

        [HttpGet("localizations/{localization}")]
        public ActionResult GetByLoc([FromRoute] string localization)
        {
            if (!string.IsNullOrEmpty(localization))
                return Ok(buildingContext.Localizations.Where(loc => loc.Language.Equals(localization)).ToList());
            else
                return NotFound();
        }

        [HttpPut]
        [Route("")]
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
    }
}
