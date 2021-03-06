﻿using System;
using System.Collections.Generic;
using System.Globalization;

namespace ScmBackup.Resources
{
    /// <summary>
    /// Provides translated string resources
    /// </summary>
    public class ResourceProvider : IResourceProvider
    {
        private bool initialized;
        private CultureInfo culture;

        /// <summary>
        /// temporary list of resources until we have a better solution
        /// </summary>
        private Dictionary<string, string> resources = new Dictionary<string, string>
        {
            { "ApiGettingUrl", "{0}: Getting {1}" },
            { "ApiHeaders", "{0} headers: {1}" },
            { "ApiResult", "{0} result: {1}" },
            { "AppTitle", "SCM Backup" },
            { "BackupFailed", "Backup failed!" },
            { "ConfigSourceIsNull", "ConfigSource must not be NULL" },
            { "EndSeconds", "The application will close in {0} seconds!" },
            { "ReadingConfig", "{0}: Reading config" },
            { "StartingBackup", "{0}: Starting backup" },
            { "GithubAuthNameEmpty", "AuthName is empty (GitHub)" },
            { "GithubNameEmpty", "name is empty (GitHub)" },
            { "GithubPasswordEmpty", "Password is empty (GitHub)" },
            { "GithubWrongHoster", "wrong hoster (GitHub): {0}" },
            { "GithubWrongType", "wrong type (GitHub): {0}" },
            { "HosterDoesntExist", "Hoster {0} doesn't exist" },
            { "LocalFolderMissing", "Local folder is missing!" },
            { "NoSourceConfigured", "No source configured!" },
            { "Return", "{0}: Return" },
            { "TypeIsNoIHoster", "Can't register {0} in the HosterFactory because it doesn't implement IHoster" },
        };

        public void Initialize(CultureInfo culture)
        {
            if (culture == null)
            {
                throw new InvalidOperationException("Invalid culture!");
            }

            this.culture = culture;
            this.initialized = true;
        }

        public string GetString(string key)
        {
            if (!this.initialized)
            {
                throw new InvalidOperationException("ResourceProvider not initialized!");
            }

            string result;
            this.resources.TryGetValue(key, out result);
            return result;
        }
    }
}
