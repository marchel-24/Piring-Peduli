using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using PiringPeduliClass.Model;

namespace PiringPeduliWPF.Services
{
    public class GeoService
    {
        /// <summary>
        /// Calculates the distance in meters between two geographical points using the Haversine formula.
        /// </summary>
        public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371000; // Radius of the Earth in meters (mean radius)

            // Convert degrees to radians
            double lat1Rad = lat1 * Math.PI / 180.0;
            double lat2Rad = lat2 * Math.PI / 180.0;
            double deltaLat = (lat2 - lat1) * Math.PI / 180.0;
            double deltaLon = (lon2 - lon1) * Math.PI / 180.0;

            // Apply Haversine formula
            double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                       Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                       Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distance = R * c; // Distance in meters

            // Debug output
            Debug.WriteLine($"Distance between ({lat1}, {lon1}) and ({lat2}, {lon2}) = {distance} meters");

            return distance;
        }


        /// <summary>
        /// Finds the account closest to the specified latitude and longitude.
        /// </summary>
        /// <param name="lat">Latitude of the reference point.</param>
        /// <param name="lon">Longitude of the reference point.</param>
        /// <param name="accounts">List of accounts to search.</param>
        /// <returns>The nearest account to the specified point.</returns>
        public static Account FindNearestAccount(double lat, double lon, List<Account> accounts)
        {
            if (accounts == null || accounts.Count == 0)
            {
                return null; // Return null if the list is empty or null
            }

            Account nearestAccount = null;
            double minDistance = double.MaxValue;

            foreach (var account in accounts)
            {
                // Skip accounts that do not have valid coordinates
                if (account.Lat == 0 && account.Lon == 0) continue;

                // Calculate distance to the current account
                double distance = CalculateDistance(lat, lon, account.Lat, account.Lon);

                Debug.WriteLine($"Account {account.AccountId} ({account.Username}):");
                Debug.WriteLine($" - Coordinates: ({account.Lat}, {account.Lon})");
                Debug.WriteLine($" - Distance: {distance} meters");

                // Update nearest account if a closer one is found
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestAccount = account;
                }
            }

            return nearestAccount;
        }
    }
}
