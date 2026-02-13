using System;
using System.Collections.Generic;

namespace GitHub.Runner.Worker.Handlers
{
    /// <summary>
    /// Helper to override ACTIONS_CACHE_URL and ACTIONS_RESULTS_URL environment variables
    /// with values from the actual process environment (e.g. set by the cache proxy),
    /// while preserving the original values for downstream use.
    /// </summary>
    public static class CacheEnvironmentHelper
    {
        /// <summary>
        /// Saves the current values of ACTIONS_CACHE_URL and ACTIONS_RESULTS_URL into
        /// ACTIONS_CACHE_URL_ORIGINAL and ACTIONS_RESULTS_URL_ORIGINAL, then overrides
        /// them with values from the actual process environment variables.
        /// Also sets ACTIONS_CACHE_SERVICE_V2 to "true".
        /// </summary>
        public static void OverrideCacheEnvironment(Dictionary<string, string> environment)
        {
  // Save original values so the cache proxy (or other consumers) can reach the real backend
  if (environment.TryGetValue("ACTIONS_CACHE_URL", out var originalCacheUrl))
  {
      environment["ACTIONS_CACHE_URL_ORIGINAL"] = originalCacheUrl;
  }

  if (environment.TryGetValue("ACTIONS_RESULTS_URL", out var originalResultsUrl))
  {
      environment["ACTIONS_RESULTS_URL_ORIGINAL"] = originalResultsUrl;
  }

  // Override with values from the process environment (set by the cache proxy)
  var envCacheUrl = System.Environment.GetEnvironmentVariable("ACTIONS_CACHE_URL");
  if (!string.IsNullOrEmpty(envCacheUrl))
  {
      environment["ACTIONS_CACHE_URL"] = envCacheUrl;
  }

  var envResultsUrl = System.Environment.GetEnvironmentVariable("ACTIONS_RESULTS_URL");
  if (!string.IsNullOrEmpty(envResultsUrl))
  {
      environment["ACTIONS_RESULTS_URL"] = envResultsUrl;
  }

  // Enable cache service v2
  environment["ACTIONS_CACHE_SERVICE_V2"] = "true";
        }
    }
}
