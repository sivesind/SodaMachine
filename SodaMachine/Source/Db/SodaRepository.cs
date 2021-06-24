using SodaMachine.DomainModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SodaMachine.Repository
{
    /// <summary>
    /// Emulates the db layer and storage in the machine. Simple Repository approach. 
    /// <seealso cref="https://medium.com/@pererikbergman/repository-design-pattern-e28c0f3e4a30"/>
    /// </summary>
    public class SodaRepository
    {
        //data in json, for simple adding of new sodas
        private readonly string _jsonFilePath = "Source/Db/sodatable.json";
        
        //our simple data structure
        private List<SodaDbRow> _sodaDbRows = new List<SodaDbRow>();


        /// <summary>
        /// Reads sodas from jsonfile. FIlepath can be overridden for testing.
        /// </summary>
        /// <param name="jsonFilePath">To override jsonfile for testing</param>
        internal SodaRepository(string jsonFilePath = null)
        {
            //override path?
            if (!String.IsNullOrWhiteSpace(jsonFilePath))
            {
                _jsonFilePath = jsonFilePath;
            }

            //read file
            using (StreamReader r = new StreamReader(_jsonFilePath))
            {
                string jsonString = r.ReadToEnd();
                _sodaDbRows = JsonSerializer.Deserialize<List<SodaDbRow>>(jsonString);
            }
        }

        public string ListSodaNames()
        {
            return string.Join(",", _sodaDbRows.Select(soda => soda.Name));
        }

        public bool ValidSodaName(string requestedSodaName)
        {
            return GetSodaByName(requestedSodaName) != null;
        }

        public bool HaveInStock(uint requestedNumberInStock, string requestedSodaName)
        {
            SodaDbRow soda = GetSodaByName(requestedSodaName);
            return
                soda != null
                &&
                soda.NumberInStock >= requestedNumberInStock;
        }

        public bool HaveInStock(uint requestedNumberInStock, string requestedSodaname, out Soda sodaInStock)
        {
            var sodaInDb = GetSodaByName(requestedSodaname);
            sodaInStock = sodaInDb != null && sodaInDb.NumberInStock >= requestedNumberInStock ? new Soda(sodaInDb.Name, sodaInDb.Price) : null;
            return sodaInStock != null;
        }

        public void RemoveFromStock(uint numberToRemove, string requestedSodaName)
        {
            var sodaInDb = GetSodaByName(requestedSodaName);
            //ignore overdraw, not supported in this version anyway
            if (sodaInDb != null && sodaInDb.NumberInStock >= numberToRemove)
            {
                sodaInDb.NumberInStock = sodaInDb.NumberInStock - numberToRemove;
            }
        }

        private SodaDbRow GetSodaByName(string sodaName)
        {
            if (!String.IsNullOrWhiteSpace(sodaName))
            {
                return _sodaDbRows.FirstOrDefault(soda => soda.Name == sodaName.ToLower());
            }
            return null;
        }

        private class SodaDbRow
        {
            public string Name { get; set; }
            public uint Price { get; set; }
            public uint NumberInStock { get; set; }
        }
    }
}
