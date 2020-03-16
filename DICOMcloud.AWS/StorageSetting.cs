using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DICOMcloud.AWS
{
    public class StorageSetting
    {
        string bucketName = "qnpacs_storage";
        string accessKey = "8G85X13C9C36QQRBFTCY";
        string secretKey = "B0DxngrV3szS2ArFaWl6i1xknYHrK052rcAm10oM";

        public StorageSetting()
        {
            _config = new AmazonS3Config();
            _config.ServiceURL = "https://ss-hn-1.vccloud.vn";
            _config.ForcePathStyle = true;
        }

        public string BucketName { get => bucketName; set => bucketName = value; }
        public string AccessKey { get => accessKey; set => accessKey = value; }
        public string SecretKey { get => secretKey; set => secretKey = value; }

        public AmazonS3Config _config { get; set; }

        public AmazonS3Client AmazonS3Client
        {
            get
            {
                return new AmazonS3Client(accessKey, secretKey, _config);
            }
        }
    }
}
