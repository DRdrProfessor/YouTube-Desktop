﻿using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using YouTubeScrap.Core.ReverseEngineer;
using YouTubeScrap.Handlers;

namespace YouTubeScrap.Core.Youtube
{
    //TODO: After making the user, call the API to get the user details. For now we can use the user to get logged in responses from the YouTube API.
    //TODO: Write the user/cookie functionality out in Obsidian or something to make the implementation more clear.
    //TODO: Make a system to save user to disk in binary or hashed JSON/binary, and maybe add a password/hash protection to the file.
    //TODO: Implement a per user NetworkHandler class to keep the cookies apart from multiple users. (The new cookie implementation will work with this!)
    public class YoutubeUser
    {
        public CookieContainer UserCookieContainer => _userCookieContainer;
        public UserData UserData;
        public UserSettings UserSettings;
        public bool HasLogCookies = false;
        public string Test = "Official string!";

        private NetworkHandler _network;
        private CookieContainer _userCookieContainer;
        private readonly Cookie userSAPISID;
        private readonly string[] requiredCookies = new string[]
        {
            "SAPISID",
            "__Secure-3PSIDCC",
            "SIDCC"
        };
        /// <summary>
        /// Setup a user, for browsing YouTube. If no cookies are given and/or the config has no default to load account, then we will setup a default user that is NOT logged in, and will be temporary cached to disk.
        /// The default user(Not logged in) will be used if there is no user cookies or login is given and for anonymous browsing. Cookies will be temporary, and or will be deleted when the system restarts.
        /// </summary>
        /// <param name="cookieJar">CookieContainer with the required cookies to receive user data, and to perform user actions.</param>
        public YoutubeUser(CookieContainer cookieJar = null)
        {
            //TODO: Check the main config for default user to load on startup!
            if (cookieJar != null)
            {
                _userCookieContainer = cookieJar;
                if (TryGetCookie("SAPISID", out Cookie cookieOut))
                {
                    userSAPISID = cookieOut;
                    HasLogCookies = true;
                }
                else
                    Trace.WriteLine("Could not acquire the SAPISID/__Secure-3PAPISID! User is unable to login!");
            }
            _network = new NetworkHandler(this);
        }
        public void SaveUser()
        {
            //string pathToSave = Path.Combine(SettingsManager.Settings.UserStoragePath, UserData.UserID);
            //MemoryStream memStream = new MemoryStream();
            //BinaryFormatter binFormat = new BinaryFormatter();
            //binFormat.Serialize(memStream, UserCookies);
            //File.WriteAllBytes(Path.Combine(pathToSave, $"user_{UserData.UserID}.ytudata"), memStream.ToArray());
            //memStream.Dispose();
        }
        public static YoutubeUser LoadUser()
        {
            return null;
        }
        public void SaveCookies(CookieContainer cookieJar)
        {
            using (Stream writeStream = File.Create(Path.Combine(Directory.GetCurrentDirectory(), "user_cookies.yt_cookies")))
            {
                try
                {
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    bFormatter.Serialize(writeStream, cookieJar);
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"Exception while saving cookies to disk!: {e}");
                }
            }
        }
        public static CookieContainer ReadCookies()
        {
            using (Stream readStream = File.Open(Path.Combine(Directory.GetCurrentDirectory(), "user_cookies.yt_cookies"), FileMode.Open))
            {
                try
                {
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    return (CookieContainer)bFormatter.Deserialize(readStream);
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"Exception while reading cookies from disk!: {e}");
                }
            }
            return null;
        }
        // Needed to implement this function because the default CookieContainer class does not have this simple function, to get all the cookies without passing the domain URI! Why?!
        public CookieCollection GetAllCookies()
        {
            Hashtable domainTable = (Hashtable)_userCookieContainer.GetType().GetField("m_domainTable", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(_userCookieContainer);
            if (domainTable == null)
                return null;
            CookieCollection cookieCollection = new CookieCollection();
            foreach (DictionaryEntry domain in domainTable)
            {
                SortedList cookieList = (SortedList)domain.Value.GetType().GetField("m_list", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(domain.Value);
                if (cookieList == null)
                    continue;
                foreach (DictionaryEntry cookieColl in cookieList)
                {
                    cookieCollection.Add((CookieCollection)cookieColl.Value);
                }
            }
            return cookieCollection;
        }

        public bool TryGetCookie(string name, out Cookie cookieOut)
        {
            cookieOut = null;
            if (string.IsNullOrEmpty(name)) return false;
            CookieCollection cookieCol = GetAllCookies();
            cookieOut = cookieCol[name];
            return cookieOut != null;
        }
        public void GetUserData()
        {
            //TODO: Need to call the network handler for a request, deserialize the response and then populate the user class with the users data.
        }
        public AuthenticationHeaderValue GenerateAuthentication()
        {
            return UserAuthentication.GetSapisidHashHeader(userSAPISID.Value);
        }
    }
    public struct UserData
    {
        public string UserID { get; set; } // User/Channel ID.
        public string UserName { get; set; }
        public string AvatarUrl { get; set; }
    }
    public struct UserSettings
    {
        // Will be populated when there a settings implemented!
    }
}