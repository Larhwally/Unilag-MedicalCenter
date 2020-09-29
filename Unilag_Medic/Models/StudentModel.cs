using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Unilag_Medic.Models
{
    public class StudentModel
    {
        [JsonProperty("MatricNo")]
        public string MatricNo { get; set; }

        [JsonProperty("ProgrammeID")]
        public string ProgrammeId { get; set; }


        [JsonProperty("ProgrammeStatus")]
        public string ProgrammeStatus { get; set; }

        [JsonProperty("NameTitle")]
        public string NameTitle { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("MiddleName")]
        public string MiddleName { get; set; }

        [JsonProperty("Gender")]
        public string Gender { get; set; }

        [JsonProperty("Religion")]
        public string Religion { get; set; }

        [JsonProperty("DateOfBirth")]
        public string DateOfBirth { get; set; }

        [JsonProperty("Nationality")]
        public string Nationality { get; set; }

        [JsonProperty("StateOfOrigin")]
        public string StateOfOrigin { get; set; }

        [JsonProperty("LocalGovernmentArea")]
        public string LocalGovernmentArea { get; set; }

        [JsonProperty("MaritalStatus")]
        public string MaritalStatus { get; set; }

        [JsonProperty("NextOfKinName")]
        public string NextOfKinName { get; set; }

        [JsonProperty("AddressOfNextOfKin")]
        public string AddressOfNextOfKin { get; set; }

        [JsonProperty("NextOfKinTelephoneNumber")]
        public string NextOfKinTelephoneNumber { get; set; }

        [JsonProperty("SponsorName")]
        public string SponsorName { get; set; }

        [JsonProperty("SponsorContactAddress")]
        public string SponsorContactAddress { get; set; }

        [JsonProperty("SponsorTelephoneNumber")]
        public string SponsorTelephoneNumber { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }

        [JsonProperty("PostalAddress")]
        public string PostalAddress { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("InstitutionEmail")]
        public string InstitutionEmail { get; set; }

        [JsonProperty("AlternateEmail")]
        public string AlternateEmail { get; set; }

        [JsonProperty("Telephone")]
        public string Telephone { get; set; }

        [JsonProperty("Mobile")]
        public string Mobile { get; set; }

        [JsonProperty("FathersPhone")]
        public string FathersPhone { get; set; }

        [JsonProperty("FathersEmail")]
        public string FathersEmail { get; set; }

        [JsonProperty("MothersPhone")]
        public string MothersPhone { get; set; }

        [JsonProperty("MothersEmail")]
        public string MothersEmail { get; set; }


    }

}