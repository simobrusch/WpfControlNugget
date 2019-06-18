using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfControlNugget.Model
{
    public class LocationModel : ModelBase<LocationModel>
    {
        public new int Id { get; set; }
        public int? ParentId { get; set; }
        public int AdressId { get; set; }
        public string Designation { get; set; }
        public int BuildingNr { get; set; }
        public int RoomNr { get; set; }


        public LocationModel()
        {

        }
        public LocationModel(int id, int? parentId, int adressId, string designation, int buildingNr, int roomNr)
        {
            this.Id = id;
            this.ParentId = parentId;
            this.AdressId = adressId;
            this.Designation = designation;
            this.BuildingNr = buildingNr;
            this.RoomNr = roomNr;
        }
        public bool Equals(LocationModel location)
        {
            if (Object.ReferenceEquals(null, location)) return false;
            if (Object.ReferenceEquals(this, location)) return true;

            return String.Equals(Designation, location.Designation) && String.Equals(BuildingNr, location.BuildingNr) && String.Equals(RoomNr, location.RoomNr);
        }
        public override bool Equals(object value)
        {
            return Equals(value as LocationModel);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                // Choose large primes to avoid hashing collisions
                const int hashingBase = (int)2166136261;
                const int hashingMultiplier = 16777619;

                int hash = hashingBase;
                hash = (hash * hashingMultiplier) ^ (!Object.ReferenceEquals(null, Designation) ? Designation.GetHashCode() : 0);
                hash = (hash * hashingMultiplier) ^ (!Object.ReferenceEquals(null, BuildingNr) ? BuildingNr.GetHashCode() : 0);
                hash = (hash * hashingMultiplier) ^ (!Object.ReferenceEquals(null, RoomNr) ? RoomNr.GetHashCode() : 0);
                return hash;
            }
        }
    }
}

