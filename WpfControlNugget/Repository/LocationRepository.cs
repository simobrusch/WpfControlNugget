using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using WpfControlNugget.Model;

namespace WpfControlNugget.Repository
{
    public class LocationRepository : RepositoryBase<LocationModel>
    {
        public override string TableName => "Location";

        public List<LocationModel> Locations { get; set; }
        public LocationModel _Locations { get; set; }
        public LocationRepository(string connectionString) : base(connectionString)
        {
            Locations = new List<LocationModel>();
        }
        public override void Add(LocationModel location)
        {
            try
            {
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText =
                            $"INSERT INTO {TableName} (address_fk , designation , building , room) VALUES (address_fk = {location.Id} , designation = '{location.Designation}', building = {location.BuildingNr} , room = {location.RoomNr} WHERE location_id = {location.Id})";
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
        }

        public override void Delete(LocationModel location)
        {
            try
            {
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open(); 
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $"DELETE FROM {TableName} WHERE location_id = {location.Id}";
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
        }

        public override List<LocationModel> GetAll(string whereCondition, Dictionary<string, object> parameterValues)
        {
            var whereCon = whereCondition;
            if (parameterValues.Count > 0 && whereCondition != null)
            {
                foreach (KeyValuePair<string, object> p in parameterValues)
                {
                    whereCon = whereCon.Replace($"@{p.Key}", p.Value.ToString());
                }
            }
            try
            {
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $"SELECT location_id, parentId, address_fk, designation, building, room FROM {TableName} WHERE {whereCon}";
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Locations.Add(new LocationModel(

                                reader.GetInt32("location_id"),
                                reader.GetInt32("parentId"),
                                reader.GetInt32("address_fk"),
                                reader.GetValue(reader.GetOrdinal("designation")) as string,
                                reader.GetInt32("building"),
                                reader.GetInt32("room")
                            ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
            return Locations;
        }

        public override List<LocationModel> GetAll()
        {
            try
            {
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $"SELECT location_id, parentId, address_fk, designation, building, room FROM {TableName}";
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Locations.Add(new LocationModel(

                                reader.GetInt32("location_id"),
                                reader.GetInt32("parentId"),
                                reader.GetInt32("address_fk"),
                                reader.GetValue(reader.GetOrdinal("designation")) as string,
                                reader.GetInt32("building"),
                                reader.GetInt32("room")
                            ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
            return Locations;
        }

        public override void CallStoredProcedure(LocationModel entity)
        {
            throw new NotImplementedException();
        }

        public override LocationModel GetSingle<P>(P pkValue)
        {
            try
            {
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $"SELECT location_id, parentId, address_fk, designation, building, room FROM {TableName} WHERE location_id = {pkValue}";
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            _Locations = (new LocationModel(

                                reader.GetInt32("location_id"),
                                reader.GetInt32("parentId"),
                                reader.GetInt32("address_fk"),
                                reader.GetValue(reader.GetOrdinal("designation")) as string,
                                reader.GetInt32("building"),
                                reader.GetInt32("room")
                            ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
            return _Locations;
        }

        public override void Update(LocationModel location)
        {
            try
            {
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText =
                            $"UPDATE {TableName} SET address_fk = {location.Id} , designation = '{location.Designation}', building = {location.BuildingNr} , room = {location.RoomNr} WHERE location_id = {location.Id}";
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
        }
    }
}
