﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AppNiZiAPI.Variables
{
    public static class Messages
    {
        // Errors
        public const string ErrorServer = "Server Time Out";
        public const string ErrorPostBody = "Incorrect values";
        public const string ErrorMissingValues = "Missing values";

        // Auth
        public const string AuthNoAcces = "Authorization Error: No acces";
        public const string AuthLogIn = "Authification Error: Log in to use the API";

        // OK Messages
        public const string OKResult = "Result: OK";
        public const string OKPost = "Inserting Data Successfully";
        public const string OKUpdate = "Updating Data Successfully";
        public const string OKDelete = "Removing Data Successfully";
    }
}