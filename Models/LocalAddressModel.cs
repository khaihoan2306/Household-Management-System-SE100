using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Household_Management_System.Models
{
    public class LocalAddressModel
    {
        private string provinceCode, provinceName, districtCode, districtName, wardCode, wardName;

        public LocalAddressModel(string provinceCode, string provinceName, string districtCode, string districtName, string wardCode, string wardName)
        {
            this.ProvinceCode = provinceCode;
            this.ProvinceName = provinceName;
            this.DistrictCode = districtCode;
            this.DistrictName = districtName;
            this.WardCode = wardCode;
            this.WardName = wardName;
        }

        public string ProvinceCode { get => provinceCode; set => provinceCode = value; }
        public string ProvinceName { get => provinceName; set => provinceName = value; }
        public string DistrictCode { get => districtCode; set => districtCode = value; }
        public string DistrictName { get => districtName; set => districtName = value; }
        public string WardCode { get => wardCode; set => wardCode = value; }
        public string WardName { get => wardName; set => wardName = value; }
        public string GetFullAddress()
        {
            return wardName + ", " + districtName + ", " + provinceName;
        }
    }
}
