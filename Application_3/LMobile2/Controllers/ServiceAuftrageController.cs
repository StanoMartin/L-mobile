using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using LMobile2.Models;

namespace LMobile2.Controllers
{
    public class ServiceAuftrageController : ODataController
    {
        private LMobileContext db = new LMobileContext();

        // GET: odata/ServiceAuftrage
        [EnableQuery]
        public IQueryable<ServiceAuftrag> GetServiceAuftrage()
        {
            return db.ServiceAuftrage;
        }

        // GET: odata/ServiceAuftrage(5)
        [EnableQuery]
        public SingleResult<ServiceAuftrag> GetServiceAuftrag([FromODataUri] string key)
        {
            var auftrag = db.ServiceAuftrage
                .Include(a => a.Kunde)
                .Include(a => a.Machine)
                .Include(a => a.ArbeitsZeitMeldungen)
                .Where(a => a.AuftragsNummer == key);
            return SingleResult.Create(auftrag);
        }

        // PUT: odata/ServiceAuftrage(5)
        public async Task<IHttpActionResult> Put([FromODataUri] string key, Delta<ServiceAuftrag> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ServiceAuftrag serviceAuftrag = await db.ServiceAuftrage.FindAsync(key);
            if (serviceAuftrag == null)
            {
                return NotFound();
            }

            patch.Put(serviceAuftrag);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceAuftragExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(serviceAuftrag);
        }

        // POST: odata/ServiceAuftrage
        public async Task<IHttpActionResult> Post(ServiceAuftrag serviceAuftrag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try
            {
                // check if serviceauftrag already exists
                ServiceAuftrag serviceAuftragdb = await db.ServiceAuftrage.FindAsync(serviceAuftrag.AuftragsNummer);
                if (serviceAuftragdb != null)
                {
                    serviceAuftragdb.ApplyChanges(serviceAuftrag);

                    Kunde kundedb = await db.Kundes.FindAsync(serviceAuftrag.Kunde.KundenNummer);
                    kundedb.ApplyChanges(serviceAuftrag.Kunde);

                    Machine machinedb = await db.Machines.FindAsync(serviceAuftrag.Machine.MachinenNummer);
                    machinedb.ApplyChanges(serviceAuftrag.Machine);

                    ApplyChanges(serviceAuftrag.AuftragsNummer, serviceAuftrag.ArbeitsZeitMeldungen);
                }
                else
                {
                    db.ServiceAuftrage.Add(serviceAuftrag);
                }

                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ServiceAuftragExists(serviceAuftrag.AuftragsNummer))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(serviceAuftrag);
        }

        private void ApplyChanges(string auftragNummer, ICollection<ArbeitsZeitMeldung> arbeitsZeitMeldungen)
        {
            try
            {
                // find and remove items
                var azmListdb = db.ArbeitsZeitMeldungs.Where(m => m.ServiceAuftragNummer == auftragNummer).ToList();
                var azmListRemove = azmListdb.Except(arbeitsZeitMeldungen, new ArbeitsZeitMeldungComparer());
                foreach (ArbeitsZeitMeldung azm in azmListRemove)
                {
                    db.ArbeitsZeitMeldungs.Remove(azm);
                }

                // add or update the rest of items
                foreach (var azm in arbeitsZeitMeldungen)
                {
                    ArbeitsZeitMeldung arbeitsZeitMeldungdb = azmListdb.FirstOrDefault(m => m.ArtikelNummer == azm.ArtikelNummer);
                    if (arbeitsZeitMeldungdb != null)
                    {
                        arbeitsZeitMeldungdb.ApplyChanges(azm);
                    }
                    else
                    {
                        db.ArbeitsZeitMeldungs.Add(azm);
                    }
                }
            }
            catch(Exception exc)
            {
                throw exc;
            }
        }

        // PATCH: odata/ServiceAuftrage(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] string key, Delta<ServiceAuftrag> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ServiceAuftrag serviceAuftrag = await db.ServiceAuftrage.FindAsync(key);
            if (serviceAuftrag == null)
            {
                return NotFound();
            }

            patch.Patch(serviceAuftrag);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceAuftragExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(serviceAuftrag);
        }

        // DELETE: odata/ServiceAuftrage(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] string key)
        {
            ServiceAuftrag serviceAuftrag = await db.ServiceAuftrage.FindAsync(key);
            if (serviceAuftrag == null)
            {
                return NotFound();
            }

            db.ServiceAuftrage.Remove(serviceAuftrag);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ServiceAuftrage(5)/ArbeitsZeitMeldungen
        [EnableQuery]
        public IQueryable<ArbeitsZeitMeldung> GetArbeitsZeitMeldungen([FromODataUri] string key)
        {
            return db.ServiceAuftrage.Where(m => m.AuftragsNummer == key).SelectMany(m => m.ArbeitsZeitMeldungen);
        }

        // GET: odata/ServiceAuftrage(5)/Kunde
        [EnableQuery]
        public SingleResult<Kunde> GetKunde([FromODataUri] string key)
        {
            return SingleResult.Create(db.ServiceAuftrage.Where(m => m.AuftragsNummer == key).Select(m => m.Kunde));
        }

        // GET: odata/ServiceAuftrage(5)/Machine
        [EnableQuery]
        public SingleResult<Machine> GetMachine([FromODataUri] string key)
        {
            return SingleResult.Create(db.ServiceAuftrage.Where(m => m.AuftragsNummer == key).Select(m => m.Machine));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ServiceAuftragExists(string key)
        {
            return db.ServiceAuftrage.Count(e => e.AuftragsNummer == key) > 0;
        }
    }
}
