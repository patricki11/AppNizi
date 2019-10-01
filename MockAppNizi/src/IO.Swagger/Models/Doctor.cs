/*
 * App Nizi API
 *
 * The API for the Nizi-app
 *
 * OpenAPI spec version: 1.0.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Models
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class Doctor : IEquatable<Doctor>
    { 
        /// <summary>
        /// Gets or Sets Account
        /// </summary>
        [DataMember(Name="account")]
        public ComponentsschemaAccount Account { get; set; }

        /// <summary>
        /// Gets or Sets DoctorId
        /// </summary>
        [Required]
        [DataMember(Name="doctor_id")]
        public int? DoctorId { get; set; }

        /// <summary>
        /// Gets or Sets Birthdate
        /// </summary>
        [DataMember(Name="birthdate")]
        public int? Birthdate { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Doctor {\n");
            sb.Append("  Account: ").Append(Account).Append("\n");
            sb.Append("  DoctorId: ").Append(DoctorId).Append("\n");
            sb.Append("  Birthdate: ").Append(Birthdate).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Doctor)obj);
        }

        /// <summary>
        /// Returns true if Doctor instances are equal
        /// </summary>
        /// <param name="other">Instance of Doctor to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Doctor other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Account == other.Account ||
                    Account != null &&
                    Account.Equals(other.Account)
                ) && 
                (
                    DoctorId == other.DoctorId ||
                    DoctorId != null &&
                    DoctorId.Equals(other.DoctorId)
                ) && 
                (
                    Birthdate == other.Birthdate ||
                    Birthdate != null &&
                    Birthdate.Equals(other.Birthdate)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                    if (Account != null)
                    hashCode = hashCode * 59 + Account.GetHashCode();
                    if (DoctorId != null)
                    hashCode = hashCode * 59 + DoctorId.GetHashCode();
                    if (Birthdate != null)
                    hashCode = hashCode * 59 + Birthdate.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(Doctor left, Doctor right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Doctor left, Doctor right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
