using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Household_Management_System.Models
{
    public class LocalAddressModel
    {
        private string provinceCode, provinceName, districtCode, districtName, wardCode, wardName, villageCode, villageName;

        public LocalAddressModel(string provinceCode, string provinceName, string districtCode, string districtName, string wardCode, string wardName, string villageCode, string villageName)
        {
            this.ProvinceCode = provinceCode;
            this.ProvinceName = provinceName;
            this.DistrictCode = districtCode;
            this.DistrictName = districtName;
            this.WardCode = wardCode;
            this.WardName = wardName;
            this.VillageCode = villageCode;
            this.VillageName = villageName;
        }

        public string ProvinceCode { get => provinceCode; set => provinceCode = value; }
        public string ProvinceName { get => provinceName; set => provinceName = value; }
        public string DistrictCode { get => districtCode; set => districtCode = value; }
        public string DistrictName { get => districtName; set => districtName = value; }
        public string WardCode { get => wardCode; set => wardCode = value; }
        public string WardName { get => wardName; set => wardName = value; }
        public string VillageCode { get => villageCode; set => villageCode = value; }
        public string VillageName { get => villageName; set => villageName = value; }

        public string GetFullAddress()
        {
            return wardName + ", " + districtName + ", " + provinceName;
        }
    }
}
