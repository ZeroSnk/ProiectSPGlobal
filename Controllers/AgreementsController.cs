using Microsoft.AspNetCore.Mvc;
using SandP.Models;
using SandP.Models.Repositories;

namespace SandP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgreementsController : ControllerBase
    {
        private readonly ILogger<AgreementsController> _logger;

        private readonly IAgreementRepository _agreementyRepo;
        public AgreementsController(IAgreementRepository agreementRepo, ILogger<AgreementsController> logger)
        {
            _agreementyRepo = agreementRepo;
            _logger = logger;
        }

        [HttpGet] 
        public async Task<IEnumerable<Agreement>> GetEntities()
        {
            _logger.LogError("GetEntitiesErrors:...");
            return await _agreementyRepo.GetAll();
        }
        [HttpGet("{CNP}")]
        public async Task<ActionResult<Agreement>> GetEntities(string CNP)
        {
            
            try
            {   
                var entityToFind = await _agreementyRepo.Get(CNP);

                if (entityToFind == null)
                {
                    _logger.LogError($"GetEntitieErrors:Agreement with {CNP} not found!");
                    return NotFound("Agreement not found!");
                }
                return Ok(entityToFind);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unexpected error");
                return BadRequest();

            }
            

        }
        [HttpPost]
        public async Task<ActionResult<Agreement>> PostEntity([FromBody] Agreement agreement)
        {
            _logger.LogError("da");
            try
            {
                var agreementExist = await _agreementyRepo.Get(agreement.CNP);
                if (agreementExist == null)
                {
                    var newAgreement = await _agreementyRepo.Create(agreement);

                    return Ok(newAgreement);
                }
                else
                {
                    _logger.LogError($"PostErrors:-Agreement with {agreement.CNP} already exists!");
                    return BadRequest("Agreement already exists!");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unexpected error");
                return BadRequest();
            }
        }
        
        [HttpPut]
        public async Task<ActionResult<Agreement>> UpdateEntity(string CNP, [FromBody] Agreement agreement)
        {
          
            if (CNP != agreement.CNP)
            {
                _logger.LogError($"PutErrors:-Agreement with {CNP} not found!");
                return BadRequest("Agreement not found!");
            }

            await _agreementyRepo.Update(agreement);
           
            return NoContent();
        }
        [HttpDelete]
        public async Task<ActionResult<Agreement>> Delete(string CNP)
        {
            
            try
            {
                var agreementToDelete = await _agreementyRepo.Get(CNP);

                if (agreementToDelete == null)
                {
                    _logger.LogError($"DeleteErrors:-Agreement with {CNP} not found");
                    return NotFound("Agreement not found!");
                }

                await _agreementyRepo.Delete(agreementToDelete.CNP);
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unexpected error");

                return BadRequest();
            }
            
        }
    }
}
