using System;
using System.Threading.Tasks; 
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using VerificationProvider.Services;

namespace VerificationProvider.Functions
{
    public class VerificationCleaner
    {
        private readonly ILogger<VerificationCleaner> _logger; 
        private readonly IVerificationCleanerService _verificationCleanerService;

        
        public VerificationCleaner(ILogger<VerificationCleaner> logger, IVerificationCleanerService verificationCleanerService)
        {
            _logger = logger;
            _verificationCleanerService = verificationCleanerService;
        }

        [Function("VerificationCleaner")]
        public async Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer)
        {
            try
            {
                await _verificationCleanerService.RemoveExpiredRecordsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: VerificationCleaner.Run :: {ex.Message}");
            }
        }
    }
}
