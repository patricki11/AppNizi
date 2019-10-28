﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using AppNiZiAPI.Models.Dietarymanagement;

namespace AppNiZiAPI.Models.Repositories
{
    public interface IDietaryManagementRepository
    {
        bool AddDietaryManagement(DietaryManagementModel dietary);
        bool DeleteDietaryManagement(int id);
        List<DietaryManagementModel> GetDietaryManagementByPatient(int patientId);
        List<DietaryRestriction> GetDietaryRestrictions();
        bool UpdateDietaryManagement(int id, DietaryManagementModel dietaryManagement);
    }

    public class DietaryManagementRepository : Repository, IDietaryManagementRepository
    {

        public List<DietaryManagementModel> GetDietaryManagementByPatient(int patientId)
        {

            List<DietaryManagementModel> dietaryManagementModels = new List<DietaryManagementModel>();

            conn.Open();
            var text = $"SELECT b.description, a.amount, a.is_active, a.patient_id, a.id " +
                $"FROM DietaryManagement AS a, DietaryRestriction AS b " +
                $"WHERE a.dietary_restriction_id = b.id AND a.patient_id={patientId}";

            using (SqlCommand cmd = new SqlCommand(text, conn))
            {
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    DietaryManagementModel dietaryManagementModel = new DietaryManagementModel();
                    while (reader.Read())
                    {

                        // Uit lezen bijv
                        dietaryManagementModel.Id = Int32.Parse(reader["id"].ToString());
                        dietaryManagementModel.Description = reader["description"].ToString();
                        dietaryManagementModel.Amount = Int32.Parse(reader["amount"].ToString());
                        dietaryManagementModel.PatientId = Int32.Parse(reader["patient_id"].ToString());
                        dietaryManagementModel.IsActive = Convert.ToBoolean(reader["is_active"]);

                        dietaryManagementModels.Add(dietaryManagementModel);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            conn.Close();
            return dietaryManagementModels;
        }

        public List<DietaryRestriction> GetDietaryRestrictions()
        {
            List<DietaryRestriction> dietaryRestrictions = new List<DietaryRestriction>();
            conn.Open();
            var query = "SELECT * FROM DietaryRestriction";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                try
                {
                    DietaryRestriction restriction = new DietaryRestriction();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        restriction.Id = Int32.Parse(reader["id"].ToString());
                        restriction.Description = reader["description"].ToString();
                        dietaryRestrictions.Add(restriction);
                    }
                }
                catch (Exception e)
                {

                    throw e;
                }
            }

            conn.Close();
            return dietaryRestrictions;
        }


        public bool UpdateDietaryManagement(int id, DietaryManagementModel dietaryManagement)
        {

            conn.Open();
            var query = @"UPDATE DietaryManagement
                                SET dietary_restriction_id = (SELECT r.id
                                                                FROM DietaryRestriction as r
                                                                WHERE r.description = @DESCRIPTION)
                                  ,amount = @AMOUNT
                                  ,patient_id = @PATIENT
                                  ,is_active = @ISACTIVE
                             WHERE id = @ID";

            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = dietaryManagement.Description;
            command.Parameters.Add("@AMOUNT", SqlDbType.Int).Value = dietaryManagement.Amount;
            command.Parameters.Add("@PATIENT", SqlDbType.Int).Value = dietaryManagement.PatientId;
            command.Parameters.Add("@ISACTIVE", SqlDbType.Bit).Value = dietaryManagement.IsActive;
            command.Parameters.Add("@ID", SqlDbType.Int).Value = id;

            return command.ExecuteNonQuery() > 0;

        }

        public bool DeleteDietaryManagement(int id)
        {

            conn.Open();
            var query = @"DELETE FROM DietaryManagement
                                WHERE id = @ID";

            try
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                return command.ExecuteNonQuery() > 0;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public bool AddDietaryManagement(DietaryManagementModel dietary)
        {
            conn.Open();
            var query = @"INSERT INTO DietaryManagement
                                   (dietary_restriction_id,
                                    amount,
                                    patient_id,
                                    is_active)
                             VALUES
                                   (
                                    (
                                    SELECT r.id
                                        FROM DietaryRestriction as r
                                        WHERE r.description = @DESCRIPTION
                                    ),
                                    @AMOUNT,
                                    @PATIENT,
                                    @ISACTIVE
                                )";

            try
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = dietary.Description;
                command.Parameters.Add("@AMOUNT", SqlDbType.Int).Value = dietary.Amount;
                command.Parameters.Add("@PATIENT", SqlDbType.Int).Value = dietary.PatientId;
                command.Parameters.Add("@ISACTIVE", SqlDbType.Bit).Value = dietary.IsActive;
                return command.ExecuteNonQuery() > 0;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
